using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;
    public GameObject Water;
    [SerializeField]
    public int totalPipes = 0;
    [SerializeField]
    int correctedPipes;
    // Start is called before the first frame update
    void Start()
    {
        totalPipes = PipesHolder.transform.childCount;
        Pipes = new GameObject[totalPipes];
        for(int i = 0; i<Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        correctedPipes += 1;
        if (correctedPipes == totalPipes)
        {
            Debug.Log("You win!");
            Water.SetActive(true);
            PlayerProgress.wonWaterPuzzle = true;
            StartCoroutine(EndGame());
        }

        }
    public void wrongMove()
    {
        correctedPipes -= 1;
    }
    IEnumerator EndGame()
    {
        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    }
