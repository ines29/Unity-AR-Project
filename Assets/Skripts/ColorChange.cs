using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Material GreenMaterial;
    public Material RedMaterial;
    public Material BlueMaterial;
    public Material YellowMaterial;
    private Material PreviousMaterial;

    public GameObject fireworks;
    public float fireworksDelay = 1.0f;

    //public GameObject plane;
    public AudioSource cubeSound;

    // Start is called before the first frame update
    void Start()
    {
        PreviousMaterial = GetComponent<Renderer>().material;

        fireworks.SetActive(false);

        /* if (GameObject.FindWithTag("Rules")!= null)
         {
             plane.SetActive(true);

             Invoke("DetectivePlane", 10f);
         }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TurnRed()
    {
        GameObject cube = GameObject.FindWithTag("Cube");
        if (cube != null)
        {
            cube.GetComponent<Renderer>().material = RedMaterial;
            CheckWinCondition();
            if (cubeSound != null)
            {
                cubeSound.Play();
            }
           
        }
    }

    public void TurnBlue()
    {
        GameObject cube = GameObject.FindWithTag("Cube2");
        if (cube != null)
        {
            cube.GetComponent<Renderer>().material = BlueMaterial;
            CheckWinCondition();
            if (cubeSound != null)
            {
                cubeSound.Play();
            }

        }
    }

    public void TurnGreen()
    {
        GameObject cube = GameObject.FindWithTag("Cube1");
        if (cube != null)
        {
            cube.GetComponent<Renderer>().material = GreenMaterial;
            CheckWinCondition();
            if (cubeSound != null)
            {
                cubeSound.Play();
            }

        }

    }

    public void TurnBack()
    {
        GetComponent<Renderer>().material = PreviousMaterial;
    }

    public void PlayFireworks()
    {
        StartCoroutine(PlayFireworksCoroutine());
    }

    private IEnumerator PlayFireworksCoroutine()
    {
        yield return new WaitForSeconds(fireworksDelay);
        fireworks.SetActive(true);
        //start the fireworks particle system
        fireworks.GetComponent<ParticleSystem>().Play();
    }

    public void CheckWinCondition()
    {
        GameObject cube = GameObject.FindWithTag("Cube");
        GameObject cube1 = GameObject.FindWithTag("Cube1");
        GameObject cube2 = GameObject.FindWithTag("Cube2");

        if (cube.GetComponent<Renderer>().material.color == RedMaterial.color &&
            cube1.GetComponent<Renderer>().material.color == GreenMaterial.color &&
            cube2.GetComponent<Renderer>().material.color == BlueMaterial.color)
        {

            Debug.Log("You won the game!");
            PlayFireworks();

        }

    }
}

