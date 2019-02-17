using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
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

        ChessPiece ch;
        int i, j;

        //top
        i = currentX - 1;
        j = currentZ + 1;

        if (currentZ!=7)
        {
            for (int k=0;k < 3;k++)
            {
                if (i>=0 || i < BoardManager.NUM)
                {
                    ch = BoardManager.Instance.chessPieces[i, j];
                    if (ch == null)
                    {
                        r[i, j] = true;
                    }
                    else if(isLight != ch.isLight)
                    {
                        r[i, j] = true;
                    }
                }
                i++;
            }
        }

        //down
        i = currentX - 1;
        j = currentZ - 1;

        if (currentZ != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < BoardManager.NUM)
                {
                    ch = BoardManager.Instance.chessPieces[i, j];
                    if (ch == null)
                    {
                        r[i, j] = true;
                    }
                    else if (isLight != ch.isLight)
                    {
                        r[i, j] = true;
                    }
                }
                i++;
            }
        }

        //middle left
        if (currentX != 0)
        {
            ch = BoardManager.Instance.chessPieces[currentX - 1, currentZ];
            if (ch==null)
            {
                r[currentX - 1, currentZ] = true;
            } else if (isLight != ch.isLight)
            {
                r[currentX - 1, currentZ] = true;
            }
        }

        //middle right
        if (currentX != 7)
        {
            ch = BoardManager.Instance.chessPieces[currentX + 1, currentZ];
            if (ch == null)
            {
                r[currentX + 1, currentZ] = true;
            }
            else if (isLight != ch.isLight)
            {
                r[currentX + 1, currentZ] = true;
            }
        }

        return r;

    }
}
