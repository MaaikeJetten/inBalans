using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        float[] distances = new float[32];
        distances[10] = 15;
        camera.layerCullDistances = distances;
        camera.layerCullSpherical = true;
    }
}
