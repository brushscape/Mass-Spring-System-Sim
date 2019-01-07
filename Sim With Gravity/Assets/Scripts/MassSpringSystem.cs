using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassSpringSystem : MonoBehaviour {
    private const float mass = 1f;
    private const float ks = 2f; //spring stiffness
    private const float lr = 3f; //rest length
    private const float kd = 0.5f; //damping constant 
    private const float r = 0.3f;
    private const float deltaT = 0.1f; 
    private Vector3 g = new Vector3(0, -9.8f, 0); //gravity
    private GameObject cube;
    private GameObject sphere;

    private Vector3 prevVel;
    private Vector3 prevAccel;
    private Vector3 prevPos;
    private Vector3 pos; 


    // Use this for initialization
    void Start () {
        cube = GameObject.Find("Cube");
        sphere = GameObject.Find("Sphere");

        prevPos = sphere.transform.position;
        prevVel = new Vector3(0, 0, 0);
       
    }
	
	void Update () {
        //for when sphere being dragged
        if(!sphere.transform.position.Equals(pos))
        {
            prevPos = sphere.transform.position;
            prevVel = new Vector3(0, 0, 0);
        }

        //calculate spring force
        float lc = Vector3.Distance(sphere.transform.position, cube.transform.position);
        Vector3 difference = sphere.transform.position - cube.transform.position;
        Vector3 unitV = difference / difference.magnitude;
        Vector3 Fs = -ks * (lc - lr) * unitV; 

        //calculate damping force
        Vector3 cubeVel = new Vector3(0, 0, 0);
        Vector3 Fd = -kd * Vector3.Dot((prevVel - cubeVel), unitV) * unitV;

        //calculate final, total force
        Vector3 Fg = mass * g;
        Vector3 Fr = -r * prevVel;
        Vector3 F = Fs + Fg + Fd + Fr;

        //calculate new position
        Vector3 accel = F / mass;
        Vector3 vel = prevVel + accel * deltaT;
        pos = prevPos + vel * deltaT;
        sphere.transform.position = pos;

        //save info for next calculation
        prevAccel = accel;
        prevVel = vel;
        prevPos = pos;

 
    }
}
