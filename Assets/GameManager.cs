using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;
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
        }

        }
    public void wrongMove()
    {
        correctedPipes -= 1;
    }
}
