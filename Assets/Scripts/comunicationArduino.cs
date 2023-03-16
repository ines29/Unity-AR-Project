using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using Photon.Pun;

public class comunicationArduino : MonoBehaviourPunCallbacks
{
    SerialPort stream = new SerialPort("COM5", 9600);
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            stream.Open();
            Debug.Log("connection Arduino started");
            stream.ReadTimeout = 1;
        }
        catch (System.Exception)
        { 
        
        }
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() { 
    
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            try
            {
                string tmp = stream.ReadLine();
                Debug.Log("Read " + tmp  );
                stream.Write("1");
                if (tmp=="1") {
                    foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
                    {
                        if (gameObj.name == "Cube")
                        {
                            gameObj.GetComponent<Renderer>().material.color = new Color(0, 204, 102);
                            Debug.Log(gameObj.transform.position);
                        }
                    }

                }
                //Debug.Log("written");
                // do other stuff with the data
            }
            catch (System.Exception )
            {
                //Debug.Log("nothing recived");
                // no-op, just to silence the timeouts. 
                // (my arduino sends 12-16 byte packets every 0.1 secs)
            }
            
            

        }
        
    }

    void send() { 
    
    }
}
