using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void UpdatePos(GameObject piece, (int, int) pos)
    {
        if (piece.CompareTag("Black"))
        {
            blackPieces[piece] = pos;
            GameObject[] cp = whitePieces.Keys.ToArray();
            foreach (GameObject white in cp)
            {
                if (whitePieces[white] == pos)
                {
                    white.SetActive(false);
                    whitePieces.Remove(white);
                }
            }
        }
        else
        {
            whitePieces[piece] = pos;
            GameObject[] cp = blackPieces.Keys.ToArray();
            foreach (GameObject black in cp)
            {
                if (blackPieces[black] == pos)
                {
                    black.SetActive(false);
                    blackPieces.Remove(black);
                }
            }
        }
    }

    public void ChangeTurnToWhite()
    {
        if (BlackMoved())
        {
            foreach (GameObject black in blackPieces.Keys)
                black.GetComponent<CapsuleCollider>().enabled = false;
            foreach (GameObject white in whitePieces.Keys)
                white.GetComponent<CapsuleCollider>().enabled = true;
        }
        GameObject[] cp = blackPieces.Keys.ToArray();
        foreach (GameObject black in cp)
        {
            black.TryGetComponent<SnapToPos>(out SnapToPos stp);
            stp.UpdatePositionWithBoard();
        }
        cp = whitePieces.Keys.ToArray();
        foreach (GameObject white in cp)
        {
            white.TryGetComponent<SnapToPos>(out SnapToPos stp);
            stp.UpdatePositionWithBoard();
        }
    }

    bool WhiteMoved()
    {
        foreach (GameObject white in whitePieces.Keys)
        {
            white.TryGetComponent<SnapToPos>(out SnapToPos stp);
            if (!whitePieces[white].Equals(stp.GetCurrentPos()))
                return true;
        }
        return false;
    }

    public void ChangeTurnToBlack()
    {
        GameObject[] cp = whitePieces.Keys.ToArray();
        foreach (GameObject white in cp)
        {
            white.TryGetComponent<SnapToPos>(out SnapToPos stp);
            stp.UpdatePositionWithBoard();
        }
        cp = blackPieces.Keys.ToArray();
        foreach (GameObject black in cp)
        {
            black.TryGetComponent<SnapToPos>(out SnapToPos stp);
            stp.UpdatePositionWithBoard();
        }
    }

    bool BlackMoved()
    {
        foreach (GameObject black in blackPieces.Keys)
        {
            black.TryGetComponent<SnapToPos>(out SnapToPos stp);
            if (!blackPieces[black].Equals(stp.GetCurrentPos()))
                return true;
        }
        return false;
    }

    public GameObject PieceAtPos((int, int) pos)
    {
        if (blackPieces.ContainsValue(pos))
            return blackPieces.Keys.First(s => blackPieces[s].Equals(pos));
        if (whitePieces.ContainsValue(pos))
            return whitePieces.Keys.First(s => whitePieces[s].Equals(pos));
        return null;
    }
}
