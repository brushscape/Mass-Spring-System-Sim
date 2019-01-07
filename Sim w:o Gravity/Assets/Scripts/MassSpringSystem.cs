using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassSpringSystem : MonoBehaviour {
    private const float sphereMass = 1f;
    private const float cubeMass = 1f; 
    private const float ks = 1.8f; //spring stiffness
    private const float lr = 3f; //rest length
    private const float kd = 0.5f; //damping constant 
    private const float r = 0.5f;
    private const float deltaT = 0.1f; 

    private GameObject cube;
    private GameObject sphere;

    private Vector3 prevSphereVel;
    private Vector3 prevSphereAccel;
    private Vector3 prevSpherePos;
    private Vector3 spherePos;
    private Vector3 prevCubeVel;
    private Vector3 prevCubeAccel;
    private Vector3 prevCubePos;
    private Vector3 cubePos;


    // Use this for initialization
    void Start () {
        cube = GameObject.Find("Cube");
        sphere = GameObject.Find("Sphere");

        prevSpherePos = sphere.transform.position;
        prevSphereVel = new Vector3(0, 0, 0);
        prevCubePos = cube.transform.position;
        prevCubeVel = new Vector3(0, 0, 0);

    }
	
	void Update () {
        //for when sphere being dragged
        if(!sphere.transform.position.Equals(spherePos))
        {
            prevSpherePos = sphere.transform.position;
            prevSphereVel = new Vector3(0, 0, 0);
        }

        //for when cube being dragged
        if (!cube.transform.position.Equals(cubePos))
        {
            prevCubePos = cube.transform.position;
            prevCubeVel = new Vector3(0, 0, 0);
        }

        //calculate spring force
        float lc = Vector3.Distance(sphere.transform.position, cube.transform.position);
        Vector3 difference = sphere.transform.position - cube.transform.position;
        Vector3 unitV = difference / difference.magnitude;
        Vector3 Fs = -ks * (lc - lr) * unitV; 

        //calculate damping force
        Vector3 Fd = -kd * Vector3.Dot((prevSphereVel - prevCubeVel), unitV) * unitV;

        //calculate final, total force on sphere
        Vector3 Fr = -r * prevSphereVel;
        Vector3 F = Fs + Fd + Fr;

        //calculate new position for sphere
        Vector3 sphereAccel = F / sphereMass;
        Vector3 sphereVel = prevSphereVel + sphereAccel * deltaT;
        spherePos = prevSpherePos + sphereVel * deltaT;
        sphere.transform.position = spherePos;

        //save info for next sphere calculation
        prevSphereAccel = sphereAccel;
        prevSphereVel = sphereVel;
        prevSpherePos = spherePos;

        //calculate final, total force on cube
        Fr = -r * prevCubeVel;
        F = -Fs + -Fd + Fr;

        //calculate new position for cube
        Vector3 cubeAccel = F / cubeMass;
        Vector3 cubeVel = prevCubeVel + cubeAccel * deltaT;
        cubePos = prevCubePos + cubeVel * deltaT;
        cube.transform.position = cubePos;

        //save info for next cube calculation
        prevCubeAccel = cubeAccel;
        prevCubeVel = cubeVel;
        prevCubePos = cubePos;


    }
}
