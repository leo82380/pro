using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [Header("�α���")]
    public Text statusText;
    public InputField nickNameInput;
    public InputField roomNameInput;
    public GameObject uiPanel;
    public byte userNum = 5;

    private bool connect = false;

    [Header("���� ������")]
    public GameObject userPanel;
    public Text userName;

    private void Update() => statusText.text = PhotonNetwork.NetworkClientState.ToString();

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        print("�������� �Ϸ�");
        string nickName = PhotonNetwork.LocalPlayer.NickName;
        nickName = nickNameInput.text;
        print($"����� �̸��� {nickName} �Դϴ�.");
        connect = true;
    }
    public void Disconnect() => PhotonNetwork.Disconnect();
    public override void OnDisconnected(DisconnectCause cause) => print("�������");

    public void JoinRoom()
    {
        if (connect)
        {
            PhotonNetwork.JoinRandomRoom();
            uiPanel.SetActive(false);
            print(roomNameInput.text + "�濡 �����Ͽ����ϴ�");
            userPanel.SetActive(true);
            userName.text = "����� �̸�: " + nickNameInput.text;
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
        => PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = userNum });
    public override void OnJoinedRoom()
        => PhotonNetwork.Instantiate("player", Vector3.zero, Quaternion.identity);
}
