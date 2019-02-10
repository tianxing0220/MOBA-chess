using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private const float TILE_SIZE = 1.0f; //dimension of the tile
    private const float TILE_OFFSET = 0.5f; //offset to reach center of the tile
    private const int NUM = 10; //10 tiles for width
    private const float PIECE_OFFSET = 0.5f; //offset to move up the chess piece

    //default selected piece coordinate
    //with a 10x10 chess board,
    //the point range is (0,0) and (10,10)
    private int selectedX = -1;
    private int selectedZ = -1;

    //all pieces avaliable
    //assume to be initialized already
    public List<GameObject> allPieces;

    //the pieces on board
    //initialized to avoid null reference
    public List<GameObject> onboardPieces =  new List<GameObject>();

    //the orientation of the pieces
    //default facing the cetner of the board
    private Quaternion orientation = Quaternion.Euler(0,180,0);

    /* 
     * Start is called before the first frame update
     */
    void Start()
    {
        //spawn the default pieces on start
        SpawnAll();
    }

    /*
     * Update is called once per frame
     */
    void Update()
    {
        /*
         * DEBUG: Draw() method displays the tile grid of
         *        the board. By default, it is 10x10.
         */
        Draw(); //display the outline

        UpdateSelection(); //update the piece selection
    }

    /*
     *  use debug to draw the grid of the 10x10 board 
     */
    private void Draw()
    {
        Vector3 dir; //the starting point of each line

        //applying the number of tiles in the dimension
        Vector3 width = Vector3.right * NUM;
        Vector3 height = Vector3.forward * NUM;

        //NUM+1 for the additional line in the end of the board
        //to wrap around
        for (int i=0; i<NUM+1; i++)
         {
             //line is drawn from left to right
             dir = Vector3.forward * i;
             Debug.DrawLine(dir,dir+width);
         }

        for (int i=0; i<NUM+1; i++)
        {
            //line is drawn from back to forward
            dir = Vector3.right * i;
            Debug.DrawLine(dir,dir+height);
        }

        //a point on board is selected
        if (selectedX >=0 && selectedZ >=0)
        {
            /*
             * DEBUG: displays cross line upon the
             *        the tile grid currently being
             *        selected by the user
             */
            Debug.DrawLine(
                Vector3.forward*selectedZ+Vector3.right*selectedX,
                Vector3.forward* (selectedZ+TILE_SIZE)+Vector3.right*(selectedX+TILE_SIZE));
            Debug.DrawLine(
                Vector3.forward * (selectedZ+TILE_SIZE) + Vector3.right * selectedX,
                Vector3.forward * selectedZ + Vector3.right * (selectedX+TILE_SIZE));
        }
    }

    /*
     * update the cursor selection on the board 
     */
    private void UpdateSelection()
    {
        if (!Camera.main)
        {
            return;
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(
                Camera.main.ScreenPointToRay(Input.mousePosition),
                out hit, 
                Mathf.Infinity, 
                LayerMask.GetMask("Plane")))
            {
                /*
                 * DEBUG: displays the point that the cursor
                 *        hits on the chess plane, of which
                 *        the layermask is defined to be "Plane"
                 */
                //Debug.Log(hit.point);

                //update at the point
                //selected by the cursor
                selectedX = (int)hit.point.x;
                selectedZ = (int)hit.point.z;
            }
            else
            {   
                //reset selected point to null
                selectedX = -1;
                selectedZ = -1;
            }
        }
    }

    /*
     * spawn the chess pieces from allPieces
     * 
     * @param   int index -- index of the piece to spawn in allPieces
     * @param   int index -- spawn location
     */
    private void Spawn(int index, Vector3 position)
    {
        //declare new game object from list allPieces
        GameObject piece = Instantiate(
            allPieces[index],
            position,
            orientation) as GameObject;

        //update the location of the parent
        piece.transform.SetParent(transform);

        //update the onboard pieces list
        onboardPieces.Add(piece);
    }

    /*
     * helper method that returns the center location of a particular tile
     * 
     * @param   int x -- x coordinate of the tile origin
     * @param   int y -- y coordinate of the tile origin
     */
     private Vector3 GetTileCenter(int x, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * z) + TILE_OFFSET;
        //to locate the piece on top of the board
        //instead of through the board
        origin.y += PIECE_OFFSET; 
        return origin;
    }

    /*
     * helper method that spawns all default start pieces 
     */
     private void SpawnAll()
    {
        int currCoord = 0; //the current coordinate

        //spawn all default pieces
        //player 1 team
        for (int i=0; i<allPieces.Capacity; i++)
        {
            Spawn(i,GetTileCenter(currCoord+i,0));
        }

        currCoord = NUM-1;
        //player 2 team
        for (int i=0; i<allPieces.Capacity;i++)
        {
            Spawn(i,GetTileCenter(currCoord-i,NUM-1));
        }
    }
}
