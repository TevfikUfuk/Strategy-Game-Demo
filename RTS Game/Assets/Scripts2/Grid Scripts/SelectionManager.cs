using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Vuforia;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager me;
    public selectingModes selectionMode;
    public GameObject buildingPlaceCursor;

    [SerializeField]
    List<GameObject> currentlySelected; //for multi selection

    private bool drawMultiSelectBox = false;

    private GameObject firstTileSelected = null;
    private GameObject lastTileSelected = null;



    void Awake()
    {
        me = this;
        currentlySelected = new List<GameObject>();
    }



    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentlySelected.Count);
        //CreatingBuildings_checkForMouseClick();
        //Units_CheckForLeftMouseClick();



        if (selectionMode != selectingModes.creatingBuildings && selectionMode != selectingModes.tiles)
        {
            decideSelecetionMode();
        }

        if (selectionMode == selectingModes.tiles)//added different selecting modes just to be able to place buildings and get tiles
        {
            tiles_checkForLeftMouseClick();
        }
        else if (selectionMode == selectingModes.creatingBuildings)
        {
            //Debug.Log("Burdaaa");
            CreatingBuildings_checkForMouseClick();
        }
        else if (selectionMode == selectingModes.units)
        {
            Units_CheckForLeftMouseClick();
        }
        else if (selectionMode == selectingModes.buildings)
        {
            buildings_checkForLeftClick();
        }

        shouldWeDisplayBuildingConstructCursor();



    }

    void decideSelecetionMode()
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        RaycastHit2D raycast = Physics2D.Raycast(mousePos, Vector2.zero, 0f);

        try
        {

            GameObject hitObject = raycast.collider.gameObject;
            Debug.Log(hitObject.tag);
            if (hitObject.gameObject.tag=="Building")
            { 
                selectionMode = selectingModes.buildings;
            }
            else if(hitObject.gameObject.tag=="Unit")
            {
                selectionMode = selectingModes.units;
            }

        }
        catch
        {
           
        }

    }

    void shouldWeDisplayBuildingConstructCursor()
    {

        if (selectionMode == selectingModes.creatingBuildings)//added different selecting modes just to be able to place buildings and get tiles
        {
            buildingPlaceCursor.SetActive(true);
        }
        else
        {
            buildingPlaceCursor.SetActive(false);
        }

    }


    void Units_CheckForLeftMouseClick()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

            RaycastHit2D raycast = Physics2D.Raycast(mousePos, Vector2.zero, 0f);
            try
            {
                GameObject hitObject = raycast.collider.gameObject;
                Debug.Log(hitObject.name);
                if (hitObject.tag == "Unit")//have to also make sure that units have a z value of -1 so that the colliders on the tiles dont block raycast,maybe use a mask to avoid this issue.

                {
                    Debug.Log("ssss");
                    setSelected(hitObject);
                }
            }
            catch
            {
                Debug.Log("No valid object is selected");

            }
        }
    }




    //create some kind of constructor manager to put in it?
    void CreatingBuildings_checkForMouseClick()//this is the controller for checking if the player wants to build something
    {
        //Debug.Log("F");

       // GameObject selectedBuilding = BuildingStore.me.getToBuild();
       //GameObject selectedBuilding = ScrollableInventory.me.getToBuild();
        GameObject selectedBuilding = AssignMethodToInventoryItems.me.getToBuild();


        //Debug.Log(selectedBuilding.name);

        if (selectedBuilding != null)//make sure a building has been selected
        {
            Building selectedBuildScript = selectedBuilding.GetComponent<Building>();
            if (selectedBuildScript != null)//make sure that selected building has a building component
            {
                Debug.Log(selectedBuildScript.name);
                getMultipleTilesFromCoords(selectedBuildScript.widthInTiles, selectedBuildScript.heightInTiles);//gets the tiles that the building would need
                //if it were placed at the current mouse position

                buildingPlaceCursor.GetComponent<SpriteRenderer>().sprite = selectedBuildScript.buildingSprite;//draws the cursor showing an image of the building
                buildingPlaceCursor.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

                Color cursorClour = new Color(1, 1, 1, 0.5f);
                buildingPlaceCursor.GetComponent<SpriteRenderer>().color = cursorClour;//makes it semitranparent so that we can see through
                if (isCurrentLocAValidForConstruction() == true)
                {
                    buildingPlaceCursor.GetComponent<SpriteRenderer>().color = cursorClour;
                }
                else
                {
                    cursorClour = new Color(1, 0, 0, 0.5f);
                    buildingPlaceCursor.GetComponent<SpriteRenderer>().color = cursorClour;

                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (isCurrentLocAValidForConstruction())//creates the building there if the location is valid and the player has left clicked
                    {
                        createBuildingAtLocation(buildingPlaceCursor.transform.position, selectedBuildScript.widthInTiles, selectedBuildScript.heightInTiles);
                        selectionMode = selectingModes.buildings;
                    }
                }
            }
           
        }

    }


    void buildings_checkForLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject g = getSelectionRaycast();

            if (g.tag == "Building")
            {
                Debug.Log("tıklandıı");
                setSelected(g);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            clearSelected();
        }



    }


    void createBuildingAtLocation(Vector3 cursorPos, int width, int height)//creates a building from the one currently selected at the mouse position
    {

        //gets the lowets and highest of the each axis, works cause the selected tiles are in increasing order
        int xLowBound = (int)currentlySelected[0].GetComponent<TileMasterClass>().getGridCoords().x;
        int xHighBound = (int)currentlySelected[currentlySelected.Count - 1].GetComponent<TileMasterClass>().getGridCoords().x;
        int yLowBound = (int)currentlySelected[0].GetComponent<TileMasterClass>().getGridCoords().y;
        int yHighBound = (int)currentlySelected[currentlySelected.Count - 1].GetComponent<TileMasterClass>().getGridCoords().y;

        for (int x = 0; x < currentlySelected.Count - 1; x++)//goes through all the tiles and makes the ones not on the outside(where the actual building will be placed unwalkable)
        {

            GameObject tile = currentlySelected[x];
            TileMasterClass tm = tile.GetComponent<TileMasterClass>();

            Vector2 curTileGrid = tm.getGridCoords();
            if (curTileGrid.x == xLowBound || curTileGrid.x == xHighBound)
            {
                if (curTileGrid.y == yLowBound || curTileGrid.y == yHighBound)
                {
                    Debug.Log("Keeping" + tile.name + " Walkable");

                }
            }
            else if (curTileGrid.y == yLowBound || curTileGrid.y == yHighBound)
            {
                if (curTileGrid.x == xLowBound || curTileGrid.x == xHighBound)
                {
                    Debug.Log("Keeping" + tile.name + " Walkable");

                }
            }
            else
            {
                tm.setTileWalkable(false);

            }
        }


        //this code creates the building
        Vector3 spawnPos = currentlySelected[(currentlySelected.Count - 1) / 2].transform.position;
        GameObject built = (GameObject)Instantiate(AssignMethodToInventoryItems.me.getToBuild(), spawnPos, Quaternion.Euler(0, 0, 0));
        SpriteRenderer sr = built.AddComponent<SpriteRenderer>();
        sr.sprite = built.GetComponent<Building>().buildingSprite;
        sr.sortingOrder = 10;
        built.AddComponent<BoxCollider2D>();
        built.SetActive(true);
        clearSelected();


    }





    bool isCurrentLocAValidForConstruction()//basicly goes through the selection of tiles and checks if they are a valid place to build a building
    {
        if (currentlySelected.Count <= 0)
        {
            return false;//there is not a tile at the mouse cursor for the script to use

        }

        //for building to be placed at a location there has to be 2 conditions met all the tiles must be walkable&
        //&the building must have a ring of walkable tiles around it.

        foreach (GameObject tile in currentlySelected)
        {
            TileMasterClass tm = tile.GetComponent<TileMasterClass>();
            if (tm.isTileWalkable() == false)
            {
                return false;
            }

        }

        int xLowBound = (int)currentlySelected[0].GetComponent<TileMasterClass>().getGridCoords().x;
        int xHighBound = (int)currentlySelected[currentlySelected.Count - 1].GetComponent<TileMasterClass>().getGridCoords().x;
        int yLowBound = (int)currentlySelected[0].GetComponent<TileMasterClass>().getGridCoords().y;
        int yHighBound = (int)currentlySelected[currentlySelected.Count - 1].GetComponent<TileMasterClass>().getGridCoords().y;

        for (int x = 0; x < currentlySelected.Count - 1; x++)
        {
            GameObject tile = currentlySelected[x];
            TileMasterClass tm = tile.GetComponent<TileMasterClass>();

            Vector2 curTileGrid = tm.getGridCoords();
            if (curTileGrid.x == xLowBound || curTileGrid.x == xHighBound)
            {
                if (curTileGrid.y == yLowBound || curTileGrid.y == yHighBound)
                {
                    if (tm.isTileWalkable() == false)
                    {
                        return false;//make sure that all edge tiles are walkable

                    }
                }
            }

            if (curTileGrid.y == yLowBound || curTileGrid.y == yHighBound)
            {
                if (curTileGrid.x == xLowBound || curTileGrid.x == xHighBound)
                {
                    if (tm.isTileWalkable() == false)
                    {
                        return false;//make sure that all edge tiles are walkable

                    }
                }

            }

        }

        return true;
    }




    void getMultipleTilesFromCoords(int width, int height)//this method modifiesthe coords passed in to account for the  ring of walkable tiles
    //we need around it and gets the coords from the grid generator
    {
        try//uses mouse position and building dimensions to calculate the tiles to get and then gets them from the grid generator
        {
            width += 2;
            height += 2;//adding 2 to account for edge around building
            GameObject tileAtMousePoint = null;
            selectionRaycast(ref tileAtMousePoint);
            TileMasterClass tm = tileAtMousePoint.GetComponent<TileMasterClass>();//throws an error when the mouse is not over a valid tile
            Vector2 tileGridCoords = tm.getGridCoords();
            if (isSelectionInGridRange(tileGridCoords, width, height))
            {
                Debug.Log("Enough Space");
                Vector2 startPos = new Vector2(tileGridCoords.x - (width / 2), tileGridCoords.y - (height / 2));
                Vector2 endPos = new Vector2(tileGridCoords.x + (width / 2), tileGridCoords.y + (height / 2));
                setSelected(GridGenerator.me.getTiles(startPos, endPos), true);


            }
            else
            {
                clearSelected();
                Debug.Log("Not enough space");
            }
        }
        catch
        {
            Debug.Log("No tile is at mouse position");

        }

    }

    bool isSelectionInGridRange(Vector2 centerCoords, int width, int height)
    {
        width /= 2;
        height /= 2;
        if ((centerCoords.x - width) < 0 || (centerCoords.y - height) < 0 || (centerCoords.x + width) > GridGenerator.me.gridDimensions.x || (centerCoords.y + height) > GridGenerator.me.gridDimensions.y)
        {
            return false;
        }
        else
        {
            return true;
        }


    }




    public void setSelected(GameObject toSet) //setter for selected object
    {
        if (currentlySelected.Count>0)
        {
            clearSelected();
        }
        Debug.Log("CURRENTLY SELECTED ÖNCESİ");
        currentlySelected.Add(toSet);
        Debug.Log("CURRENTLY SELECTED sonrası");

        

       // clearSelected();
        if (toSet.GetComponent<TileMasterClass>() == true)
        {
            toSet.GetComponent<TileMasterClass>().onSelect();

        }
    }

    public void setSelected(List<GameObject> toSet, bool clearExisting) //setter for selected object
    {
        if (clearExisting == true)
        {
            clearSelected();
        }

        currentlySelected = toSet;
        foreach (GameObject obj in getSelected())
        {
            if (obj.GetComponent<TileMasterClass>() == true)
            {
                obj.GetComponent<TileMasterClass>().onSelect();

            }

        }
        clearSelectedIfAnyNull();

    }

    void clearSelectedIfAnyNull()
    {
        foreach (GameObject g in currentlySelected)
        {
            if (g == null)
            {
                clearSelected();
                break;
            }
        }
    }

    public List<GameObject> getSelected() //getter for selected object
    {
        return currentlySelected;
    }

    void clearSelected() //clears the selected, also resets the values of the first and last tile selected for the next multi select
    {
        //firstTileSelected = null;
        //lastTileSelected = null;
        
        //if (currentlySelected.Count>0)
        //{
        //    //currentlySelected.RemoveAt(0);

        currentlySelected = new List<GameObject>();

        //}
        foreach (GameObject obj in getSelected())
        {
            if (obj.GetComponent<TileMasterClass>() == true)
            {
                obj.GetComponent<TileMasterClass>().OnDeselect();

            }
        }

        //currentlySelected = new List<GameObject>();
    }

    void tiles_checkForLeftMouseClick() //checks for a left click
    {
        if (Input.GetKey(KeyCode.LeftControl) == true)
        //if the player is holding down left control it does multi tile selection else it just does single tiles
        {
            Debug.Log("leftCtrl");
            if (Input.GetMouseButtonDown(0) == true)
            {
                Debug.Log("Multi select");
                if (firstTileSelected == null)
                {
                    Debug.Log("Selecting th first tile");
                    selectionRaycast(ref firstTileSelected);
                }
                else if (firstTileSelected != null && lastTileSelected != null)
                {
                    Vector2 startCoords = firstTileSelected.GetComponent<TileMasterClass>().getGridCoords();
                    Vector2 endCoords = lastTileSelected.GetComponent<TileMasterClass>().getGridCoords();
                    Debug.Log("Start " + startCoords);
                    Debug.Log("END " + endCoords);
                    if (firstTileSelected != null && lastTileSelected != null)
                    {
                        List<GameObject> selectedTiles = GridGenerator.me.getTiles(startCoords, endCoords);
                        setSelected(selectedTiles, true);
                        firstTileSelected = null;
                        lastTileSelected = null;
                    }
                }
            }

            if (Input.GetMouseButton(0) == false && firstTileSelected != null)
            {
                Debug.Log("Selecting second tile");
                drawMultiSelectBox = true;
                selectionRaycast(ref firstTileSelected);
            }
            else
            {
                drawMultiSelectBox = false;
            }
        }
        else if (Input.GetMouseButton(0) == true)
        {
            firstTileSelected = null;
            lastTileSelected = null;
            Debug.Log("Clicking, looking  for raycast hits");
            selectionRaycast();
        }
        else
        {
            drawMultiSelectBox = false;
        }
    }

    bool areWeMultiSelecting()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl))
        {
            return true;
        }
        else
        {
            return false;

        }
    }


    void selectionRaycast() //fires a raycast from the mouse position looking for an object with a collider,try/catch is there just incase the player doent click on one
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D raycast = Physics2D.Raycast(mousePos, Vector2.zero, 0f);
        try
        {
            GameObject hitObject = raycast.collider.gameObject;
            Debug.Log(hitObject.name);
            setSelected(hitObject);
        }
        catch
        {
            Debug.Log("No valid object is selected.");
        }
    }




    void selectionRaycast(ref GameObject objToSet)
    {
        objToSet = getSelectionRaycast();
    }

    GameObject getSelectionRaycast() //does the same as the selection raycast but instead of setting the hit object in the list it stores it in the reference                                                    //to the object provided used in the multi selection to set the initial and end point for the selection grid
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
        Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D raycast = Physics2D.Raycast(mousePos, Vector2.zero, 0f);
        try
        {
            GameObject hitObject = raycast.collider.gameObject;
            Debug.Log(hitObject.name);
            return hitObject;
        }
        catch
        {
            Debug.Log("No valid object is selected.");
        }

        return null;
    }



    void OnGUI()
    {
        //if (drawMultiSelectBox == true)//draws a box  to indicate which tiles are being selected
        //{
        //    Vector3 startScreenPos = Camera.main.WorldToScreenPoint(firstTileSelected.transform.position);
        //    Vector3 endScreenPos = Input.mousePosition;

        //    float width, height;
        //    if (startScreenPos.x > endScreenPos.x)
        //    {
        //        width = startScreenPos.x - endScreenPos.x;
        //    }
        //    else
        //    {
        //        width = endScreenPos.x - startScreenPos.x;
        //    }



        //    if (startScreenPos.y > endScreenPos.y)
        //    {
        //        height = startScreenPos.y - endScreenPos.y;
        //    }
        //    else
        //    {
        //        height = endScreenPos.y - startScreenPos.y;
        //    }

        //    Rect posToDrawBox;
        //    if (endScreenPos.x > startScreenPos.x)//need 4 ways to draw the rect due to unity always drawing it from the bottom left corner of the square
        //    //the points are the first selected tle and the mouse position
        //    {

        //        if (endScreenPos.y > startScreenPos.y)
        //        {
        //            posToDrawBox = new Rect(startScreenPos.x, Screen.height - endScreenPos.y, width, height);
        //        }
        //        else
        //        {
        //            posToDrawBox = new Rect(startScreenPos.x, Screen.height - startScreenPos.y, width, height);

        //        }

        //    }
        //    else
        //    {
        //        if (endScreenPos.y > startScreenPos.y)
        //        {
        //            posToDrawBox = new Rect(endScreenPos.x, Screen.height - endScreenPos.y, width, height);
        //        }
        //        else
        //        {
        //            posToDrawBox = new Rect(endScreenPos.x, Screen.height - startScreenPos.y, width, height);

        //        }
        //    }
        //    GUI.DrawTexture(posToDrawBox, GUIManager.me.getBlackBoxSemiTrans());

        //}
    }




}

public enum selectingModes
{
    tiles,
    units,
    creatingBuildings,
    buildings
}