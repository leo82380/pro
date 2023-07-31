using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [Header("로그인")]
    public Text statusText;
    public InputField nickNameInput;
    public InputField roomNameInput;
    public GameObject uiPanel;
    public byte userNum = 5;

    private bool connect = false;

    [Header("유저 프로필")]
    public GameObject userPanel;
    public Text userName;

    private void Update() => statusText.text = PhotonNetwork.NetworkClientState.ToString();

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        print("서버접속 완료");
        string nickName = PhotonNetwork.LocalPlayer.NickName;
        nickName = nickNameInput.text;
        print($"당신의 이름은 {nickName} 입니다.");
        connect = true;
    }
    public void Disconnect() => PhotonNetwork.Disconnect();
    public override void OnDisconnected(DisconnectCause cause) => print("연결끊김");

    public void JoinRoom()
    {
        if (connect)
        {
            PhotonNetwork.JoinRandomRoom();
            uiPanel.SetActive(false);
            print(roomNameInput.text + "방에 입장하였습니다");
            userPanel.SetActive(true);
            userName.text = "당신의 이름: " + nickNameInput.text;
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
        => PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = userNum });
    public override void OnJoinedRoom()
        => PhotonNetwork.Instantiate("player", Vector3.zero, Quaternion.identity);
}
