using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class AssignMethodToInventoryItems : MonoBehaviour {


	public static AssignMethodToInventoryItems me;

	[SerializeField] GameObject selectedBuilding;
	public GameObject[] buildings;

	

	private List<Transform> childs;
	public GameObject getToBuild()
	{
		return selectedBuilding;
	}

	// Use this for initialization
	void Start()
	{
		me = this;
	   
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
}
