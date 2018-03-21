using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridGenerator : MonoBehaviour
{

    public static GridGenerator me;//Controls grid of tiles


    private Dictionary<Vector2, TileMasterClass> tileMap;

    private TileMasterClass[,] gridOfTiles;
    public GameObject prefabTile;

    public Vector2 gridDimensions;


    // Use this for initialization
    void Awake()
    {

        me = this;
        gridOfTiles = new TileMasterClass[(int)gridDimensions.x, (int)gridDimensions.y];


    }

    void Start()
    {
        generateGrid();

    }


    void generateGridAt(int x,int y)
    {
    }

    void generateGrid()
    {
        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                Vector3 posToCreateTile = new Vector3(x, y, 0);
                GameObject mostRecentTile =
                    (GameObject)Instantiate(prefabTile, posToCreateTile, Quaternion.Euler(0, 0, 0));

                mostRecentTile.GetComponent<TileMasterClass>().setGridCoords(new Vector2(x,y));
                mostRecentTile.transform.parent = this.gameObject.transform;
                mostRecentTile.name =
                    "Tile " + mostRecentTile.GetComponent<TileMasterClass>().getGridCoords().ToString();

                int r = Random.Range(1, 10);
                //if (r>8)
                //{
                //    mostRecentTile.GetComponent<TileMasterClass>().setTileWalkable(false);
                //    mostRecentTile.GetComponent<SpriteRenderer>().color = Color.cyan;
                    
                //}

                gridOfTiles[x, y] = mostRecentTile.GetComponent<TileMasterClass>();
            }
        }
    }

    public TileMasterClass getTile(int x,int y)//try-catch is for incase player clicks out of grid
    {
        try
        {
            return gridOfTiles[x, y];

        }
        catch
        {
            return null;
        }

    }


    public List<GameObject> getTiles(Vector2 startPos,Vector2 endPos) //gets a section of  the grid based on the coords passed in
    {
        Debug.Log("Getting Tiles");
        int lowestX, lowestY, highestX, highestY;
        List<GameObject> retVal = new List<GameObject>();
        if (startPos.x<=endPos.x)
        {
            lowestX = (int) startPos.x;
            highestX = (int) endPos.x;
        }
        else
        {
            lowestX = (int) endPos.x;
            highestX = (int) startPos.x;

        }


        if (startPos.y <= endPos.y)
        {
            lowestY = (int)startPos.y;
            highestY = (int)endPos.y;
        }
        else
        {
            lowestY = (int)endPos.y;
            highestY = (int)startPos.y;

        }


        for (int x = (int)lowestX;x<=(int)highestX;x++)
        {
            for (int y = (int)lowestY;y<=(int)highestY;y++)
            {
                retVal.Add(gridOfTiles[x,y].gameObject);
            }
        }

        Debug.Log(retVal.Count);
        return retVal;
    }




    public List<TileMasterClass> getTileNeighbors(TileMasterClass Tile)//gets passed in a tile and based on the coords in the grid it will get the neighboring tiles
    //and return them as a list
    {

        List<TileMasterClass> retVal = new List<TileMasterClass>();
        TileMasterClass t = Tile;

        Vector2 pos = t.getGridCoords();


        if (pos.x==0)
        {
            if (pos.y == 0)//bottom left
            {
                retVal.Add(gridOfTiles[(int)pos.x+1,(int)pos.y]);
                retVal.Add(gridOfTiles[(int)pos.x, (int)pos.y+1]);


            }else if(pos.y==gridDimensions.y-1)//top left
            {

                retVal.Add(gridOfTiles[(int)pos.x + 1, (int)pos.y]);
                retVal.Add(gridOfTiles[(int)pos.x, (int)pos.y - 1]);


            }
            else
            {
                retVal.Add(gridOfTiles[(int)pos.x + 1, (int)pos.y]);
                retVal.Add(gridOfTiles[(int)pos.x, (int)pos.y + 1]);
                retVal.Add(gridOfTiles[(int)pos.x, (int)pos.y - 1]);

            }

        }else if (pos.x==gridDimensions.x-1)
        {


            if (pos.y == 0)//bottom right
            {
                retVal.Add(gridOfTiles[(int)pos.x - 1, (int)pos.y]);
                retVal.Add(gridOfTiles[(int)pos.x, (int)pos.y + 1]);


            }
            else if (pos.y == gridDimensions.y - 1)//top right
            {
                retVal.Add(gridOfTiles[(int)pos.x - 1, (int)pos.y]);
                retVal.Add(gridOfTiles[(int)pos.x, (int)pos.y - 1]);
            }
            else
            {
                retVal.Add(gridOfTiles[(int)pos.x - 1, (int)pos.y]);
                retVal.Add(gridOfTiles[(int)pos.x, (int)pos.y + 1]);
                retVal.Add(gridOfTiles[(int)pos.x, (int)pos.y - 1]);
            }

        }
        else
        {
            //grid not horizontal x
            if (pos.y == 0)//bottom right
            {
                retVal.Add(gridOfTiles[(int)pos.x - 1, (int)pos.y]);
                retVal.Add(gridOfTiles[(int)pos.x, (int)pos.y + 1]);
                retVal.Add(gridOfTiles[(int)pos.x+1, (int)pos.y]);

            }
            else if (pos.y == gridDimensions.y - 1)//top right
            {
                retVal.Add(gridOfTiles[(int)pos.x - 1, (int)pos.y]);
                retVal.Add(gridOfTiles[(int)pos.x, (int)pos.y - 1]);
                retVal.Add(gridOfTiles[(int)pos.x+1, (int)pos.y]);

            }
            else
            {
                retVal.Add(gridOfTiles[(int)pos.x - 1, (int)pos.y]);
                retVal.Add(gridOfTiles[(int)pos.x, (int)pos.y - 1]);
                retVal.Add(gridOfTiles[(int)pos.x+1, (int)pos.y]);
                retVal.Add(gridOfTiles[(int)pos.x,(int)pos.y + 1]);

            }
        }

        return retVal;



    }





    // Update is called once per frame
    void Update()
    {

    }
}
