using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winningScript : MonoBehaviour
{

    //Zählvariable für die Anzahl der platzierten Puzzle-Teile
    public int PlacedPieces = 0;

    //Referenz auf das Endmenu-GameObject
    public GameObject EndMenu;

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
            EndMenu.SetActive(true);
            Debug.Log("You won!");
        }
    }

    // Methode, um das Spiel zu beenden
    public void EndGame()
    {
        // Deaktiviere das Puzzle-Objekt
        GameObject puzzleObject = GameObject.Find("Puzzle");
        puzzleObject.SetActive(false);

        // Deaktiviere das EndMenu-Objekt
        EndMenu.SetActive(false);
    }
}
