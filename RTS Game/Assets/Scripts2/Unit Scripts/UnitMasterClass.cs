using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMasterClass : MonoBehaviour {
    //base class for all units
    //runs the action queue for units, will run the first one until it is completed then will remove that item from the queue
    //only want to get the first item in the queue
    //will eventuall add functionality to allow for either queing up tasks or just replacing the current task
    public List<Action> actionsQueue;

    void Awake()
    {
        actionsQueue = new List<Action>();
    }

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		queueMoniter();
	}

    public void removeCurrentAction()
    {
        Action a = actionsQueue[0];
        actionsQueue.Remove(a);
        Destroy(a);
    }


    void queueMoniter()
    {
        if (actionsQueue.Count>0)
        {
            if (actionsQueue[0].actionStarted==false)
            {
                actionsQueue[0].doAction();
                actionsQueue[0].actionStarted = true;

            }
            else
            {
                if (actionsQueue[0].getIsActionComplete()==true)
                {
                    removeCurrentAction();
                }
            }
        }
    }
}
