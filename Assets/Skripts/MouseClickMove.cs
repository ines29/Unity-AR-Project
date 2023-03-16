using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    private int neededCubes = 15;
    [SerializeField]
    private Dictionary<string, Vector3> originalPositions = new Dictionary<string, Vector3>();
    private Dictionary<string, Material> originalMaterials = new Dictionary<string, Material>();
    public Material greenMaterial;
    private bool isSaved = false;
    private int count=1;
    public float confettisDelay = 1.0f;
    public GameObject youWon;
    public GameObject confetti;

    public TMPro.TextMeshPro text;
    public GameObject panel;





    // Start is called before the first frame update
    void Start()
    {

        ////Ich habe ein Dictonary erstellt, hier werden alle urspr�nglichen Positionen der einzelnen W�rfel im Spiel gespeichert
        /*originalPositions.Add("Cube1", new Vector3(-3, 5, 12));
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
        originalPositions.Add("EmptyCube", new Vector3(3, -1, 12));*/
        // speichert auch die urspr�nglichen Materialien der einzelnen W�rfel
       
        
        ShuffleCubes();
       
    }
    void Awake()
    {
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        
        //win[0].SetActive(false);

        foreach (GameObject cube in cubes)
        {
            //Debug.Log("is filled " + originalPositions.ContainsKey(cube.name));
            if (!originalPositions.ContainsKey(cube.name))
            {
                originalMaterials.Add(cube.name, cube.GetComponent<Renderer>().material);
                originalPositions.Add(cube.name, cube.transform.position);
               // Debug.Log(cube.name + cube.transform.position);
            }
            
        }
        StartCoroutine(twelve());


    }

        //// Update is called once per frame
        //Die Methode Update() wird einmal pro Frame aufgerufen und pr�ft,
        //ob sich ein W�rfel gerade bewegt. Ist dies der Fall, bewegt das Skript den W�rfel in Richtung seiner Zielposition
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
    //ob ein W�rfel anklickbar ist. Wenn sich die Maus �ber einem leeren W�rfel befindet, setzt OnTriggerStay() die Variable isClickable auf true und speichert die Position des leeren W�rfels in targetPosition.
    //Wenn die Maus den leeren W�rfel verl�sst, setzt OnTriggerExit() isClickable auf false.

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

    //Wenn der Spieler auf einen klickbaren W�rfel klickt, wird OnMouseDown() aufgerufen.
    //Wenn der angeklickte W�rfel klickbar ist,
    //setzt diese Methode tempTargetPosition auf targetPosition,
    //newPosForEmptyCube auf die Position des angeklickten W�rfels und isMoving auf true.
    public void ButtonPressed ()
    {
        
        if (isClickable)
        {
            tempTargetPosition = targetPosition;
            newPosForEmptyCube = transform.position;
            isMoving = true;
            //SwapCubes();
        }
    }

    //Die Methode SwapCubes() wird w�hrend der Methode Update() aufgerufen,
    //wenn sich ein W�rfel bewegt. Diese Methode bewegt den angeklickten W�rfel in Richtung seiner Zielposition und �berpr�ft,
    //ob er sein Ziel erreicht hat. Wenn der W�rfel sein Ziel erreicht hat,
    //setzt die Methode isMoving auf false, �ndert das Material des W�rfels auf gr�n,
    //wenn er sich in der richtigen Position befindet, setzt die Position des leeren W�rfels
    //auf die Position des angeklickten W�rfels und pr�ft, ob sich alle W�rfel in ihrer urspr�nglichen
    //Position befinden. Wenn sich alle W�rfel in ihrer urspr�nglichen Position befinden, ist das R�tsel gel�st.

    void SwapCubes()
    {
       
        transform.position = Vector3.MoveTowards(transform.position, tempTargetPosition, step);
        //Debug.Log("new Position"+transform.position*1000);
        //wird die resultierende Position mit Mathf.Round() auf zwei Dezimalstellen gerundet und mit 100f multipliziert, um den Float-Typ zu erhalten.
        transform.position = tempTargetPosition;

        // // Pr�fen, ob die Position des W�rfels und der Kugel ungef�hr gleich sind
        //Debug.Log(Vector3.Distance(transform.position, tempTargetPosition));
        if (Vector3.Distance(transform.position, tempTargetPosition) < 0.007f)
        {
            //Debug.Log("swap");
            isMoving = false;

            // Pr�fen, ob der W�rfel in seine urspr�ngliche Position verschoben wurde
           // Debug.Log(originalPositions[gameObject.name]);
            if (transform.position == originalPositions[gameObject.name])
            {
                //  das Material auf gr�n setzen, wenn es sich an der richtigen Stelle befindet
                GetComponent<Renderer>().material = greenMaterial;
            }
            else
            {
                //das Material auf sein urspr�ngliches Material zur�ckstellen, wenn es nicht in der richtigen Position ist
                GetComponent<Renderer>().material = originalMaterials[gameObject.name];
            }

            // Setzen die Position des leeren W�rfels auf die Position des verschobenen W�rfels
            emptyCube.transform.position = newPosForEmptyCube;
            //Der Code rundet die Position des Spielobjekts "emptyCube" auf zwei Dezimalstellen und setzt die gerundete Position als neue Position des Objekts
            //Jeder der drei Werte wird dann mit 100 multipliziert,
            //um eine Genauigkeit von zwei Dezimalstellen zu erreichen. Dann wird jeder Wert mit der Mathf.Round-Funktion gerundet
            //und durch 100f dividiert, um auf zwei Dezimalstellen genau zu bleiben.
            //Schlie�lich wird eine neue Vector3 erstellt, indem die gerundeten x-, y- und z-Werte in den entsprechenden Feldern der Vector3 gespeichert werden,
            //und diese neue Vector3 wird als die neue Position des Objekts "emptyCube" gesetzt.
            //emptyCube.transform.position = new Vector3(Mathf.Round(emptyCube.transform.position.x * 10000f) / 10000f, Mathf.Round(emptyCube.transform.position.y * 10000f) / 10000f, Mathf.Round(emptyCube.transform.position.z * 10000f) / 10000f);
            //Debug.Log("gerundet");
            
            // �berpr�fen ob alle Cubes in der urpsr�nglichen Position sind
            bool allCubesAreInPlace = true;
            int correctCubes = 0;
            foreach (KeyValuePair<string, Vector3> pair in originalPositions)
            {
                Debug.Log("win calculation");
                if (pair.Key != "EmptyCube")
                {
                    GameObject cube = GameObject.Find(pair.Key);
                    Debug.Log(cube.transform.position*100);
                    Debug.Log(pair.Value * 100);
                    if (cube.transform.position != pair.Value && correctCubes<3)
                    {
                        allCubesAreInPlace = false;
                        //break;
                    }
                    else {
                        Debug.Log("correct");
                        correctCubes++;

                    }
                }
                

            }
            if (correctCubes >= neededCubes) {
                allCubesAreInPlace = true;
            }

            //// Wenn alle W�rfel an ihrer urspr�nglichen Position sind, wird eine Meldung auf der Konsole ausgegeben
            if (allCubesAreInPlace)
            {
                Debug.Log("You win!");
                StartCoroutine(EndGame());
                PlayerProgress.wonSchiebepuzzle = true;
            }
        }
    }

    void ShuffleCubes()
    {
        //eine Liste von Cube-Objekten erstellen
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
       
        for(int i = 0; i < cubes.Length; i++)
        {
            /*Debug.Log(originalPositions.ContainsKey(cubes[i].name));
            if (!originalPositions.ContainsKey(cubes[i].name)) {
                originalPositions.Add(cubes[i].name, cubes[i].transform.position);
                Debug.Log("original "+cubes[i].name + cubes[i].transform.position*1000);
            }*/
            //cubes[i].transform.position = new Vector3(Mathf.Round(cubes[i].transform.position.x*1000)/1000, Mathf.Round(cubes[i].transform.position.y*1000)/1000, Mathf.Round(cubes[i].transform.position.z*1000)/1000);
        }
        

        //eine zuf�llige Permutation von Indizes von Cube-Objekten erstellen
        //Ein Array von Integer-Indizes wird erstellt, das die L�nge der Cube-Liste hat
        //und eine zuf�llige Permutation der Indizes darstellt
        //Die Methode Enumerable.Range(start, count) ist eine LINQ-Erweiterungsmethode,
        //die eine Folge von Ganzzahlen erzeugt, die mit der angegebenen Start-Ganzzahl
        //beginnt und die Anzahl der Elemente enth�lt
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

        //die Positionen der Cube-Objekte entsprechend der zuf�lligen Permutation tauschen
        //Die Schleife durchl�uft alle Indizes in der zuf�lligen Permutation und
        //tauscht die Positionen der Cubes an diesen Indizes
        for (int i = 0; i < indices.Length; i++)
        {
            int j = indices[i];
            Vector3 temp = cubes[j].transform.position;
            
            cubes[j].transform.position = cubes[i].transform.position;
            cubes[i].transform.position = temp;
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

       IEnumerator twelve()
    {
        yield return new WaitForSecondsRealtime(120);
        neededCubes = 12;
        panel.SetActive(true);
        text.text = "Jetzt m�ssen nur noch 12 W�rfel auf der richtigen Position sein";
        StartCoroutine(eight());
    }

    IEnumerator eight()
    {
        yield return new WaitForSecondsRealtime(60);
        neededCubes = 8;
        panel.SetActive(true);
        text.text = "Jetzt m�ssen nur noch 8 W�rfel auf der richtigen Position sein";
        StartCoroutine(four());
    }
    IEnumerator four()
    {
        yield return new WaitForSecondsRealtime(60);
        panel.SetActive(true);
        text.text = "Jetzt m�ssen nur noch 4 W�rfel auf der richtigen Position sein";
        neededCubes = 4;
    }
}