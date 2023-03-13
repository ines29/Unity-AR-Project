using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnapToPos : MonoBehaviour
{
    [SerializeField]
    private GameObject chessBoard;
    private List<Transform> snaps = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] fields = GameObject.FindGameObjectsWithTag("Field");
        foreach(GameObject field in fields) {
            snaps.Add(field.GetComponent<Transform>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        float smallestDistance = float.MaxValue;
        Vector3 bestPos = transform.position;
        foreach (Transform snap in snaps)
        {
            if (Vector3.Distance(snap.position, transform.position) < smallestDistance)
            {
                //transform.position = snap.position;
                smallestDistance = Vector3.Distance(snap.position, transform.position);
                bestPos = snap.position;
            }
        }
        transform.position = bestPos;
    }
}
