using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript3D : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };
    public float[] correctRotation;
    [SerializeField]
    bool isPlaced = false;
    [SerializeField]
    int connections = 0;

    int PossibleRots = 1;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager3D").GetComponent<GameManager>();
    }
    private void Start()
    {
        PossibleRots = correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        //Debug.Log(Quaternion.identity.x);
        transform.Rotate(new Vector3(rotations[rand],0,0));
        //Debug.Log(Quaternion.identity.x);
        //Debug.Log(transform.eulerAngles.x.ToString()+" random: "+rotations[rand].ToString());
        if (PossibleRots > 1)
        {

            if (transform.eulerAngles.x == correctRotation[0] || transform.eulerAngles.x == correctRotation[1])
            {
                isPlaced = true;
                gameManager.correctMove();
            }
        }
        else
        {
            if (transform.eulerAngles.x == correctRotation[0])
            {
                isPlaced = true;
                gameManager.correctMove();
            }
        }

    }


    private void OnMouseDown()
    {
      //  Debug.Log("Before: " + (transform.eulerAngles.x).ToString());

        transform.Rotate(new Vector3(-90, 0, 0));
      
        //Debug.Log(isPlaced.ToString() + transform.eulerAngles.x);
       // Debug.Log(isPlaced.ToString() + (transform.eulerAngles.x).ToString());

    }
    public void ButtonPressed()
    {
        Debug.Log("Button Pressed");
        transform.Rotate(new Vector3(-90, 0, 0));
       

    }
    private void OnTriggerEnter(Collider other)
    {
        connections++;
        if (connections == 2)
        {
            isPlaced = true;
            gameManager.correctMove();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        connections--;
        if (connections == 1) {
            isPlaced = false;
            gameManager.wrongMove();
        }
        

    }




}
