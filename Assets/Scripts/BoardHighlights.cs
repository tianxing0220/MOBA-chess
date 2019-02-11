using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour
{
    public static BoardHighlights Instance { set; get; }

    public GameObject highlight; //the highlight indicator object
    private List<GameObject> highlights;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     *  finds the first instance of highlight object in all highlights on the board
     *  and returns the object; returns a new object if none found
     *  
     *  @return GameObject  -- the highlight object that is either from the list
     *                         or newly instantiated
     */
    private GameObject getHighLight()
    {
        //find the first instance that the highlight is
        //not active
        GameObject hl = highlights.Find(g => !g.activeSelf);

        if (hl == null) //if no element found, instantiate a new highlight
        {
            hl = Instantiate(highlight);
            highlights.Add(hl);
        }

        return hl;
    }

    /*
     *  change the active state of the highlight objects among the path
     *  that is allowed
     *  
     *  @param  bool[,] moves -- 2D array containing the true and false
     *                           values of the allowed moves for a 
     *                           chess piece
     */
    public void highlightAllowedMoves(bool[,] moves)
    {
        for (int i=0; i<BoardManager.NUM;i++)
        {
            for (int j=0; j<BoardManager.NUM;j++)
            {
                if (moves[i,j])
                {
                    GameObject hl = getHighLight();
                    hl.SetActive(true);
                    //offset 0.5f to reach the center of the board
                    hl.transform.position = new Vector3(i+0.5f,0,j+0.5f);
                }
            }
        }
    }

    /*
     *  turn off the highlights before each turn 
     */
     public void hideHighlight()
    {
        foreach (GameObject hl in highlights)
        {
            hl.SetActive(false);
        }
    }

}
