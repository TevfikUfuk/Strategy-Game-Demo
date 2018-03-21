using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_MoveToLocation : Action {
    //this script contains code that moves the unit that the action is assigned to a location given using the pathfinding algorithm
    private Vector3 positionWeAreMovingTo;


    public override void initialiseLocation(Vector3 position)
    {
        positionWeAreMovingTo = position;
    }


    public override void doAction()
    {
        UnitMovement um = this.GetComponent<UnitMovement>();
        um.moveToLocation(positionWeAreMovingTo);
    }


    public override bool getIsActionComplete()
    {
        if (Vector3.Distance(positionWeAreMovingTo,this.transform.position)<2.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
