using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float panSpeed = 1f;
    public float panBorderThickness = 1f;

	
	// Update is called once per frame
	void Update ()
	{
	    Debug.Log(transform.GetComponent<Camera>().orthographicSize.ToString());


        Vector3 pos = transform.position;
        //if (Input.GetKey("w")||Input.mousePosition.y>=Screen.height-panBorderThickness)
        //{
        //    pos.y += panSpeed * Time.deltaTime;
        //}

        //if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        //{
        //    pos.y -= panSpeed * Time.deltaTime;
        //}

        //if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        //{
        //    pos.x += panSpeed * Time.deltaTime;
        //}

        //if (Input.GetKey("a") || Input.mousePosition.x <=  panBorderThickness)
        //{
	    //    pos.x -= panSpeed * Time.deltaTime;
	    //}

	    if (Input.GetKey("w")) 
	    {
	        pos.y += panSpeed * Time.deltaTime;
	    }

	    if (Input.GetKey("s") )
	    {
	        pos.y -= panSpeed * Time.deltaTime;
	    }

	    if (Input.GetKey("d") )
	    {
	        pos.x += panSpeed * Time.deltaTime;
	    }

	    if (Input.GetKey("a") )
	    {
	        pos.x -= panSpeed * Time.deltaTime;
	    }

        transform.position = pos;

	}
}
