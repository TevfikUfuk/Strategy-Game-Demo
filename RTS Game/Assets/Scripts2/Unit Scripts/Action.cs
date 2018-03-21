using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {
    //base class for action system
    //all methods can be overridden by the child classes to suit the action
    public bool actionStarted = false;


    public virtual void initialiseLocation(Vector3 position)
    {

    }


    public virtual void initialiseTarget(GameObject target)
    {

    }


    public virtual void initialiseTile(TileMasterClass tm)
    {
        
    }

    public virtual void doAction()
    {

    }


    public virtual void doAction(GameObject me,GameObject target)
    {

    }

    public virtual void doAction(GameObject me) 
    {

    }

    public virtual void doAction(GameObject me,Vector3 position)
    {

    }

    public virtual void doAction(GameObject me,TileMasterClass tile)
    {

    }

    public virtual void onActionComplete()
    {

    }

    public virtual bool getIsActionComplete()
    {
        return false;
    }

    
}
