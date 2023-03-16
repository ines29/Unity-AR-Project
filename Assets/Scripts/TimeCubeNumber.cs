using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeCubeNumber : MonoBehaviour
{
    float startRotation = 0f;
    int currentNumber;
    public string currentNumberChar;
    public AudioSource soundPlayer;
    public TimeCubeEvent tcEvent;

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        if (rotation.y != startRotation)
        {
            startRotation = rotation.y;
            //Debug.Log("x: " + rotation.x + " y: " + startRotation + " z:" + rotation.z);
            int modRotation = (int)Math.Round(((startRotation % 360) / 36 + 3) % 10);
            //Debug.Log("number: " + modRotation);
            currentNumber = modRotation;
            currentNumberChar = modRotation.ToString();
        }
    }

    public void snapNumber()
    {
        float y = transform.localRotation.eulerAngles.y;
        startRotation = (currentNumber - 3.0f) * 36.0f;
        transform.Rotate(0f, -(y - startRotation), 0f, Space.Self);
        soundPlayer.Play();
        tcEvent.checkYear();
    }
}
