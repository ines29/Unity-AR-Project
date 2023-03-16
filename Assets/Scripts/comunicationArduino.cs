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
        stream.Open();
        Debug.Log("connection Arduino started");

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(stream.ReadLine());
    }
}
