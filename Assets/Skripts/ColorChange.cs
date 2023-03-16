using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorChange : MonoBehaviour
{
    public Material GreenMaterial;
    public Material RedMaterial;
    public Material BlueMaterial;
    public Material YellowMaterial;
    private Material PreviousMaterial;

   /* public GameObject confettiBlastBlue;
    public GameObject confettiBlastGreenYellow;
    public GameObject confettiBlastOrangePurple;
    public GameObject confettiBlastRainbow;
   */
    public float confettisDelay = 1.0f;
    public GameObject youWon;
    public GameObject confetti;
    public GameObject sadMouth;
    public GameObject happyMouth;
    public GameObject sadMouth1;
    public GameObject happyMouth1;
    public GameObject sadMouth2;
    public GameObject happyMouth2;

    //public GameObject plane;
    public AudioSource cubeSound;

    // Start is called before the first frame update
    void Start()
    {

        PreviousMaterial = GetComponent<Renderer>().material;
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
            sadMouth.SetActive(false);
            happyMouth.SetActive(true);
          
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
            sadMouth2.SetActive(false);
            happyMouth2.SetActive(true);
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
            sadMouth1.SetActive(false);
            happyMouth1.SetActive(true);
            CheckWinCondition();
            if (cubeSound != null)
            {
                cubeSound.Play();
            }

        }

    }

    public void TurnYellow()
    {
        GameObject cube = GameObject.FindWithTag("Cube");
        GameObject cube1 = GameObject.FindWithTag("Cube1");
        GameObject cube2 = GameObject.FindWithTag("Cube2");

        if (cube != null)
        {
            cube.GetComponent<Renderer>().material = YellowMaterial;
            sadMouth.SetActive(true);
            happyMouth.SetActive(false);
        }

        if (cube1 != null)
        {
            cube1.GetComponent<Renderer>().material = YellowMaterial;
            sadMouth1.SetActive(true);
            happyMouth1.SetActive(false);
        }

        if (cube2 != null)
        {
            cube2.GetComponent<Renderer>().material = YellowMaterial;
            sadMouth2.SetActive(true);
            happyMouth2.SetActive(false);
        }

        CheckWinCondition();

        if (cubeSound != null)
        {
            cubeSound.Play();
        }

    }

    public void TurnBack()
    {
        GetComponent<Renderer>().material = PreviousMaterial;
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
            //confettiBlastBlue.SetActive(true);
            //confettiBlastGreenYellow.SetActive(true);
           //confettiBlastOrangePurple.SetActive(true);
           // confettiBlastRainbow.SetActive(true);
            StartCoroutine(winInfo());




        }

    }
    IEnumerator winInfo()
    {
        PlayerProgress.wonCubes = true;
        confetti.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        youWon.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

    }
}



