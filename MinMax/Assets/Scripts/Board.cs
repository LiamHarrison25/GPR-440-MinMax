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

    private Dictionary<int, GameObject> tileGameObjects;
    private Dictionary<int, GameObject> pieceGameObjects;

    private Dictionary<int, Piece> pieces;
    private Dictionary<int, Tile> board;

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        int totalSpaces = boardSize * boardSize;
        board = new Dictionary<int, Tile>(totalSpaces);
        pieces = new Dictionary<int, Piece>(totalSpaces);
        tileGameObjects = new Dictionary<int, GameObject>(totalSpaces);
        pieceGameObjects = new Dictionary<int, GameObject>(totalSpaces);

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

                GameObject tileGameObject;
                if (isRed)
                {
                    tileGameObject = Instantiate(redSpace, newPos, Quaternion.identity);
                }
                else
                {
                    tileGameObject = Instantiate(blackSpace, newPos, Quaternion.identity);
                }

                if (tileGameObject != null)
                {
                    tileGameObject.transform.SetParent(this.transform);
                    tileGameObjects.Add(boardSize * i + j, tileGameObject);
                    Tile tile = new Tile();
                    board.Add(boardSize * i + j, tile);
                }

                if (j <= 2) // Spawn in red pieces
                {
                    if (!isRed)
                    {
                        GameObject newPiece = Instantiate(redPiece, newPos, Quaternion.identity);
                        newPiece.transform.SetParent(tileGameObject.transform);

                        Piece piece = new Piece();
                        piece.pieceType = pieceType.Red;
                        Tile tile = board[boardSize * i + j];
                        tile.AddPiece(piece);
                        pieces[boardSize * i + j] = piece;
                    }
                    
                }
                else if (j >= 5) // Spawn in black pieces
                {
                    if (isRed)
                    {
                        GameObject newPiece = Instantiate(blackPiece, newPos, Quaternion.identity);
                        newPiece.transform.SetParent(tileGameObject.transform);

                        Piece piece = new Piece();
                        piece.pieceType = pieceType.Black;
                        Tile tile = board[boardSize * i + j];
                        tile.AddPiece(piece);
                        pieces[boardSize * i + j] = piece;
                    }
                }
                else
                {
                    Piece piece = new Piece();
                    piece.pieceType = pieceType.None;
                    pieces[boardSize * i + j] = piece;
                }

                    isRed = !isRed;
                
            }
        }
    }

    public int GetBoardSize()
    {
        return boardSize;
    }

}

//TODO: Need to optimize the memory consumption. Could get up to 700mb per turn if I use the Piece and Tile classes. 
// Create an optimized way to store the pieces and the board

// Every state will have around 20 moves. Every state will have 256 bytes based on the 8x8 board
// 1 level -> 20
// 2 level -> 20*20
// 3 level -> 20^3          //Around 2 mb for level 3
// 4 level -> 20^4
// 5 level -> 20^5          //Around 700 mb for level 5
