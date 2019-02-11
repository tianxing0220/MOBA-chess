using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int currentX { set; get; }
    public int currentZ { set; get; }

    public bool isLight; //to distinguish the teams

    /*
     *  update the chess piece to itself 
     */
    public void setPosition(int x, int z)
    {
        currentX = x;
        currentZ = z;
    }

    /*
     *  to be overriden by the child members
     *  determines the possible chess moves
     */
    public virtual bool[,] PossibleMove()
    {
        return new bool[BoardManager.NUM,BoardManager.NUM];
    }
}
