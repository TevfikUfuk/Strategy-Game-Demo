using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{//basicly storage for info on buildngs

    public int widthInTiles, heightInTiles;
    public string name;
    public Sprite buildingSprite;
    public bool built = false;


    private TileMasterClass tileNearest;
    private SpriteRenderer sr;

    public List<BuildingAction> buildingActionQueue;
    public List<BuildingAction> actionsWeCanPerform;

    void Awake()
    {
        sr = this.GetComponent<SpriteRenderer>();
       
    }

    void Start()
    {
        TileMasterClass tm = new TileMasterClass();
        tm.setGridCoords(new Vector2(transform.position.x, transform.position.y));

        setTileNearMe(tm);
    }

    void Update()
    {
        // if (built==true)
        //{


        actionsQueueMoniter();

        //}
    }

    public void setTileNearMe(TileMasterClass tm)//returns a tile that is walkable near the front of the building(nagative on y axis)
    {
        int x = (int)tm.getGridCoords().x;
        int y = (int)tm.getGridCoords().y;

        int mod = (heightInTiles / 2) + 1;
        y -= mod;

        tileNearest = GridGenerator.me.getTile(x, y);

        Debug.Log("TILE NEAREST: " + tileNearest.name);
    }

    public TileMasterClass getGoToTile()//returns the tile that is walkable and below the building in question
    {
        return tileNearest;
    }

    void actionsQueueMoniter()
    {

        if (buildingActionQueue.Count > 0)
        {
            //if (buildingActionQueue[0].enabled == false)
            //{
            //    buildingActionQueue[0].enabled = true;
            //}

            //if (buildingActionQueue[0].started== false)
            //{
            //    buildingActionQueue[0].startAction();
            //    buildingActionQueue[0].doAction();
            //}
            //else
            //{               
            //    if (buildingActionQueue[0].callEachFrame==true)
            //    {
            //        buildingActionQueue[0].doAction();
            //    }              
            //}
            // if (buildingActionQueue[0].areWeDone() == true)
            //{
            buildingActionQueue[0].onComplete();
            BuildingAction ba = buildingActionQueue[0];
            buildingActionQueue.RemoveAt(0);
            Destroy(ba.gameObject);
            //}




        }

    }
}
