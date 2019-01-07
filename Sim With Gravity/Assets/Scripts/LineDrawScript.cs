using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawScript : MonoBehaviour {

    private GameObject cube;
    private GameObject sphere;
    LineRenderer line; 

    // Use this for initialization
    void Start () {
        cube = GameObject.Find("Cube");
        sphere = GameObject.Find("Sphere");
        line = gameObject.AddComponent<LineRenderer>();
        line.widthMultiplier = 0.2f;
        line.positionCount = 2;
        line.material = new Material(Shader.Find("Specular")); 
    }

    // Update is called once per frame
    void Update () {
        if (cube != null && sphere != null)
        {
            line.SetPosition(0, cube.transform.position);
            line.SetPosition(1, sphere.transform.position);
        }

    }
}
