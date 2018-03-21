using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtonClickHandler : MonoBehaviour
{
	public static InventoryButtonClickHandler me;

	public delegate void OnButtonClickDelegate();
	public static OnButtonClickDelegate ButtonBarakaClickDelegate;
	public static OnButtonClickDelegate ButtonEnerjiSantaliClickDelegate; 



	public void onButtonBarakaClick() 
	{
		SelectionManager.me.selectionMode = selectingModes.creatingBuildings;
		Debug.Log("baraka");
		ButtonBarakaClickDelegate();
	}

	public void onButtonEnerjiSantraliClick()
	{
		SelectionManager.me.selectionMode = selectingModes.creatingBuildings;
		Debug.Log("enerji staral");

		ButtonEnerjiSantaliClickDelegate();
	}

	// Use this for initialization
	void Start ()
	{


		me = this;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
