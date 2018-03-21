using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    
    public static GUIManager me;//acts a a storage for any gui elements that are used by other scripts

    public Texture2D blackBoxSemiTrans;

    private selectingModes selectionMode;
    // Use this for initialization
    void Awake()
    {
        me = this;

    }

    public Texture2D getBlackBoxSemiTrans()
    {
        return blackBoxSemiTrans;
    }

    private float originalWidth = 1920.0f;//sca
    private float originalHeight = 1080.0f;
    private Vector3 scale;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        selectionMode = SelectionManager.me.selectionMode;

    }


    void OnGUI()
    {
        GUI.depth = 0;
        scale.x = Screen.width / originalWidth;
        scale.y = Screen.height / originalHeight;
        scale.z = 1;

        var svMat = GUI.matrix;
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);



        if (selectionMode == selectingModes.buildings)
        {
            //drawGUIBackground();
            //drawBuildingActionButtons();
        }

        GUI.matrix = svMat;
    }

    void drawGUIBackground()
    {
        Rect bgPos = new Rect(0,originalHeight-(originalHeight/5),originalWidth,originalHeight/5);
        GUI.Box(bgPos,"");
    }


    void drawBuildingActionButtons()
    {
        List<GameObject> building = SelectionManager.me.getSelected();
        if (building.Count>0)
        {
            Building b = building[0].GetComponent<Building>();
            List<BuildingAction> buildActions = b.actionsWeCanPerform;

            if (buildActions.Count>0)
            {
                string queueInfo = "";
                if (b.buildingActionQueue.Count==0)
                {
                    queueInfo = "No actions queued";
                }
                else
                {
                    queueInfo = "\n" + b.buildingActionQueue[0].getProgress() + "\n" + "There are" +
                                (b.buildingActionQueue.Count - 1).ToString() + " actions remaining"+"\n"+b.name;
                }


                string BuildingInfo = b.name;

                Rect infoPos = new Rect(20,(originalHeight-(originalHeight/5))+10,originalWidth/4,(originalHeight/5)-20);//have slight offsetsso there is a gap at the edge
                GUI.Box(infoPos,BuildingInfo+queueInfo);

                int xoff = 0;
                int yoff = 0;
                foreach (BuildingAction ba in buildActions)
                {
                    Rect buttonPos = new Rect(550+(200*xoff), originalHeight - (originalHeight / 5)+(20+(100*yoff)),200,100);

                    if ( GUI.Button(buttonPos, ba.getButtonText()))
                    {
                        BuildingAction baNew = (BuildingAction) Instantiate(ba, building[0].transform);
                        building[0].gameObject.GetComponent<Building>().buildingActionQueue.Add(baNew);
                       

                    }

                    xoff++;
                    if (xoff>4)
                    {
                        xoff = 0;
                        yoff++;
                    }
                }


            }
        }


    }
}






