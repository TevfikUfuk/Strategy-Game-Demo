using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BuildingStore : MonoBehaviour
{
    public static BuildingStore me; //this script stores all possible buildings and draws gui for placing them when you want to build one

    public GameObject[] buildings;

    [SerializeField] GameObject selectedBuilding;

    void Awake()
    {
        me = this;
    }

    public GameObject getToBuild()
    {
        return selectedBuilding;
    }

    void OnGUI()
    {

        if (SelectionManager.me.selectionMode==selectingModes.creatingBuildings)
        {
            int yMod = 0;
            foreach (GameObject b in buildings)
            {
                try
                {

                    Building buildingSrc = b.GetComponent<Building>();
                    Rect pos = new Rect(50,50+(50*yMod),100,50);
                    
                    if (GUI.Button(pos,b.name))
                    {
                        selectedBuilding = b;
                    }
                    yMod += 1;

                }
                catch
                {
                    Debug.Log("Building missing a component");
                }
            }
        }
    }


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}