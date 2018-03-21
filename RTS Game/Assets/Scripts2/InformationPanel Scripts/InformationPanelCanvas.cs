using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InformationPanelCanvas : MonoBehaviour
{

    public static InformationPanelCanvas me;
    public Text buildingName;
    public Image buildingImage;
    public Button createWorker;
    private UnityEvent btn;
    public bool isClicked = false;

    private selectingModes selectionMode;


    void Awake()
    {
        me = this;
    }

    // Use this for initialization
    void Start()
    {


    }

    public void Clicked()
    {
        isClicked = true;
    }



    // Update is called once per frame
    void Update()
    {
        selectionMode = SelectionManager.me.selectionMode;

        List<GameObject> building = SelectionManager.me.getSelected();

        if (building.Count > 0)
        {
            Building b = building[0].GetComponent<Building>();
            // if (b.actionsWeCanPerform != null)
            // {
            List<BuildingAction> buildActions = b.actionsWeCanPerform;

            if (buildActions.Count > 0)
            {
                //string queueInfo = "";
                //if (b.buildingActionQueue.Count == 0)
                //{
                //    queueInfo = "No actions queued";
                //}
                //else
                //{
                //    queueInfo = "\n" + b.buildingActionQueue[0].getProgress() + "\n" + "There are" +
                //                (b.buildingActionQueue.Count - 1).ToString() + " actions remaining" + "\n" + b.name;
                //}
                string BuildingInfo = b.name;
                foreach (BuildingAction ba in buildActions)
                {
                    if (isClicked)
                    {
                        BuildingAction baNew = (BuildingAction)Instantiate(ba, building[0].transform);
                        building[0].gameObject.GetComponent<Building>().buildingActionQueue.Add(baNew);
                        isClicked = false;


                    }
                }
            }
            // }
            if (building[0].GetComponent<Building>() == true)
            {
                buildingName.text = building[0].GetComponent<Building>().name;
                buildingImage.sprite = building[0].GetComponent<Building>().buildingSprite;
            }
            else
            {
                Debug.Log("ffe");
            }
        }

    }

    public void CreateWorker()
    {
        isClicked = true;
    }
}


