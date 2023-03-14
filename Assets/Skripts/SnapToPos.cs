using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnapToPos : MonoBehaviour
{
    private Dictionary<Transform, (int, int)> fields = new Dictionary<Transform, (int, int)>();
    private List<Transform> snaps = new List<Transform>();

    [SerializeField]
    private int startCol;
    [SerializeField]
    private int startRow;
    private (int, int) currentPos;
    
    // Start is called before the first frame update
    void Start()
    {
        currentPos = (startCol, startRow);
        GameObject[] fieldsObjects = GameObject.FindGameObjectsWithTag("Field");
        foreach(GameObject fieldObject in fieldsObjects) {
            fields.Add(fieldObject.GetComponent<Transform>(), GetPos(fieldObject));
        }
        AddPosToSnaps(startCol, startRow);
    }

    // Update is called once per frame
    void Update()
    {
        float smallestDistance = float.MaxValue;
        Transform bestSnap = transform;
        foreach (Transform snap in snaps)
        {
            if (Vector3.Distance(snap.position, transform.position) < smallestDistance)
            {
                //transform.position = snap.position;
                smallestDistance = Vector3.Distance(snap.position, transform.position);
                bestSnap = snap;
            }
        }
        fields.TryGetValue(bestSnap, out currentPos);
        transform.position = bestSnap.position;
    }

    (int,int) GetPos(GameObject field)
    {
        string name = field.name;
        int col = ConvertColumnToRow(name[0]);
        int row = Int32.Parse(name[3].ToString());
        if(col == -1)
        {
            throw new Exception("Field name seems to be wrong, Error with Column in: " + name);
        }
        if (row <= 0 || 9 <= row)
        {
            throw new Exception("Field name seems to be wrong, Error with Row in: " + row + " from " + name);
        }
        return (col, row);
    }

    int ConvertColumnToRow(char col) => col switch
    {
        'a' => 1,
        'b' => 2,
        'c' => 3,
        'd' => 4,
        'e' => 5,
        'f' => 6,
        'g' => 7,
        'h' => 8,
        _ => -1,
    };

    void AddPosToSnaps(int col, int row)
    {
        snaps.Add(fields.Keys.First(s => fields[s].Equals((col, row))));
    }

    public void PawnMoves()
    {
        snaps.Clear();
        AddPosToSnaps(currentPos.Item1, currentPos.Item2);
        if(currentPos.Item2 + 1 <= 8)
            AddPosToSnaps(currentPos.Item1, currentPos.Item2 + 1);
    }
    public void RookMoves()
    {
        snaps.Clear();
        AddPosToSnaps(currentPos.Item1, currentPos.Item2);
        for(int i = 1; i < 8; i++)
        {
            if (currentPos.Item1 + i <= 8)
                AddPosToSnaps(currentPos.Item1 + i, currentPos.Item2);

            if (currentPos.Item2 + i <= 8)
                AddPosToSnaps(currentPos.Item1, currentPos.Item2 + i);

            if (currentPos.Item1 - i > 0)
                AddPosToSnaps(currentPos.Item1 - i, currentPos.Item2);

            if (currentPos.Item2 - i > 0)
                AddPosToSnaps(currentPos.Item1, currentPos.Item2 - i);
        }
    }
    public void KnightMoves()
    {
        snaps.Clear();
        AddPosToSnaps(currentPos.Item1, currentPos.Item2);
        if (currentPos.Item1 + 2 <= 8 && currentPos.Item2 - 1 > 0)
            AddPosToSnaps(currentPos.Item1 + 2, currentPos.Item2 - 1);

        if (currentPos.Item1 + 2 <= 8 && currentPos.Item2 + 1 <= 8)
            AddPosToSnaps(currentPos.Item1 + 2, currentPos.Item2 + 1);

        if (currentPos.Item1 + 1 <= 8 && currentPos.Item2 + 2 <= 8)
            AddPosToSnaps(currentPos.Item1 + 1, currentPos.Item2 + 2);

        if (currentPos.Item1 - 1 > 0 && currentPos.Item2 + 2 <= 8)
            AddPosToSnaps(currentPos.Item1 - 1, currentPos.Item2 + 2);

        if (currentPos.Item1 - 2 > 0 && currentPos.Item2 + 1 <= 8)
            AddPosToSnaps(currentPos.Item1 - 2, currentPos.Item2 + 1);

        if (currentPos.Item1 - 2 > 0 && currentPos.Item2 - 1 > 0)
            AddPosToSnaps(currentPos.Item1 - 2, currentPos.Item2 - 1);

        if (currentPos.Item1 - 1 > 0 && currentPos.Item2 - 2 > 0)
            AddPosToSnaps(currentPos.Item1 - 1, currentPos.Item2 - 2);

        if (currentPos.Item1 + 1 <= 8 && currentPos.Item2 - 2 > 0)
            AddPosToSnaps(currentPos.Item1 + 1, currentPos.Item2 - 2);
    }
    public void BishopMoves()
    {
        snaps.Clear();
        AddPosToSnaps(currentPos.Item1, currentPos.Item2);
        for (int i = 1; i < 8; i++)
        {
            if (currentPos.Item1 + i <= 8 && currentPos.Item2 + i <= 8)
                AddPosToSnaps(currentPos.Item1 + i, currentPos.Item2 + i);

            if (currentPos.Item1 + i <= 8 && currentPos.Item2 - i > 0)
                AddPosToSnaps(currentPos.Item1 + i, currentPos.Item2 - i);

            if (currentPos.Item1 - i > 0 && currentPos.Item2 + i <= 8)
                AddPosToSnaps(currentPos.Item1 - i, currentPos.Item2 + i);

            if (currentPos.Item1 - i > 0 && currentPos.Item2 - i > 0)
                AddPosToSnaps(currentPos.Item1 - i, currentPos.Item2 - i);
        }
    }
    public void QueenMoves()
    {
        snaps.Clear();
        AddPosToSnaps(currentPos.Item1, currentPos.Item2);
        for (int i = 1; i < 8; i++)
        {
            if (currentPos.Item1 + i <= 8)
                AddPosToSnaps(currentPos.Item1 + i, currentPos.Item2);

            if (currentPos.Item2 + i <= 8)
                AddPosToSnaps(currentPos.Item1, currentPos.Item2 + i);

            if (currentPos.Item1 - i > 0)
                AddPosToSnaps(currentPos.Item1 - i, currentPos.Item2);

            if (currentPos.Item2 - i > 0)
                AddPosToSnaps(currentPos.Item1, currentPos.Item2 - i);

            if (currentPos.Item1 + i <= 8 && currentPos.Item2 + i <= 8)
                AddPosToSnaps(currentPos.Item1 + i, currentPos.Item2 + i);

            if (currentPos.Item1 + i <= 8 && currentPos.Item2 - i > 0)
                AddPosToSnaps(currentPos.Item1 + i, currentPos.Item2 - i);

            if (currentPos.Item1 - i > 0 && currentPos.Item2 + i <= 8)
                AddPosToSnaps(currentPos.Item1 - i, currentPos.Item2 + i);

            if (currentPos.Item1 - i > 0 && currentPos.Item2 - i > 0)
                AddPosToSnaps(currentPos.Item1 - i, currentPos.Item2 - i);
        }
    }
    public void KingMoves()
    {
        snaps.Clear();
        AddPosToSnaps(currentPos.Item1, currentPos.Item2);
        if (currentPos.Item1 + 1 <= 8)
            AddPosToSnaps(currentPos.Item1 + 1, currentPos.Item2);

        if (currentPos.Item1 + 1 <= 8 && currentPos.Item2 + 1 <= 8)
            AddPosToSnaps(currentPos.Item1 + 1, currentPos.Item2 + 1);

        if (currentPos.Item2 + 1 <= 8)
            AddPosToSnaps(currentPos.Item1, currentPos.Item2 + 1);

        if (currentPos.Item1 - 1 > 0 && currentPos.Item2 + 1 <= 8)
            AddPosToSnaps(currentPos.Item1 - 1, currentPos.Item2 + 1);

        if (currentPos.Item1 - 1 > 0)
            AddPosToSnaps(currentPos.Item1 - 1, currentPos.Item2);

        if (currentPos.Item1 - 1 > 0 && currentPos.Item2 - 1 > 0)
            AddPosToSnaps(currentPos.Item1 - 1, currentPos.Item2 - 1);

        if (currentPos.Item2 - 1 > 0)
            AddPosToSnaps(currentPos.Item1, currentPos.Item2 - 1);

        if (currentPos.Item1 + 1 <= 8 && currentPos.Item2 - 1 > 0)
            AddPosToSnaps(currentPos.Item1 + 1, currentPos.Item2 - 1);
    }
}
