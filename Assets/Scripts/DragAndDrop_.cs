using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DragAndDrop_ : MonoBehaviour
{
    // Array von Puzzlebildern
    public Sprite[] Levels;

    // Referenz auf das Endmenu-GameObject
    public GameObject EndMenu;

    // Referenz auf das ausgewählte Puzzle-Teil
    public GameObject SelectedPiece;

    // Zählvariable für die Sortierreihenfolge
    int OIL = 1;

    // Zählvariable für die Anzahl der platzierten Puzzle-Teile
    public int PlacedPieces = 0;



    void Start()
    {
        // Setze das Puzzlebild für jedes Puzzle-Teil
        for (int i = 0; i < 36; i++)
        {
            // Puzzlebild für jedes Teil setzen
            GameObject.Find("Piece (0)").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Levels[0];
        }

    }

    void Update()
    {

        // Wenn die linke Maustaste gedrückt wird
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast ausführen, um das angeklickte Puzzle-Teil zu ermitteln
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            // Wenn das angeklickte Objekt das Tag "Puzzle" hat
            if (hit.transform.CompareTag("Puzzle"))
            {
                // Wenn das Puzzle-Teil noch nicht richtig platziert ist
                if (!hit.transform.GetComponent<piceseScript>().InRightPosition)
                {
                    // Setze das ausgewählte Puzzle-Teil und erhöhe den Sortierindex
                    SelectedPiece = hit.transform.gameObject;
                    SelectedPiece.GetComponent<piceseScript>().Selected = true;
                    SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                    OIL++;
                }
            }
        }

        // Wenn die linke Maustaste losgelassen wird
        if (Input.GetMouseButtonUp(0))
        {
            // Setze das ausgewählte Puzzle-Teil auf nicht-ausgewählt
            if (SelectedPiece != null)
            {
                SelectedPiece.GetComponent<piceseScript>().Selected = false;
                SelectedPiece = null;
            }
        }

        // Wenn ein Puzzle-Teil ausgewählt wurde
        if (SelectedPiece != null)
        {
            // Setze die Position des ausgewählten Puzzle-Teils auf die Position des Mauszeigers
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SelectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);
        }

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
