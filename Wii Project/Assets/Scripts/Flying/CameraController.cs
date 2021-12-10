using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject plane;
    private float positionZ;
    private Vector3 startPlane;
    private Vector3 startCamera;
    public Vector2 cameraArm;

    private void Start()
    {
        startPlane = plane.transform.position;
        startCamera = new Vector3(startPlane.x, startPlane.y + cameraArm.x, startPlane.z - cameraArm.y);
    }

    private void Update()
    {
        Vector3 planePosition = plane.transform.position;
        transform.position = new Vector3(planePosition.x, planePosition.y + cameraArm.x, planePosition.z - cameraArm.y);
        transform.LookAt(new Vector3(planePosition.x, planePosition.y+7, planePosition.z));
    }
}
