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

    public GameObject confettiBlastBlue;
    public GameObject confettiBlastGreenYellow;
    public GameObject confettiBlastOrangePurple;
    public GameObject confettiBlastRainbow;
    public float confettisDelay = 1.0f;

    //public GameObject plane;
    public AudioSource cubeSound;

    // Start is called before the first frame update
    void Start()
    {

        PreviousMaterial = GetComponent<Renderer>().material;

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

   /* public void PlayConfettis()
    {
        StartCoroutine(PlayConfettiCoroutine());
    }*/

   /* private IEnumerator PlayConfettiCoroutine()
    {
        yield return new WaitForSeconds(confettisDelay);
        confettiBlastBlue.SetActive(true);
        confettiBlastGreenYellow.SetActive(true);
        confettiBlastOrangePurple.SetActive(true);
        confettiBlastRainbow.SetActive(true);

        //start the confetti particle system
        confettiBlastBlue.GetComponent<ParticleSystem>().Play();
        confettiBlastGreenYellow.GetComponent<ParticleSystem>().Play();
        confettiBlastOrangePurple.GetComponent<ParticleSystem>().Play();
        confettiBlastRainbow.GetComponent<ParticleSystem>().Play();

    }
   */


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
            //PlayConfettis();
            /*if (GameObject.FindWithTag("Confetti") != null)
            {
                confettiBlastBlue.SetActive(true);
            }*/
            //
            confettiBlastBlue.SetActive(true);
            confettiBlastGreenYellow.SetActive(true);
            confettiBlastOrangePurple.SetActive(true);
            confettiBlastRainbow.SetActive(true);


        }

    }
}

