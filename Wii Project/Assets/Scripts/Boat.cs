using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Keep in mind: the script is called Boat but is actually applied to the worldplane object
public class Boat : MonoBehaviour
{
    private Vector3 moveDelta;
    private Vector3 size;
    private MeshRenderer renderer;
    public int duration;
    private float moveSpeed;

    public GameObject[] worldObjects;

    private string eventTrigger = "";

    public Popup popUp;

    private Vector3 positionStart;
    private Vector3[] positions; 

    public bool play;

    void Start()
    {
        //check size worldplane mesh
        renderer = GetComponent<MeshRenderer>();
        size = renderer.bounds.size;
        moveSpeed = (size.z + 100) / (duration);


        positionStart = transform.position;
        play = false;

        //save original positions all objects to reset correctly
        positions = new Vector3[worldObjects.Length];

        for (int i = 0; i < worldObjects.Length; i++)
        {
            positions[i] = worldObjects[i].transform.position;

        }
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
            //Move everything as long as the character is not yet at the end
            if ((size.z / 2) > (transform.position.z * -1))
            {
                transform.Translate(0, 0, -moveSpeed * Time.deltaTime, Space.World);

                for (int i = 0; i < worldObjects.Length; i++)
                {
                    GameObject gObject = worldObjects[i];
                    gObject.transform.Translate(0, 0, -moveSpeed * Time.deltaTime, Space.World);

                }
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
        transform.position = positionStart;
         for (int i = 0; i < worldObjects.Length; i++)
        {
          GameObject gObject = worldObjects[i];
          worldObjects[i].transform.position = positions[i];
        }


    }
}
