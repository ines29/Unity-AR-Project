using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2Puzzle : MonoBehaviour
{
    private TrackPieces ts;
    private int turn = 1;
    [SerializeField]
    private GameObject blackBishopH6;
    // Start is called before the first frame update
    void Start()
    {
        ts = gameObject.GetComponent<TrackPieces>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleMove(GameObject piece)
    {
        piece.TryGetComponent<SnapToPos>(out SnapToPos stp);
        (int, int) piecePos = stp.GetCurrentPos();
        print(piecePos);
        if (turn == 1) {
            if(piece.name.Equals("Rock White 2") && piecePos.Equals((6,4))) 
            {
                ts.ChangeTurnToBlack();
                blackBishopH6.TryGetComponent<SnapToPos>(out SnapToPos BishStp);
                Transform targetField = BishStp.GetField((6, 4)).transform;
                BishStp.ClearSnaps();
                BishStp.AddPosToSnapsCheckPieces(6, 4);
                BishStp.transform.position = targetField.position;
                BishStp.SetCurrentPositionFromTransform(targetField);
                BishStp.UpdatePositionWithBoard();
                turn++;
            } else
            {
                Transform originalField = stp.GetField(stp.GetStartPosition()).transform;
                stp.transform.position = originalField.position;
                stp.SetCurrentPositionFromTransform(originalField);
                stp.UpdatePositionWithBoard();
                print("Wrong move!");
                //Signal wrong move
            }
        } else
        {
            if (piece.name.Equals("Queen White") && piecePos.Equals((3, 5)))
            {
                //Win Message
                print("You won!");
            }
            else
            {
                Transform originalField = stp.GetField(stp.GetStartPosition()).transform;
                stp.transform.position = originalField.position;
                stp.SetCurrentPositionFromTransform(originalField);
                stp.UpdatePositionWithBoard();
                //Signal wrong move
            }
        }
    }
}
