using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this script to a GameObject with a Renderer component attached
//If the GameObject is visible to the camera, the message is output to the console

public class IsVisible : MonoBehaviour
{
    Renderer m_Renderer;
    // Use this for initialization
    void Start()
    {
        m_Renderer = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (m_Renderer.isVisible)
        {
            Debug.Log("Object is visible");
        }
        else if (!m_Renderer.isVisible)
        {
            Debug.Log("Object is no longer visible");
        }
    }
}
