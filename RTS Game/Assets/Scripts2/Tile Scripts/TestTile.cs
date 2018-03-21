using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTile : TileMasterClass {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void onSelect()
    {
        Debug.Log("Test Tile Class"+this.gameObject.transform.position);
    }


}
