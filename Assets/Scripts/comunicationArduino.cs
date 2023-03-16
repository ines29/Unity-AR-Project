using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using Photon.Pun;
using Photon.Realtime;


public class comunicationArduino : MonoBehaviour, IPunObservable
{
    public bool test;
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
                    this.test = true;

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

    

    public void IPunObservable.OnPhotonSerializeView(PhotonStream photonStream, PhotonMessageInfo info) {
        if (photonStream.isWriting)
        {
            photonStream.SendNext(this.test);
        }
        else {
            this.test = (bool)photonStream.ReceivNext();
            Debug.Log("empfangen from master");
        }
    
    }



}
