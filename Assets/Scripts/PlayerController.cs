using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public PhotonView pv;
    private bool isJump;
    private Rigidbody2D rigid;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (pv.IsMine)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * 7, Input.GetAxisRaw("Vertical") * Time.deltaTime * 7, 0));

            if (Input.GetButtonDown("Jump"))
            {
                isJump = true;
                pv.RPC("Jump", RpcTarget.All);
            }
        }
    }
    [PunRPC]
    void Jump(float axis)
    {
        if (!isJump)
            return;
        rigid.AddForce(Vector3.up * .5f, ForceMode2D.Impulse);
        isJump = false;
    }
}
