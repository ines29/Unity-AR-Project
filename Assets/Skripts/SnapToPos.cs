using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnapToPos : MonoBehaviour
{
    private Dictionary<GameObject, (int, int)> fields = new Dictionary<GameObject, (int, int)>();
    private List<Transform> snaps = new List<Transform>();
    private GameObject[] fieldsObjects;
    [SerializeField]
    private int startCol;
    [SerializeField]
    private int startRow;
    [SerializeField]
    private GameObject chessBoard;
    private (int, int) currentPos;
    
    // Start is called before the first frame update
    void Start()
    {
        currentPos = (startCol, startRow);
        fieldsObjects = GameObject.FindGameObjectsWithTag("Field");
        foreach(GameObject fieldObject in fieldsObjects) {
            fields.Add(fieldObject, GetPos(fieldObject));
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
        transform.position = bestSnap.position;
        SetCurrentPositionFromTransform(bestSnap);
    }

    public (int,int) GetStartPosition()
    {
        return (startCol, startRow);
    }

    void SetCurrentPositionFromTransform(Transform trans)
    {
        GameObject gO = fields.Keys.FirstOrDefault(obj => obj.GetComponent<Transform>().Equals(trans));
        if (gO != null)
        {
            print("Updating current Pos of " + gameObject.name + " from " + currentPos);
            fields.TryGetValue(gO, out currentPos);
            print(gameObject.name + " to " + currentPos);
        }
    }

    internal (int, int) GetCurrentPos()
    {
        return currentPos;
    }

    public void UpdatePositionWithBoard()
    {
        chessBoard.TryGetComponent<TrackPieces>(out TrackPieces ts);
        ts.UpdatePos(gameObject, currentPos);
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
    void AddPosToSnapsCheckPieces(int col, int row)
    {
        GameObject pap = PieceAtPos(col, row);
        if (pap != null)
        {
            if (!pap.CompareTag(tag))
            {
                AddPosToSnaps(col, row); //able to capture => !
            }
        }
        else
        {
            AddPosToSnaps(col, row);
        }
    }
    void AddPosToSnapsCheckPiecesUntil(int col, int row)
    {
        GameObject pap = PieceUntilPos(col, row);
        if (pap != null)
        {
            if (!pap.CompareTag(tag) && pap.Equals(PieceAtPos(col,row)))
            {
                AddPosToSnaps(col, row); //able to capture => !
            }
        }
        else
        {
            AddPosToSnaps(col, row);
        }
    }

    void AddPosToSnaps(int col, int row)
    {
        GameObject gO = fields.Keys.First(s => fields[s].Equals((col, row)));
        snaps.Add(gO.GetComponent<Transform>());
    }

    GameObject PieceUntilPos(int col, int row)
    {
        int curCol = currentPos.Item1;
        int curRow = currentPos.Item2;
        bool colUp;
        bool rowUp;
        int endCol = col;
        int endRow = row;

        if (col == currentPos.Item1 && row == currentPos.Item2)
        {
            return null;
        } else if (col == currentPos.Item1)
        {
            colUp = true;
            if (row < currentPos.Item2)
            {
                curRow--;
                rowUp = false;
            } else
            {
                curRow++;
                rowUp = true;
            }
        } else if (row == currentPos.Item2)
        {
            rowUp = true;
            if (col < currentPos.Item1)
            {
                curCol--;
                colUp = false;
            } else
            {
                curCol++;
                colUp = true;
            }
        } else if (col < currentPos.Item1 && row < currentPos.Item2)
        {
            curCol--;
            curRow--;
            colUp = rowUp = false;
        } else if (col > currentPos.Item1 && row > currentPos.Item2)
        {
            curCol++;
            curRow++;
            colUp = rowUp = true;
        } else if (col > currentPos.Item1 && row < currentPos.Item2)
        {
            curCol++;
            curRow--;
            colUp = true;
            rowUp = false;
        } else //if(col < currentPos.Item1 && row >= currentPos.Item2)
        {
            curCol--;
            curRow++;
            colUp = false;
            rowUp = true;
        }
        int ctr = 10;
        while (ctr > 0)
        {
            if (PieceAtPos(curCol, curRow) != null)
                return PieceAtPos(curCol, curRow);
            if(curCol == endCol && curRow == endRow)
                break;
            if (curCol != endCol)
            {
                if (colUp)
                    curCol++;
                else
                    curCol--;
            }
            if (curRow != endRow)
            {
                if (rowUp)
                    curRow++;
                else
                    curRow--;
            }
            ctr--;
        }
        if (ctr == 0)
            print("Opps, something went wrong, infinite loop avoided");
        return null;
    }

    GameObject PieceAtPos(int col, int row)
    {
        //GameObject gO = fields.Keys.FirstOrDefault(s => fields[s].Equals((col, row)));
        //print("Found empty object " + gO.name + " with position "+ gO.GetComponent<Transform>().position);
        //GameObject whitePiece = GameObject.FindGameObjectsWithTag("White").FirstOrDefault(s => (s.GetComponent<Transform>().position - gO.GetComponent<Transform>().position).sqrMagnitude <= minDistance);
        //GameObject blackPiece = GameObject.FindGameObjectsWithTag("Black").FirstOrDefault(s => (s.GetComponent<Transform>().position - gO.GetComponent<Transform>().position).sqrMagnitude <= minDistance);
        //if (whitePiece != null && blackPiece != null)
        //{
        //    if (whitePiece != null)
        //        print("Found white piece in range: " + whitePiece.name + " with position " + whitePiece.GetComponent<Transform>().position);
        //    if (blackPiece != null)
        //        print("Found black piece in range: " + blackPiece.name + " with position " + blackPiece.GetComponent<Transform>().position);
        //    return true;
        //}
        //return false;
        chessBoard.TryGetComponent<TrackPieces>(out TrackPieces ts);
        return ts.PieceAtPos((col, row));
    }

    public void PawnMoves()
    {
        snaps.Clear();
        AddPosToSnaps(currentPos.Item1, currentPos.Item2);
        if (CompareTag("White") && currentPos.Item2 + 1 <= 8) {
            GameObject pap = PieceAtPos(currentPos.Item1, currentPos.Item2 + 1);
            if (pap == null) 
                AddPosToSnaps(currentPos.Item1, currentPos.Item2 + 1);
            pap = PieceAtPos(currentPos.Item1 - 1, currentPos.Item2 + 1);
            if (pap != null)
            {
                if (!pap.CompareTag(tag))
                {
                    AddPosToSnaps(currentPos.Item1 - 1, currentPos.Item2 + 1); //able to capture => !
                }
            }
            pap = PieceAtPos(currentPos.Item1 + 1, currentPos.Item2 + 1);
            if (pap != null)
            {
                if (!pap.CompareTag(tag))
                {
                    AddPosToSnaps(currentPos.Item1 + 1, currentPos.Item2 + 1); //able to capture => !
                }
            }
        }
        if (CompareTag("Black") && currentPos.Item2 - 1 > 0)
        {
            GameObject pap = PieceAtPos(currentPos.Item1, currentPos.Item2 - 1);
            if (pap == null)
                AddPosToSnaps(currentPos.Item1, currentPos.Item2 - 1);
            pap = PieceAtPos(currentPos.Item1 - 1, currentPos.Item2 - 1);
            if (pap != null)
            {
                if (!pap.CompareTag(tag))
                {
                    AddPosToSnaps(currentPos.Item1 - 1, currentPos.Item2 - 1); //able to capture => !
                }
            }
            pap = PieceAtPos(currentPos.Item1 + 1, currentPos.Item2 - 1);
            if (pap != null)
            {
                if (!pap.CompareTag(tag))
                {
                    AddPosToSnaps(currentPos.Item1 + 1, currentPos.Item2 - 1); //able to capture => !
                }
            }
        }
    }
    public void RookMoves()
    {
        snaps.Clear();
        AddPosToSnaps(currentPos.Item1, currentPos.Item2);
        for(int i = 1; i < 8; i++)
        {
            if (currentPos.Item1 + i <= 8)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1 + i, currentPos.Item2);

            if (currentPos.Item2 + i <= 8)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1, currentPos.Item2 + i);

            if (currentPos.Item1 - i > 0)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1 - i, currentPos.Item2);

            if (currentPos.Item2 - i > 0)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1, currentPos.Item2 - i);
        }
    }
    public void KnightMoves()
    {
        snaps.Clear();
        AddPosToSnaps(currentPos.Item1, currentPos.Item2);
        if (currentPos.Item1 + 2 <= 8 && currentPos.Item2 - 1 > 0)
            AddPosToSnapsCheckPieces(currentPos.Item1 + 2, currentPos.Item2 - 1);

        if (currentPos.Item1 + 2 <= 8 && currentPos.Item2 + 1 <= 8)
            AddPosToSnapsCheckPieces(currentPos.Item1 + 2, currentPos.Item2 + 1);

        if (currentPos.Item1 + 1 <= 8 && currentPos.Item2 + 2 <= 8)
            AddPosToSnapsCheckPieces(currentPos.Item1 + 1, currentPos.Item2 + 2);

        if (currentPos.Item1 - 1 > 0 && currentPos.Item2 + 2 <= 8)
            AddPosToSnapsCheckPieces(currentPos.Item1 - 1, currentPos.Item2 + 2);

        if (currentPos.Item1 - 2 > 0 && currentPos.Item2 + 1 <= 8)
            AddPosToSnapsCheckPieces(currentPos.Item1 - 2, currentPos.Item2 + 1);

        if (currentPos.Item1 - 2 > 0 && currentPos.Item2 - 1 > 0)
            AddPosToSnapsCheckPieces(currentPos.Item1 - 2, currentPos.Item2 - 1);

        if (currentPos.Item1 - 1 > 0 && currentPos.Item2 - 2 > 0)
            AddPosToSnapsCheckPieces(currentPos.Item1 - 1, currentPos.Item2 - 2);

        if (currentPos.Item1 + 1 <= 8 && currentPos.Item2 - 2 > 0)
            AddPosToSnapsCheckPieces(currentPos.Item1 + 1, currentPos.Item2 - 2);
    }
    public void BishopMoves()
    {
        snaps.Clear();
        AddPosToSnaps(currentPos.Item1, currentPos.Item2);
        for (int i = 1; i < 8; i++)
        {
            if (currentPos.Item1 + i <= 8 && currentPos.Item2 + i <= 8)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1 + i, currentPos.Item2 + i);

            if (currentPos.Item1 + i <= 8 && currentPos.Item2 - i > 0)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1 + i, currentPos.Item2 - i);

            if (currentPos.Item1 - i > 0 && currentPos.Item2 + i <= 8)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1 - i, currentPos.Item2 + i);

            if (currentPos.Item1 - i > 0 && currentPos.Item2 - i > 0)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1 - i, currentPos.Item2 - i);
        }
    }
    public void QueenMoves()
    {
        snaps.Clear();
        AddPosToSnaps(currentPos.Item1, currentPos.Item2);
        for (int i = 1; i < 8; i++)
        {
            if (currentPos.Item1 + i <= 8)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1 + i, currentPos.Item2);

            if (currentPos.Item2 + i <= 8)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1, currentPos.Item2 + i);

            if (currentPos.Item1 - i > 0)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1 - i, currentPos.Item2);

            if (currentPos.Item2 - i > 0)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1, currentPos.Item2 - i);

            if (currentPos.Item1 + i <= 8 && currentPos.Item2 + i <= 8)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1 + i, currentPos.Item2 + i);

            if (currentPos.Item1 + i <= 8 && currentPos.Item2 - i > 0)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1 + i, currentPos.Item2 - i);

            if (currentPos.Item1 - i > 0 && currentPos.Item2 + i <= 8)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1 - i, currentPos.Item2 + i);

            if (currentPos.Item1 - i > 0 && currentPos.Item2 - i > 0)
                AddPosToSnapsCheckPiecesUntil(currentPos.Item1 - i, currentPos.Item2 - i);
        }
    }
    public void KingMoves()
    {
        snaps.Clear();
        AddPosToSnaps(currentPos.Item1, currentPos.Item2);
        if (currentPos.Item1 + 1 <= 8)
            AddPosToSnapsCheckPieces(currentPos.Item1 + 1, currentPos.Item2);

        if (currentPos.Item1 + 1 <= 8 && currentPos.Item2 + 1 <= 8)
            AddPosToSnapsCheckPieces(currentPos.Item1 + 1, currentPos.Item2 + 1);

        if (currentPos.Item2 + 1 <= 8)
            AddPosToSnapsCheckPieces(currentPos.Item1, currentPos.Item2 + 1);

        if (currentPos.Item1 - 1 > 0 && currentPos.Item2 + 1 <= 8)
            AddPosToSnapsCheckPieces(currentPos.Item1 - 1, currentPos.Item2 + 1);

        if (currentPos.Item1 - 1 > 0)
            AddPosToSnapsCheckPieces(currentPos.Item1 - 1, currentPos.Item2);

        if (currentPos.Item1 - 1 > 0 && currentPos.Item2 - 1 > 0)
            AddPosToSnapsCheckPieces(currentPos.Item1 - 1, currentPos.Item2 - 1);

        if (currentPos.Item2 - 1 > 0)
            AddPosToSnapsCheckPieces(currentPos.Item1, currentPos.Item2 - 1);

        if (currentPos.Item1 + 1 <= 8 && currentPos.Item2 - 1 > 0)
            AddPosToSnapsCheckPieces(currentPos.Item1 + 1, currentPos.Item2 - 1);
    }
}
