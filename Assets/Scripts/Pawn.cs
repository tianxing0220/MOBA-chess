using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
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
        bool[,] moves = new bool[BoardManager.NUM, BoardManager.NUM];

        ChessPiece ch, ch2;

        //light team
        if (isLight)
        {
            //diagonal left
            if (currentX !=0 && currentZ != 7 )
            {
                ch = BoardManager.Instance.chessPieces[currentX-1,currentZ+1];

                //an opponent piece is encountered
                if (ch != null && !ch.isLight)
                {
                    moves[currentX-1, currentZ+1]=true;
                }
            }

            //diagonal right
            if (currentX != 7 && currentZ != 7)
            {
                ch = BoardManager.Instance.chessPieces[currentX + 1, currentZ + 1];

                //an opponent piece is encountered
                if (ch != null && !ch.isLight)
                {
                    moves[currentX + 1, currentZ + 1] = true;
                }
            }

            //middle
            if (currentZ!=7)
            {
                ch = BoardManager.Instance.chessPieces[currentX, currentZ + 1];
                if (ch==null)
                {
                    moves[currentX, currentZ + 1] = true;
                }
            }

            //middle on first move
            if (currentZ==1)
            {
                ch = BoardManager.Instance.chessPieces[currentX, currentZ + 1];
                ch2 = BoardManager.Instance.chessPieces[currentX, currentZ + 2];
                //the case when we can move forward
                if (ch == null && ch2 == null)
                {
                    moves[currentX, currentZ + 2] = true;
                }
            }
        }
        else //dark team
        {
            //diagonal left
            if (currentX != 0 && currentZ != 0)
            {
                ch = BoardManager.Instance.chessPieces[currentX - 1, currentZ - 1];

                //an opponent piece is encountered
                if (ch != null && ch.isLight)
                {
                    moves[currentX - 1, currentZ - 1] = true;
                }
            }

            //diagonal right
            if (currentX != 7 && currentZ != 0)
            {
                ch = BoardManager.Instance.chessPieces[currentX + 1, currentZ - 1];

                //an opponent piece is encountered
                if (ch != null && ch.isLight)
                {
                    moves[currentX + 1, currentZ - 1] = true;
                }
            }

            //middle
            if (currentZ != 0)
            {
                ch = BoardManager.Instance.chessPieces[currentX, currentZ - 1];
                if (ch == null)
                {
                    moves[currentX, currentZ - 1] = true;
                }
            }

            //middle on first move
            if (currentZ == 6)
            {
                ch = BoardManager.Instance.chessPieces[currentX, currentZ - 1];
                ch2 = BoardManager.Instance.chessPieces[currentX, currentZ - 2];
                //the case when we can move forward
                if (ch == null && ch2 == null)
                {
                    moves[currentX, currentZ - 2] = true;
                }
            }
        }

        return moves;

    }
}
