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


    private winningScript GameManager;


    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<winningScript>();

        //"rightPosition" auf die aktuelle Position des Puzzleteils setzen
        rightPosition = transform.position;
        // Setze die Startposition des Puzzleteils auf eine zuf�llige Position innerhalb eines Bereichs
        float range1 = Random.Range(5f, 11f);
        float range2 = Random.Range(2.5f, -7);
        Debug.Log(gameObject.name);
        Debug.Log(transform.position);
        Debug.Log("Range 1: " + range1 + " Range 2: " + range2);
        transform.position = new Vector3(range1, range2);
        Debug.Log(transform.position);

        // Erstelle den Platzhalter-GameObject an der richtigen Position
        GameObject placeholder = Instantiate(placeholderPrefab, rightPosition, Quaternion.identity);
        placeholder.name = gameObject.name + " Placeholder";

        // Debug.Log("Zielpositionen: " + GameObject.FindGameObjectsWithTag("SnapPosition"));
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + gameObject.name);
        if (other.name == gameObject.name + " Placeholder")
        {

            if (!inRightPosition)
            {
                inRightPosition = true;  // Markiere das Puzzleteil als an der korrekten Position
                Debug.Log("Match");
                StartCoroutine(Snap());

                // Pr�fe, ob das Puzzleteil sich innerhalb des Radius von der aktuellen Zielposition befindet

                transform.position = rightPosition;  // Bewege das Puzzleteil auf die Zielposition
                inRightPosition = true;  // Markiere das Puzzleteil als an der korrekten Position
                GetComponent<NearInteractionGrabbable>().enabled = false;
                GetComponent<ObjectManipulator>().enabled = false;


                GameManager.counter();
            }




        }
    }
    IEnumerator Snap()
    {
        yield return new WaitForSecondsRealtime(0);
        transform.position = rightPosition;  // Bewege das Puzzleteil auf die Zielposition

        GetComponent<NearInteractionGrabbable>().enabled = false;
        GetComponent<ObjectManipulator>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
        GameManager.counter();

    }
}