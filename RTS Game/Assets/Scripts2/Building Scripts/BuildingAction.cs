using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAction : MonoBehaviour
{


	public GameObject prefabToSpawn;

	public int woodCost, stoneCost, foodCost, ironCost, goldCost;

	public bool callEachFrame = false;

	public float timer;

	public bool started = false;
	// Use this for initialization
	public virtual void doAction() {//called while the action is being done, call each frame decides whether its called each frame or just once
		

	}


	public virtual void startAction()
	{

	}


	public virtual void onComplete()//called when the action is done
	{

	}

	public virtual bool canWeDo()//called before to check if we can do the action
	{
		return false;
	}

	public virtual bool areWeDone()//is the action done
	{
		return false;
	}


	public virtual string getButtonText()//just what to display on the button to give an indication of what the action does
	{
		return "";
	}

	public virtual string getProgress()//give some idea of how close to completing the action
	{
		return "";
	}

	// Update is called once per frame
	void Update () {
		
	}
}
