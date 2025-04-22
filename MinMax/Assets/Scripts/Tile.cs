using UnityEngine;

public class Tile
{
    private Piece piece;


    public void AddPiece(Piece piece)
    {
        this.piece = piece;
    }

    public Piece GetPiece() 
    { 
        return this.piece; 
    }
}
