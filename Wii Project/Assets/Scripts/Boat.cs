using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private Vector3 moveDelta;
    private Vector3 size;
    private MeshRenderer renderer;
    public int duration;
    private float moveSpeed;

    private string eventTrigger = "";

    public Popup popUp;

    private Vector3 position;

    public bool play;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        size = renderer.bounds.size;
        moveSpeed = (size.z+100) / (duration);
       

        position = transform.position;
        play = false;
    }

    void Update()
    {
        eventTrigger = popUp.eventTrigger;

        if (eventTrigger == "begin")
        {
            
            play = true;
        }
        if (play)
        {
            if ((size.z / 2) > (transform.position.z * -1))
                transform.Translate(0, 0, -moveSpeed * Time.deltaTime, Space.World);
        }

        Debug.Log(eventTrigger);
    }

    public void RestartPosition()
    {
        transform.position = position;
    }
}
