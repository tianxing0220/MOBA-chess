using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }
    private const float TILE_SIZE = 1.0f; //dimension of the tile
    private const float TILE_OFFSET = 0.5f; //offset to reach center of the tile
    public static int NUM = 8; //8 tiles for width

    public ChessPiece[,] chessPieces { set; get; }
    private ChessPiece selectedPiece;

    public bool isLightTurn = true; //default start with team 1

    //default selected piece coordinate
    //with a 10x10 chess board,
    //the point range is (0,0) and (8,8)
    private int selectedX = -1;
    private int selectedZ = -1;

    //all pieces avaliable
    //assume to be initialized already
    public List<GameObject> allPieces;

    //the pieces on board
    //initialized to avoid null reference
    public List<GameObject> onboardPieces;

    //the orientation of the pieces
    //default facing the cetner of the board
    private Quaternion orientation = Quaternion.Euler(0,0,0);

    /* 
     * Start is called before the first frame update
     */
    void Start()
    {
        Instance = this;
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
         *        the board. By default, it is 8x8.
         */
        Draw(); //display the outline

        UpdateSelection(); //update the piece selection



        //mouse selection and piece movement
        if (Input.GetMouseButtonDown(0))
        {
            //check within the board range
            if (selectedX >= 0 && selectedZ >=0)
            {
                //check if a piece has already been selected
                if (selectedPiece==null)
                {
                    selectPiece(selectedX,selectedZ);
                }
                else
                {
                    movePiece(selectedX,selectedZ);
                }
            }
        }
    }

    /*
     * helper method to select a piece
     */
     private void selectPiece(int x, int z)
    {
        //no selected piece
        if (chessPieces[x,z]==null) 
        {
            return;
        }

        //trying to select in the wrong turn
        if (chessPieces[x,z].isLight != isLightTurn) 
        {
            return;
        }

        //get the allowed moves according to the chess piece
        allowedMoves = chessPieces[x, z].PossibleMove();

        selectedPiece = chessPieces[x,z];

        //display the highlights
        BoardHighlights.Instance.highlightAllowedMoves(allowedMoves);
    }

    /*
     * helper method to move a piece
     */
     private void movePiece(int x, int z)
    {
        //depends on the chess piece itself
        if (allowedMoves[x,z])
        {
            ChessPiece ch = chessPieces[x, z];

            if (ch!=null && ch.isLight!=isLightTurn)
            {
                //game over case 
                if (ch.GetType()==typeof(King))
                {
                    // end game
                    return;
                }

                //eliminate the opponent piece case
                onboardPieces.Remove(ch.gameObject);
                Destroy(ch.gameObject);
            }
            //set the current position to null before moving the piece
            chessPieces[selectedPiece.currentX, selectedPiece.currentZ] = null;

            //update the new position to the chess piece
            //as well as the board
            selectedPiece.transform.position = GetTileCenter(x,z);
            chessPieces[x, z] = selectedPiece;
            selectedPiece.setPosition(x,z);

            //after movement, the turn is over
            isLightTurn = !isLightTurn;
        }

        //in the case of unallowed move is attempted
        //unselect the current piece as feedback
        selectedPiece = null;

        //hide the highlight either after moving the piece or unselect the piece
        BoardHighlights.Instance.hideHighlight();
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
     * @param   int x     -- x coordinate of the tile
     * @param   int z     -- z coordinate of the tile 
     */
    private void Spawn(int index, int x, int z)
    {
        //declare new game object from list allPieces
        GameObject piece = Instantiate(
            allPieces[index],
            GetTileCenter(x,z),
            orientation) as GameObject;

        //add the spawn piece to the 2D array
        chessPieces[x, z] = piece.GetComponent<ChessPiece>();
        //set the position of the piece in the piece itself
        chessPieces[x, z].setPosition(x,z);

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

        return origin;
    }

    /*
     * helper method that spawns all default start pieces 
     */
     private void SpawnAll()
    {
        onboardPieces = new List<GameObject>();
        chessPieces = new ChessPiece[NUM,NUM];
        //spawn all default pieces

        //player 1 team (light)
        //king
        Spawn(0, 3, 0);
        //queen
        Spawn(1, 4, 0);
        //rooks
        Spawn(2, 0, 0);
        Spawn(2, 7, 0);
        //bishops
        Spawn(3, 2, 0);
        Spawn(3, 5, 0);
        //knights
        Spawn(4, 1, 0);
        Spawn(4, 6, 0);

        //pawns
        for (int i=0; i< 8; i++)
        {
            Spawn(5, i, 1);
        }


        //player 2 team (dark)
        orientation = Quaternion.Euler(0, 180, 0);
        //king
        Spawn(6, 4, 7);
        //queen
        Spawn(7, 3, 7);
        //rooks
        Spawn(8, 0, 7);
        Spawn(8, 7, 7);
        //bishops
        Spawn(9, 2, 7);
        Spawn(9, 5, 7);
        //knights
        Spawn(10, 1, 7);
        Spawn(10, 6, 7);

        //pawns
        for (int i = 0; i < 8; i++)
        {
            Spawn(11, i, 6);
        }
    }
}
