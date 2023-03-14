using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private Vector3 rightPosition; // die korrekte Position, an die das Puzzleteil platziert werden muss
    public bool inRightPosition; // gibt an, ob das Puzzleteil bereits an der korrekten Position ist
    public bool selected; // gibt an, ob das Puzzleteil gerade ausgew�hlt ist
    public GameObject placeholderPrefab; // das Prefab des Platzhalter-GameObjects
    public GameObject script;

    private List<Transform> snaps = new List<Transform>(); // Liste der Zielpositionen



    void Start()
    {
    
        //"rightPosition" auf die aktuelle Position des Puzzleteils setzen
        rightPosition = transform.position;
        // Setze die Startposition des Puzzleteils auf eine zuf�llige Position innerhalb eines Bereichs
        transform.position = new Vector3(Random.Range(5f, 11f), Random.Range(2.5f, -7));

        // Erstelle den Platzhalter-GameObject an der richtigen Position
        GameObject placeholder = Instantiate(placeholderPrefab, rightPosition, Quaternion.identity);
        placeholder.name = gameObject.name + " Placeholder";

       // Debug.Log("Zielpositionen: " + GameObject.FindGameObjectsWithTag("SnapPosition"));
    }

    void Update()
    {
       
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name+ gameObject.name);
        if (other.name == gameObject.name+" Placeholder")
        {
            Debug.Log("Match");
            StartCoroutine(Snap());
         
                // Pr�fe, ob das Puzzleteil sich innerhalb des Radius von der aktuellen Zielposition befindet

                    
        }

    }
    IEnumerator Snap()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        transform.position = rightPosition;  // Bewege das Puzzleteil auf die Zielposition
        inRightPosition = true;  // Markiere das Puzzleteil als an der korrekten Position
        GetComponent<NearInteractionGrabbable>().enabled = false;
        GetComponent<ObjectManipulator>().enabled = false;

    }
      
    
}
