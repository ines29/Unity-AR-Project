using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPieces : MonoBehaviour
{
    private Dictionary<GameObject, (int, int)> blackPieces = new Dictionary<GameObject, (int, int)>();
    private Dictionary<GameObject, (int, int)> whitePieces = new Dictionary<GameObject, (int, int)>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] blacks = GameObject.FindGameObjectsWithTag("Black");
        foreach (GameObject black in blacks)
        {
            black.TryGetComponent<SnapToPos>(out SnapToPos stpB);
            blackPieces.Add(black, stpB.GetStartPosition());
        }
        GameObject[] whites = GameObject.FindGameObjectsWithTag("White");
        foreach (GameObject white in whites)
        {
            white.TryGetComponent<SnapToPos>(out SnapToPos stpW);
            whitePieces.Add(white, stpW.GetStartPosition());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePos(GameObject piece, (int,int) pos)
    {
        if (piece.CompareTag("Black"))
            blackPieces[piece] = pos;
        else
            whitePieces[piece] = pos;
    }

    public bool PieceAtPos((int,int) pos)
    {
        if (blackPieces.ContainsValue(pos) || whitePieces.ContainsValue(pos))
            return true;
        return false;
    }
}
