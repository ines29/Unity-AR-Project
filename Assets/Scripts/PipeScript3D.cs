using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript3D : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };
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

        int rand = Random.Range(0, rotations.Length);
        transform.Rotate(new Vector3(rotations[rand],0,0));
       

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
