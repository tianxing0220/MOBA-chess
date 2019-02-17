using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
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

        //top left
        i = currentX;
        j = currentZ;
        while (true)
        {
            i--;
            j++;
            if (i<0 || j >= BoardManager.NUM)
            {
                break;
            }
            ch = BoardManager.Instance.chessPieces[i, j];
            if (ch==null)
            {
                r[i, j] = true;
            }
            else
            {
                if (isLight != ch.isLight)
                {
                    r[i, j] = true;
                    
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
            if (i >=BoardManager.NUM || j >= BoardManager.NUM)
            {
                break;
            }
            ch = BoardManager.Instance.chessPieces[i, j];
            if (ch == null)
            {
                r[i, j] = true;
            }
            else
            {
                if (isLight != ch.isLight)
                {
                    r[i, j] = true;
                    
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
                r[i, j] = true;
            }
            else
            {
                if (isLight != ch.isLight)
                {
                    r[i, j] = true;
                    
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
                r[i, j] = true;
            }
            else
            {
                if (isLight != ch.isLight)
                {
                    r[i, j] = true;
                    
                }
                break;
            }
        }

        return r;
    }
}
