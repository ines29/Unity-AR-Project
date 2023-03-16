using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverallManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshPro text;
    public GameObject infopanel;
   
    void Start()
    {
        Debug.Log("Start");
        Debug.Log(PlayerProgress.wonWaterPuzzle);
        int solved = 0;
        if (PlayerProgress.wonChess)
        {
            solved++;
        }
        if (PlayerProgress.wonCubes)
        {
            solved++;
        }
        if (PlayerProgress.wonJigsaw)
        {
            solved++;
        }
        if (PlayerProgress.wonSchiebepuzzle)
        {
            solved++;
        }
        if (PlayerProgress.wonWaterPuzzle)
        {
            solved++;
        }
        if (PlayerProgress.wonLabyrinth)
        {
            solved++;
        }


        if (solved >= 1)
        {
            text.text = solved + "/6";
            infopanel.SetActive(true);


        }







    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
