using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAction_CreateWorker : BuildingAction
{
    private List<Vector3> spawnPosList;

    public override void doAction()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public override bool areWeDone()
    {
        if (timer <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void startAction()
    {
        started = true;
    }

    public override void onComplete()
    {
        Building b = this.gameObject.GetComponentInParent<Building>();
        Vector3 spawnPos = b.getGoToTile().transform.position;
        //Vector3 spawnPos = new Vector3(b.setTileNearMe(b.).getGridCoords().x, b.setTileNearMe().getGridCoords().x,0);    
        //if (spawnPosList.Count > 0)
        //{
        //    spawnPosList.Add(spawnPos);
        //    foreach (var pos in spawnPosList)
        //    {
        //        if (spawnPos.Equals(pos))
        //        {
        //            spawnPos.x = spawnPos.x + 1;
        //        }
        //    }
        //}
        

        Debug.Log(spawnPos);
        GameObject g = (GameObject)Instantiate(prefabToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));

    }

    public override string getButtonText()
    {
        return "Create Worker";
    }

    public override string getProgress()
    {
        return "Creating worker";
    }

    // Use this for initialization
    void Start()
    {

        spawnPosList = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
