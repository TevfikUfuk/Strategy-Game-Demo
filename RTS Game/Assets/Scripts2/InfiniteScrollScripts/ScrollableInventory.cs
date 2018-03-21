using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollableInventory : MonoBehaviour
{

    public static ScrollableInventory me;

    [SerializeField] GameObject selectedBuilding;
    public GameObject[] buildings;


    public GameObject itemSlotPrefab;
    public GameObject itemSlotPrefab2;

    public GameObject content;
    public ToggleGroup itemSlotToggleGroup;

    private int xPos = 0;
    private int yPos = 0;
    private GameObject itemSlot;
    private GameObject itemSlot1;



    public GameObject getToBuild()
    {
        return selectedBuilding;
    }

    // Use this for initialization
    void Start()
    {
        me = this;
        CreateInventroySlotsInWindow();
        InventoryButtonClickHandler.ButtonBarakaClickDelegate += buildingBarakaDrag;
        InventoryButtonClickHandler.ButtonEnerjiSantaliClickDelegate += buildingEnerjiSantraliDrag;


    }

    // Update is called once per frame
    void Update()
    {

    }

    void buildingBarakaDrag()
    {
        //foreach (GameObject b in buildings)
        //{

            selectedBuilding = buildings[0];
       // }


    }


    void buildingEnerjiSantraliDrag()
    {
        //foreach (GameObject b in buildings)
        //{

        selectedBuilding = buildings[1];
        // }


    }

    public void onDestroy()
    {
        selectedBuilding = null;
    }


    private void CreateInventroySlotsInWindow()
    {

        for (int i = 0; i < 20; i++) //
        {

            itemSlot = (GameObject)Instantiate(itemSlotPrefab);
            itemSlot.name = i.ToString();
            itemSlot.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                InventoryButtonClickHandler.me.onButtonBarakaClick();
            });
            itemSlot.transform.SetParent(content.transform);
            itemSlot.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);
            //itemSlot.GetComponent<Button>().onClick.AddListener(InventoryButtonClickHandler.me.onButtonClick);
            yPos -= (int)itemSlot.GetComponent<RectTransform>().rect.height;


            itemSlot1 = (GameObject)Instantiate(itemSlotPrefab2);
            itemSlot1.name = i.ToString();
            itemSlot1.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                InventoryButtonClickHandler.me.onButtonEnerjiSantraliClick();
            });
            itemSlot1.transform.SetParent(content.transform);
            itemSlot1.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);
            //itemSlot.GetComponent<Button>().onClick.AddListener(InventoryButtonClickHandler.me.onButtonClick);
            yPos -= (int)itemSlot.GetComponent<RectTransform>().rect.height;



        }

    }

}
