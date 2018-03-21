using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    //basic class to move the unit along a path provided by the patfinding class
    public bool areWeMoving = false;
    public List<Vector3> curPath;
    private int pathCounter = 0;

    // Use this for initialization
    void Start()
    {

    }


    public void moveToLocation(Vector3 positionTo)
    {
        pathCounter = 0;
        curPath = PathFind.me.getPath(this.transform.position, positionTo);
        areWeMoving = true;

        if (curPath.Count==0)
        {
            areWeMoving = false;//need some way to get rid of errant action
            this.GetComponent<UnitMasterClass>().removeCurrentAction();
        }

    }


    void moveAlongPath()
    {
        if (Vector3.Distance(this.transform.position,curPath[pathCounter])>0.5f)
        {
            Vector3 dir = curPath[pathCounter] - transform.position;
            transform.Translate(dir*5*Time.deltaTime);
        }
        else
        {
            if (pathCounter<curPath.Count-1)
            {
                pathCounter++;
            }
            else
            {
                areWeMoving = false;
            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (areWeMoving == true)
        {
            moveAlongPath();
        }

    }
}
