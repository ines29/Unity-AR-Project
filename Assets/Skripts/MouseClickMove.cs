using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MouseClickMove : MonoBehaviour
{
    [SerializeField]
    private bool isClickable;
    private bool isMoving;
    private Vector3 targetPosition;
    private Vector3 newPosForEmptyCube;
    private Vector3 tempTargetPosition;
    private GameObject emptyCube;
    private float step = 0f;
    private float speed = 10f;
    [SerializeField]
    private Dictionary<string, Vector3> originalPositions = new Dictionary<string, Vector3>();
    private Dictionary<string, Material> originalMaterials = new Dictionary<string, Material>();
    public Material greenMaterial;




    // Start is called before the first frame update
    void Start()
    {
       
        ////Ich habe ein Dictonary erstellt, hier werden alle ursprünglichen Positionen der einzelnen Würfel im Spiel gespeichert
        originalPositions.Add("Cube1", new Vector3(-3, 5, 12));
        originalPositions.Add("Cube2", new Vector3(-1, 5, 12));
        originalPositions.Add("Cube3", new Vector3(1, 5, 12));
        originalPositions.Add("Cube4", new Vector3(3, 5, 12));
        originalPositions.Add("Cube5", new Vector3(-3, 3, 12));
        originalPositions.Add("Cube6", new Vector3(-1, 3, 12));
        originalPositions.Add("Cube7", new Vector3(1, 3, 12));
        originalPositions.Add("Cube8", new Vector3(3, 3, 12));
        originalPositions.Add("Cube9", new Vector3(-3, 1, 12));
        originalPositions.Add("Cube10", new Vector3(-1, 1, 12));
        originalPositions.Add("Cube11", new Vector3(1, 1, 12));
        originalPositions.Add("Cube12", new Vector3(3, 1, 12));
        originalPositions.Add("Cube13", new Vector3(-3,-1, 12));
        originalPositions.Add("Cube14", new Vector3(-1, -1, 12));
        originalPositions.Add("Cube15", new Vector3(1,-1, 12));
        originalPositions.Add("EmptyCube", new Vector3(3, -1, 12));
        // speichert auch die ursprünglichen Materialien der einzelnen Würfel
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        foreach (GameObject cube in cubes)
        {
            originalMaterials.Add(cube.name, cube.GetComponent<Renderer>().material);
        }
        
        ShuffleCubes();
       
    }

    //// Update is called once per frame
    //Die Methode Update() wird einmal pro Frame aufgerufen und prüft,
    //ob sich ein Würfel gerade bewegt. Ist dies der Fall, bewegt das Skript den Würfel in Richtung seiner Zielposition
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            step = speed * Time.deltaTime; // calculate distance to move
            SwapCubes();
        }
    }

    //Die Methoden OnTriggerStay() und OnTriggerExit() werden verwendet, um festzustellen,
    //ob ein Würfel anklickbar ist. Wenn sich die Maus über einem leeren Würfel befindet, setzt OnTriggerStay() die Variable isClickable auf true und speichert die Position des leeren Würfels in targetPosition.
    //Wenn die Maus den leeren Würfel verlässt, setzt OnTriggerExit() isClickable auf false.

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("EmptyCube"))
        {
            isClickable = true;
            targetPosition = other.transform.position;
            //newPosForEmptyCube = transform.position;
            emptyCube = other.gameObject;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EmptyCube"))
        {
            isClickable = false;
        }
    }

    //Wenn der Spieler auf einen klickbaren Würfel klickt, wird OnMouseDown() aufgerufen.
    //Wenn der angeklickte Würfel klickbar ist,
    //setzt diese Methode tempTargetPosition auf targetPosition,
    //newPosForEmptyCube auf die Position des angeklickten Würfels und isMoving auf true.
    void OnMouseDown()
    {
        if (isClickable)
        {
            tempTargetPosition = targetPosition;
            newPosForEmptyCube = transform.position;
            isMoving = true;
            //SwapCubes();
        }
    }

    //Die Methode SwapCubes() wird während der Methode Update() aufgerufen,
    //wenn sich ein Würfel bewegt. Diese Methode bewegt den angeklickten Würfel in Richtung seiner Zielposition und überprüft,
    //ob er sein Ziel erreicht hat. Wenn der Würfel sein Ziel erreicht hat,
    //setzt die Methode isMoving auf false, ändert das Material des Würfels auf grün,
    //wenn er sich in der richtigen Position befindet, setzt die Position des leeren Würfels
    //auf die Position des angeklickten Würfels und prüft, ob sich alle Würfel in ihrer ursprünglichen
    //Position befinden. Wenn sich alle Würfel in ihrer ursprünglichen Position befinden, ist das Rätsel gelöst.

    void SwapCubes()
    {
       
        transform.position = Vector3.MoveTowards(transform.position, tempTargetPosition, step);
        //Debug.Log(transform.position);
        //wird die resultierende Position mit Mathf.Round() auf zwei Dezimalstellen gerundet und mit 100f multipliziert, um den Float-Typ zu erhalten.
        transform.position = new Vector3(Mathf.Round(transform.position.x * 100f) / 100f, Mathf.Round(transform.position.y * 100f) / 100f, Mathf.Round(transform.position.z * 100f) / 100f);

        // // Prüfen, ob die Position des Würfels und der Kugel ungefähr gleich sind
        Debug.Log(0.001f);
        if (Vector3.Distance(transform.position, tempTargetPosition) < 0.01f)
        {
            Debug.Log("swap");
            isMoving = false;

            // Prüfen, ob der Würfel in seine ursprüngliche Position verschoben wurde
            if (transform.position == originalPositions[gameObject.name])
            {
                //  das Material auf grün setzen, wenn es sich an der richtigen Stelle befindet
                GetComponent<Renderer>().material = greenMaterial;
            }
            else
            {
                //das Material auf sein ursprüngliches Material zurückstellen, wenn es nicht in der richtigen Position ist
                GetComponent<Renderer>().material = originalMaterials[gameObject.name];
            }

            // Setzen die Position des leeren Würfels auf die Position des verschobenen Würfels
            emptyCube.transform.position = newPosForEmptyCube;
            //Der Code rundet die Position des Spielobjekts "emptyCube" auf zwei Dezimalstellen und setzt die gerundete Position als neue Position des Objekts
            //Jeder der drei Werte wird dann mit 100 multipliziert,
            //um eine Genauigkeit von zwei Dezimalstellen zu erreichen. Dann wird jeder Wert mit der Mathf.Round-Funktion gerundet
            //und durch 100f dividiert, um auf zwei Dezimalstellen genau zu bleiben.
            //Schließlich wird eine neue Vector3 erstellt, indem die gerundeten x-, y- und z-Werte in den entsprechenden Feldern der Vector3 gespeichert werden,
            //und diese neue Vector3 wird als die neue Position des Objekts "emptyCube" gesetzt.
            emptyCube.transform.position = new Vector3(Mathf.Round(emptyCube.transform.position.x * 10000f) / 10000f, Mathf.Round(emptyCube.transform.position.y * 10000f) / 10000f, Mathf.Round(emptyCube.transform.position.z * 10000f) / 10000f);
            Debug.Log("gerundet");
            
            // Überprüfen ob alle Cubes in der urpsrünglichen Position sind
            bool allCubesAreInPlace = true;
            foreach (KeyValuePair<string, Vector3> pair in originalPositions)
            {
                if (pair.Key != "EmptyCube")
                {
                    GameObject cube = GameObject.Find(pair.Key);
                    if (cube.transform.position != pair.Value)
                    {
                        allCubesAreInPlace = false;
                        break;
                    }
                }
            }

            //// Wenn alle Würfel an ihrer ursprünglichen Position sind, wird eine Meldung auf der Konsole ausgegeben
            if (allCubesAreInPlace)
            {
                Debug.Log("You win!");
            }
        }
    }

    void ShuffleCubes()
    {
        //eine Liste von Cube-Objekten erstellen
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        
        for(int i = 0; i < cubes.Length; i++)
        {
            cubes[i].transform.position = new Vector3(Mathf.Round(cubes[i].transform.position.x*1000)/1000, Mathf.Round(cubes[i].transform.position.y*1000)/1000, Mathf.Round(cubes[i].transform.position.z*1000)/1000);
        }


        //eine zufällige Permutation von Indizes von Cube-Objekten erstellen
        //Ein Array von Integer-Indizes wird erstellt, das die Länge der Cube-Liste hat
        //und eine zufällige Permutation der Indizes darstellt
        //Die Methode Enumerable.Range(start, count) ist eine LINQ-Erweiterungsmethode,
        //die eine Folge von Ganzzahlen erzeugt, die mit der angegebenen Start-Ganzzahl
        //beginnt und die Anzahl der Elemente enthält
        int[] indices = Enumerable.Range(0, cubes.Length).ToArray();
        System.Random rng = new System.Random();
        int n = indices.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int temp = indices[k];
            indices[k] = indices[n];
            indices[n] = temp;
        }

        //die Positionen der Cube-Objekte entsprechend der zufälligen Permutation tauschen
        //Die Schleife durchläuft alle Indizes in der zufälligen Permutation und
        //tauscht die Positionen der Cubes an diesen Indizes
        for (int i = 0; i < indices.Length; i++)
        {
            int j = indices[i];
            Vector3 temp = cubes[j].transform.position;
            //Debug.Log(temp);
            cubes[j].transform.position = cubes[i].transform.position;
            cubes[i].transform.position = temp;
        }

    }
}