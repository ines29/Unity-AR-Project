using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class comunicationArduino : MonoBehaviour
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
