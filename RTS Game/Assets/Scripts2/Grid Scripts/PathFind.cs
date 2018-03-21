using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind : MonoBehaviour
{
    public static PathFind me;


    void Awake()
    {
        me = this;
    }


    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }




    public List<Vector3> getPath(Vector3 startPos, Vector3 endPos)
    {
        List<TileMasterClass> listOfTiles = new List<TileMasterClass>();
        getPath(startPos, endPos, ref listOfTiles);
        List<Vector3> retVal = convertToVectorPath(listOfTiles);
        return retVal;
    }


    void getPath(Vector3 startPos, Vector3 endPos, ref List<TileMasterClass> store)
    {
        Vector2 sPos = new Vector2((int)startPos.x, (int)startPos.y);
        Vector2 ePos = new Vector2((int)endPos.x, (int)endPos.y);//converting positions to ints in order to get the rough tile coords from them

        TileMasterClass startNode = GridGenerator.me.getTile((int)sPos.x, (int)sPos.y);//gets the two tiles as TileMasterClass
        TileMasterClass targetNode = GridGenerator.me.getTile((int)ePos.x, (int)ePos.y);

        if (startNode==null||targetNode==null||targetNode.isTileWalkable() == false)//if the destination is not a walkable tile then it breaks
        {
            Debug.Log("One of the tiles is not walkable");
            return;

        }

        List<TileMasterClass> openSet = new List<TileMasterClass>();//open set is tiles we want to check
        List<TileMasterClass> closedSet = new List<TileMasterClass>();//closed set is tiles we have checked and dont want to include in the path

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            TileMasterClass node = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)//check to see if we can find a tile with lower or equal f cost 
                //(approximation of distance to target from start including this tile)
                {
                    if (openSet[i].getH() < node.getH())//if the tile has a lower distance to the target tile than the current one in "node"
                    {
                        node = openSet[i];//sets node to be this closer tile
                    }
                }
            }


            openSet.Remove(node);
            closedSet.Add(node);
            if (node == targetNode)//checks to see if the node is the tile we want to go to, if so we return the path after is has been retraced
            {
                Debug.Log("finish the path");
                RetracePath(startNode, targetNode, ref store);
                return;
            }

            foreach (TileMasterClass neighbour in GridGenerator.me.getTileNeighbors(node))//goes through each of the neighbors of the node
            {

                if (!neighbour.isTileWalkable() || closedSet.Contains(neighbour) || neighbour == null || node == null)
                //if the neighbor is not accessable or the node is null, go onto the next neighbour
                {
                    continue;
                }

                int newCostToNeighbour = node.getG() + GetDistance(node, neighbour);//calculates the cost of going to the neighbour from the start os the path
                if (newCostToNeighbour < node.getG() || !openSet.Contains(neighbour))//if the cost is shorter and the open set doesnt contain the nieghbour
                {
                    neighbour.setG(newCostToNeighbour);
                    neighbour.setH(GetDistance(neighbour, targetNode));//sets the g and h values of the neighbour
                    neighbour.setParent(node);

                    if (!openSet.Contains(neighbour))//adds the neighbour to the openset if not already there
                    {
                        openSet.Add(neighbour);
                    }

                }
            }
        }
    }

    List<Vector3> convertToVectorPath(List<TileMasterClass> tiles)//converts the path found to a list of vector3 as the objects moving dont need all the tile info
    {
        List<Vector3> retVal = new List<Vector3>();
        foreach (TileMasterClass tile in tiles)
        {
            retVal.Add(tile.gameObject.transform.position);
        }

        return retVal;
    }


    void RetracePath(TileMasterClass startNode, TileMasterClass targetNode, ref List<TileMasterClass> store)//goes through the path via the parent variable and puts it in a list
    {

        List<TileMasterClass> path = new List<TileMasterClass>();
        TileMasterClass currentNode = targetNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.getParent();

        }
        path.Reverse();//have to reverse it because it traces the path from finish to start  using the parent variable     
        store = path;

    }



    int GetDistance(TileMasterClass nodeA, TileMasterClass nodeB)
    //gets the distance between two grid coords and returns them multiplied
    {
        int dstX = Mathf.Abs((int)nodeA.getGridCoords().x - (int)nodeB.getGridCoords().x);
        int dstY = Mathf.Abs((int)nodeA.getGridCoords().y - (int)nodeB.getGridCoords().y);

        if (dstX > dstY)//to make that  the final number is positive
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
