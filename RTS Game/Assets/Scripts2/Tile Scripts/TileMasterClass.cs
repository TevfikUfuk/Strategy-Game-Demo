using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMasterClass : MonoBehaviour
{
    private float gridX, gridY;


    //pathfinding
    private bool walkable = true;//decides if the tile can be walked on
    private int gCost;//cost for moving from the start to this tile
    private int hCost;//an estimate of the distance between this tile and the tile you want to get path to

    private TileMasterClass parent;//used in the pathfinding to retrace steps and give the final path

    public void setG(int val)
    {
        gCost = val;
    }

    public void setH(int val)
    {
        hCost = val;
    }

    public int getH()
    {
        return hCost;
    }

    public int getG()
    {
        return gCost;
    }

    


    public virtual void onSelect()
    {
        //this.GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log("Tile Master Class"+this.gameObject.transform.position);
    }

    public virtual void OnDeselect()
    {
       // this.GetComponent<SpriteRenderer>().color = Color.white;

    }

    public Vector2 getGridCoords()
    {
        return  new Vector2(gridX,gridY);

    }

    public void setGridCoords(Vector2 coords)
    {
        gridX = coords.x;
        gridY = coords.y;

    }


    public bool isTileWalkable()//patfinding
    {
        return walkable;
    }

    public void setTileWalkable(bool canWalk)//pathfinding
    {
        walkable = canWalk;

    }

    public TileMasterClass getParent()
    {
        return parent;
    }

    public int fCost//new for pathfinding
    {
        get
        {
            return gCost + hCost;//estimation of the total route to destination if this tile is used

        }
    }

    public void setParent(TileMasterClass val)
    {
        parent = val;
    }

    


}
