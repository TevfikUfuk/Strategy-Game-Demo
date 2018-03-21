using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPathFinding : MonoBehaviour
{
    public List<Vector3> path;
    private int counter = 0;

    void Awake()
    {
        path = new List<Vector3>();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        getRandomPath();
        if (shouldWeMoveAlongPath()==true)
        {
            moveAlongPath();
        }
    }

    void getRandomPath()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetCount();
            Debug.Log("Testing Pathfinding");
            float x1 = Random.Range(0, GridGenerator.me.gridDimensions.x);
            float x2 = Random.Range(0, GridGenerator.me.gridDimensions.x);
            float y1 = Random.Range(0, GridGenerator.me.gridDimensions.y);
            float y2 = Random.Range(0, GridGenerator.me.gridDimensions.y);
 
            path = PathFind.me.getPath(this.transform.position, new Vector3((int) x2, (int) y2, 0));
        }
    }

    bool shouldWeMoveAlongPath()
    {
        if (path.Count > 0 && Vector3.Distance(this.transform.position, path[path.Count - 1]) > 0.5f &&
            counter < path.Count - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void moveAlongPath()
    {
        if (Vector3.Distance(this.transform.position, path[counter]) > 0.5f)
        {
            Vector3 dir = path[counter] - transform.position;
            transform.Translate(dir * 5 * Time.deltaTime);
        }
        else
        {

            if (counter < path.Count - 1)
            {
                counter++;
            }

        }
    }


    void resetCount()
    {
        counter = 0;
    }
}