using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();  
        
    }
    public override void OnConnectedToMaster() {
        Debug.Log("connected to server");
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
