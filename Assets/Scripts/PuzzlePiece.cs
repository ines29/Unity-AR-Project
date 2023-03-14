using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private Vector3 rightPosition; // die korrekte Position, an die das Puzzleteil platziert werden muss
    public bool inRightPosition; // gibt an, ob das Puzzleteil bereits an der korrekten Position ist
    public bool selected; // gibt an, ob das Puzzleteil gerade ausgewählt ist
    public GameObject placeholderPrefab; // das Prefab des Platzhalter-GameObjects

    private List<Transform> snaps = new List<Transform>(); // Liste der Zielpositionen

    void Start()
    {
    
        //"rightPosition" auf die aktuelle Position des Puzzleteils setzen
        rightPosition = transform.position;
        // Setze die Startposition des Puzzleteils auf eine zufällige Position innerhalb eines Bereichs
        transform.position = new Vector3(Random.Range(5f, 11f), Random.Range(2.5f, -7));

        // Erstelle den Platzhalter-GameObject an der richtigen Position
        GameObject placeholder = Instantiate(placeholderPrefab, rightPosition, Quaternion.identity);
        placeholder.name = gameObject.name + " Placeholder";

        // Fülle die Liste der Zielpositionen mit den korrekten Positionen
        GameObject[] snapPositions = GameObject.FindGameObjectsWithTag("SnapPosition");
        foreach (GameObject snapPosition in snapPositions)
        {
            snaps.Add(snapPosition.GetComponent<Transform>());
        }
        Debug.Log("Zielpositionen: " + GameObject.FindGameObjectsWithTag("SnapPosition"));
    }

    void Update()
    {
        // Aktualisiere das Puzzleteil nur, wenn es nicht ausgewählt ist
        if (!selected)
        {
            // Prüfe, ob das Puzzleteil an der richtigen Position ist
            // Iteriere über die Liste der Zielpositionen
            foreach (Transform snap in snaps)
            {
                // Prüfe, ob das Puzzleteil sich innerhalb des Radius von der aktuellen Zielposition befindet
                if (Vector3.Distance(snap.position, transform.position) < 0.5f)
                {
                  
                    transform.position = snap.position;  // Bewege das Puzzleteil auf die Zielposition
                    inRightPosition = true;  // Markiere das Puzzleteil als an der korrekten Position
                    break;
                }
                else
                {
                    inRightPosition = false;  //  Puzzleteil ist nicht an der korrekten Position
                }
            }
        }
    }
}
