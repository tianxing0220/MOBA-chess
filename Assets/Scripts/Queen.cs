using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
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
        bool[,] moves = new bool[BoardManager.NUM,BoardManager.NUM];

        ChessPiece ch;
        int i, j, index;

        //going right
        index = currentX;
        while (true)
        {
            index++;
            //when out of bounds
            if (index >= BoardManager.NUM)
            {
                break;
            }
            ch = BoardManager.Instance.chessPieces[index, currentZ];

            if (ch == null) //no piece is blocking the way
            {
                moves[index, currentZ] = true; //the move is allowed
            }
            else
            {
                //if the blocking piece is an opponent piece
                if (ch.isLight != isLight)
                {
                    moves[index, currentZ] = true;
                }

                //if the blocking piece is a friendly piece, simply break
                //since the move is not allowed
                break;
            }
        }

        //going left
        index = currentX;
        while (true)
        {
            index--;
            //when out of bounds
            if (index < 0)
            {
                break;
            }
            ch = BoardManager.Instance.chessPieces[index, currentZ];

            if (ch == null) //no piece is blocking the way
            {
                moves[index, currentZ] = true; //the move is allowed
            }
            else
            {
                //if the blocking piece is an opponent piece
                if (ch.isLight != isLight)
                {
                    moves[index, currentZ] = true;
                }

                //if the blocking piece is a friendly piece, simply break
                //since the move is not allowed
                break;
            }
        }

        //going up
        index = currentZ;
        while (true)
        {
            index++;
            //when out of bounds
            if (index >= BoardManager.NUM)
            {
                break;
            }
            ch = BoardManager.Instance.chessPieces[currentX, index];

            if (ch == null) //no piece is blocking the way
            {
                moves[currentX, index] = true; //the move is allowed
            }
            else
            {
                //if the blocking piece is an opponent piece
                if (ch.isLight != isLight)
                {
                    moves[currentX, index] = true;
                }

                //if the blocking piece is a friendly piece, simply break
                //since the move is not allowed
                break;
            }
        }

        //going down
        index = currentZ;
        while (true)
        {
            index--;
            //when out of bounds
            if (index < 0)
            {
                break;
            }
            ch = BoardManager.Instance.chessPieces[currentX, index];

            if (ch == null) //no piece is blocking the way
            {
                moves[currentX, index] = true; //the move is allowed
            }
            else
            {
                //if the blocking piece is an opponent piece
                if (ch.isLight != isLight)
                {
                    moves[currentX, index] = true;
                }

                //if the blocking piece is a friendly piece, simply break
                //since the move is not allowed
                break;
            }
        }

        //top left
        i = currentX;
        j = currentZ;
        while (true)
        {
            i--;
            j++;
            if (i < 0 || j >= BoardManager.NUM)
            {
                break;
            }
            ch = BoardManager.Instance.chessPieces[i, j];
            if (ch == null)
            {
                moves[i, j] = true;
            }
            else
            {
                if (isLight != ch.isLight)
                {
                    moves[i, j] = true;

                }
                break;
            }
        }

        //top right
        i = currentX;
        j = currentZ;
        while (true)
        {
            i++;
            j++;
            if (i >= BoardManager.NUM || j >= BoardManager.NUM)
            {
                break;
            }
            ch = BoardManager.Instance.chessPieces[i, j];
            if (ch == null)
            {
                moves[i, j] = true;
            }
            else
            {
                if (isLight != ch.isLight)
                {
                    moves[i, j] = true;

                }
                break;
            }
        }

        //down left
        i = currentX;
        j = currentZ;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j < 0)
            {
                break;
            }
            ch = BoardManager.Instance.chessPieces[i, j];
            if (ch == null)
            {
                moves[i, j] = true;
            }
            else
            {
                if (isLight != ch.isLight)
                {
                    moves[i, j] = true;

                }
                break;
            }
        }

        //down right
        i = currentX;
        j = currentZ;
        while (true)
        {
            i++;
            j--;
            if (i >= BoardManager.NUM || j < 0)
            {
                break;
            }
            ch = BoardManager.Instance.chessPieces[i, j];
            if (ch == null)
            {
                moves[i, j] = true;
            }
            else
            {
                if (isLight != ch.isLight)
                {
                    moves[i, j] = true;

                }
                break;
            }
        }

        return moves;


    }
}
