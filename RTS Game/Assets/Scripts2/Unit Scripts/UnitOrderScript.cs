using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitOrderScript : MonoBehaviour
{
    //this scripts checks whether unit is selected or not and gives order to unit


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        areAnyUnitSelected();
        commandUnitsToMove();
    }


    void commandUnitsToMove()
    {
        if (areAnyUnitSelected() == true)
        {
            Debug.Log("are any units eleceted");

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Mouse basıldı");
                Vector3 mousePos = Input.mousePosition;
                Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(mousePos);
                mouseInWorld.z = 0;

                foreach (GameObject g in SelectionManager.me.getSelected())
                {
                    if (g.GetComponent<UnitMasterClass>() != null)
                    {
                        UnitMasterClass um = g.GetComponent<UnitMasterClass>();
                        Action a = g.AddComponent<Action_MoveToLocation>();
                        a.initialiseLocation(mouseInWorld);
                        um.actionsQueue.Add(a);
                    }
                }

            }
        }
    }

    bool areAnyUnitSelected()
    {
        if (SelectionManager.me.selectionMode == selectingModes.units && SelectionManager.me.getSelected().Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}