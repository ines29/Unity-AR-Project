using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class piceseScript : MonoBehaviour
{
    private Vector3 RightPosition; // die korrekte Position, an die das Puzzleteil platziert werden muss
    public bool InRightPosition; // gibt an, ob das Puzzleteil bereits an der korrekten Position ist
    public bool Selected; // gibt an, ob das Puzzleteil gerade ausgewählt ist

    void Start()
    {
        // Setze die Startposition des Puzzleteils auf eine zufällige Position innerhalb eines Bereichs
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(5f, 11f), Random.Range(2.5f, -7));
    }

    void Update()
    {
        // Überprüfe, ob das Puzzleteil nah genug an der korrekten Position ist
        if (Vector3.Distance(transform.position, RightPosition) < 0.5f)
        {
            if (!Selected) // Wenn das Puzzleteil nicht ausgewählt ist
            {
                if (InRightPosition == false) // Wenn es noch nicht an der korrekten Position ist
                {
                    transform.position = RightPosition; // Setze das Puzzleteil an die korrekte Position
                    InRightPosition = true; // Setze den Zustand des Puzzleteils auf "an der korrekten Position"
                    GetComponent<SortingGroup>().sortingOrder = 0; // Setze die Sorting-Order auf 0, um sicherzustellen, dass das Puzzleteil hinter den anderen Objekten ist
                    Camera.main.GetComponent<DragAndDrop_>().PlacedPieces++; // Inkrementiere die Anzahl der platzierten Puzzleteile im Spiel
                }
            }
        }
    }
}
