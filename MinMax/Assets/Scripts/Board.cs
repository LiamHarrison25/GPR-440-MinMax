using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    [SerializeField] private GameObject redSpace;
    [SerializeField] private GameObject blackSpace;
    [SerializeField] private float boardTileSize;
    [SerializeField] private Transform startingPos;
    [SerializeField] private GameObject blackPiece;
    [SerializeField] private GameObject redPiece;


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
                newPos.x = newPos.x + (boardTileSize * i);
                newPos.y = newPos.y + (boardTileSize * j);

                GameObject boardTile;
                if (isRed)
                {
                    boardTile = Instantiate(redSpace, newPos, Quaternion.identity);
                }
                else
                {
                    boardTile = Instantiate(blackSpace, newPos, Quaternion.identity);
                }

                if (boardTile != null)
                {
                    boardTile.transform.SetParent(this.transform);
                    board.Add(boardSize * i + j, boardTile);
                }

                if (j <= 2) // Spawn in red pieces
                {
                    if (!isRed)
                    {
                        GameObject newPiece = Instantiate(redPiece, newPos, Quaternion.identity);
                    }
                    
                }
                else if (j >= 5) // Spawn in black pieces
                {
                    if (isRed)
                    {
                        GameObject newPiece = Instantiate(blackPiece, newPos, Quaternion.identity);
                    }
                }

                isRed = !isRed;
                
            }
        }
    }

}
