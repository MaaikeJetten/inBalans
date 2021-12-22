using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float forwardSpeed;
    [SerializeField] private float diveSpeed; 
    private float diveInput;
    private Vector3 startPos;
    public bool play;
    private string eventTrigger = "";
    public PopupPlane popUp;

    private Vector3 forwardRelativeToSurfaceNormal; //For Look Rotation
    private Quaternion targetRotation;

    public GameObject propellor;

    public GameObject[] rings;
    private Vector3[] ringPos;
    private Vector3 ringWidth;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        play = false;

        for(int i = 0; i < rings.Length; i++)
        {
            ringPos[i] = rings[i].transform.position;
            ringWidth = rings[i].GetComponent<MeshRenderer>().bounds.size;
        }
    }

    // Update is called once per frame
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

            diveInput = Input.GetAxisRaw("Vertical");
            // Vector3 startPosition = transform.position;
            //targetHigh = new Vector3(0f, 65, transform.position.z);

            Debug.Log(diveInput);


            if (diveInput == 0)
            {
                targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);

            }
            //Rising
           else if (diveInput <= -0.7)
            {
                targetRotation = Quaternion.Euler(-45, 0, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
            }
            //Diving
            else if (diveInput >= 0.7)
            {
                targetRotation = Quaternion.Euler(45, 0, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
            }
            
            transform.Translate(0f, 0f, forwardSpeed, Space.Self); 


            propellor.transform.Rotate(0f, -10f, 0f, Space.Self);

            if (transform.position.y <= 0 || transform.position.y >= 120)
            {
                popUp.failRestart.failureRestart = true;
                Pause();
                play = false;
            }


        }

    }

   

    public void Begin()
    {
        play = true;
        RestartPosition();

    }

    public void Pause()
    {
        play = false;
    }

    public void Doorgaan()
    {
        play = true;
    }

    public void RestartPosition()
    {
        transform.position = startPos;
        Quaternion targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.0f);

    }

}
