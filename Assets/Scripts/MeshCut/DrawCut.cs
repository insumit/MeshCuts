using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCut : MonoBehaviour
{
    Vector3 pointA;
    Vector3 pointB;
    
    Camera cam;
    //public GameObject[] obj;
    public List<GameObject> goList;
    GameObject[] goArray;

    void Start() {
        cam = FindObjectOfType<Camera>();
        FindGameObjectsInLayer(6);
    }

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = -cam.transform.position.z;

        if (Input.GetMouseButtonDown(0)) {
            pointA = cam.ScreenToWorldPoint(mouse);
            
        }
        if (Input.GetMouseButtonUp(0)) {
            pointB = cam.ScreenToWorldPoint(mouse);
            CreateSlicePlane();
        }
    }

    void CreateSlicePlane() {
        Vector3 centre = (pointA+pointB)/2;
        Vector3 up = Vector3.Cross((pointA-pointB),(pointA-cam.transform.position)).normalized;
        
        foreach(GameObject g in goList)
            Cutter.Cut(g, centre, up, null, true, true);
    }

    GameObject[] FindGameObjectsInLayer(int layer)
    {
        goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }
}
