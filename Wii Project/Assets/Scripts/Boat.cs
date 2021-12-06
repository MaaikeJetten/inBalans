using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boat : MonoBehaviour
{
    private Vector3 moveDelta;
    private Vector3 size;
    private MeshRenderer renderer;
    public int duration;
    private float moveSpeed;

    public GameObject lightHouse;

    private string eventTrigger = "";

    public Popup popUp;
    private float cooldownTime = 3f;

    private Vector3 position;

    public bool play;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        size = renderer.bounds.size;
        moveSpeed = (size.z+100) / (duration);
       

        position = transform.position;
        play = false;

        lightHouse.transform.SetParent(this.transform);
    }

    void Update()
    {
        eventTrigger = popUp.eventTrigger;

        if (eventTrigger == "begin")
        {
            play = true;
        }

        if (eventTrigger == "pause")
        {
            play = false;
        }

        if (play)
        {
            if ((size.z / 2) > (transform.position.z * -1))
            {
                transform.Translate(0, 0, -moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    public void Begin()
    {
        play = true;
        moveSpeed = (size.z + 100) / (duration);
        RestartPosition();
       
    }

    public void Pause()
    {
        play = false;
        moveSpeed = 0;
    }

    public void Doorgaan()
    {
        
        play = true;
        moveSpeed = (size.z + 100) / (duration);
    }

    public void RestartPosition()
    {
        transform.position = position;
    }

    
}
