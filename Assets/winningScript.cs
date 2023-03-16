using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class winningScript : MonoBehaviour
{

    //Zählvariable für die Anzahl der platzierten Puzzle-Teile
    public int PlacedPieces = 0;

    //Referenz auf das Endmenu-GameObject

    public GameObject testCube;
    public float confettisDelay = 1.0f;
    public GameObject youWon;
    public GameObject confetti;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

    }

    public void counter()
    {
        PlacedPieces++;
        Debug.Log("Placed Pieces: "+PlacedPieces);

        // Wenn alle Puzzle-Teile richtig platziert wurden
        if (PlacedPieces == 36)
        {
            // Aktiviere das Endmenu-GameObject und gebe eine Debug-Meldung aus
            Debug.Log("You won!");
            PlayerProgress.wonJigsaw = true;
            StartCoroutine(EndGame());
        }
    }
    IEnumerator EndGame()
    {
        confetti.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        youWon.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }



}
