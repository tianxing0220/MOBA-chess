using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[BoardManager.NUM,BoardManager.NUM];

        //up left
        knightMove(currentX - 1, currentZ + 2, ref r);
        //up right
        knightMove(currentX + 1, currentZ + 2, ref r);
        //right up
        knightMove(currentX + 2, currentZ + 1, ref r);
        //right down
        knightMove(currentX + 2, currentZ - 1, ref r);
        //down left
        knightMove(currentX - 1, currentZ - 2, ref r);
        //down right
        knightMove(currentX + 1, currentZ - 2, ref r);
        //left up
        knightMove(currentX - 2, currentZ + 1, ref r);
        //left down
        knightMove(currentX - 2, currentZ - 1, ref r);
        return r;
    }

    public void knightMove(int x, int y, ref bool[,] r)
    {
        ChessPiece ch;
        if (x >= 0 && x < BoardManager.NUM && y>=0 && y < BoardManager.NUM)
        {
            ch = BoardManager.Instance.chessPieces[x,y];
            if (ch==null)
            {
                r[x, y] = true;
            } else if (isLight != ch.isLight)
            {
                r[x, y] = true;
            }
        }
    }
}
