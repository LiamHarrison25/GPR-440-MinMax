using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    [SerializeField] private GameObject redSpace;
    [SerializeField] private GameObject blackSpace;
    [SerializeField] private float pieceSize;
    [SerializeField] private Transform startingPos;


    private int boardSize = 8;

    private Dictionary<int, GameObject> board;

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        board = new Dictionary<int, GameObject>();

        bool isRed = true;

        int i, j;
        for (i = 0; i < boardSize; i++)
        {
            isRed = !isRed;
            for (j = 0; j < boardSize; j++)
            {
                Vector3 newPos = startingPos.position;
                newPos.x = newPos.x + (pieceSize * i);
                newPos.y = newPos.y + (pieceSize * j);

                GameObject piece;
                if (isRed)
                {
                    piece = Instantiate(redSpace, newPos, Quaternion.identity);
                }
                else
                {
                    piece = Instantiate(blackSpace, newPos, Quaternion.identity);
                }

                isRed = !isRed;

                board.Add(boardSize * i + j, piece);
            }
        }
    }

}
