using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private Vector3 rightPosition; // die korrekte Position, an die das Puzzleteil platziert werden muss
    private Vector3 rightRotation; 
    public bool inRightPosition; // gibt an, ob das Puzzleteil bereits an der korrekten Position ist
    public bool selected; // gibt an, ob das Puzzleteil gerade ausgewählt ist
    public GameObject placeholderPrefab; // das Prefab des Platzhalter-GameObjects
    public GameObject script;

    private List<Transform> snaps = new List<Transform>(); // Liste der Zielpositionen


    private winningScript GameManager;
    

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<winningScript>();

        //"rightPosition" auf die aktuelle Position des Puzzleteils setzen
        rightPosition = transform.position;
        rightRotation = transform.eulerAngles;
        // Setze die Startposition des Puzzleteils auf eine zufällige Position innerhalb eines Bereichs
        transform.position = new Vector3(Random.Range(0.2f, 0.55f),Random.Range(-0.3f, 0.16f), 0.9f);
        Debug.Log(transform.position);

        // Erstelle den Platzhalter-GameObject an der richtigen Position
        GameObject placeholder = Instantiate(placeholderPrefab, rightPosition, Quaternion.identity);
        placeholder.name = gameObject.name + " Placeholder";

       // Debug.Log("Zielpositionen: " + GameObject.FindGameObjectsWithTag("SnapPosition"));
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name+ gameObject.name);
        if (other.name == gameObject.name + " Placeholder")
        {

            if (!inRightPosition)
            {
                inRightPosition = true;  // Markiere das Puzzleteil als an der korrekten Position
                Debug.Log("Match");
                //StartCoroutine(Snap());


                // Prüfe, ob das Puzzleteil sich innerhalb des Radius von der aktuellen Zielposition befindet


            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        inRightPosition = false;
    }
    public void Snap()
    {
        if(inRightPosition)
        {

    
       // yield return new WaitForSecondsRealtime(0);
        transform.position = rightPosition;  // Bewege das Puzzleteil auf die Zielposition
        transform.eulerAngles = rightRotation;

        GetComponent<NearInteractionGrabbable>().enabled = false;
        GetComponent<ObjectManipulator>().enabled = false;
        //GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
        GameManager.counter();
        }

    }
      
    
}
