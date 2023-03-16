using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if ((gameObject.GetComponent<Rigidbody>().velocity.x > 0.0001 ||
          gameObject.GetComponent<Rigidbody>().velocity.y > 0.0001 ||
          gameObject.GetComponent<Rigidbody>().velocity.z > 0.0001) &&
          !gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
        else if (gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().Pause();

        }
    }
}