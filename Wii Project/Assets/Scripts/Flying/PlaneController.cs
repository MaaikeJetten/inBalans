using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float forwardSpeed;
    private float speed;
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
    public int lives;
    private bool looseLife;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        play = false;
        speed = 0;

        lives = 3;
        looseLife = true;
    }

    // Update is called once per frame
    void Update()
    {
        eventTrigger = popUp.eventTrigger;

        if (eventTrigger == "begin")
        {
            play = true;
            speed = forwardSpeed;
        }

        if (eventTrigger == "pause")
        {
            play = false;
            speed = 0;
        }


        if (play)
        {
            
            diveInput = Input.GetAxisRaw("Vertical");
            // Vector3 startPosition = transform.position;
            //targetHigh = new Vector3(0f, 65, transform.position.z);

            //Debug.Log(diveInput);


            if (diveInput == 0)
            {
               // targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
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
            
            transform.Translate(0f, 0f, speed, Space.Self);

            foreach (GameObject ring in rings)
            {
                float distanceZ = ring.transform.position.z - transform.position.z;
                float distanceY = ring.transform.position.y - transform.position.y;
                //Check if plane close to ring
                if (distanceZ <= 5 && distanceZ >= -5)
                {
                    //Debug.Log(ring + "close by");
                    if (distanceY <= ring.GetComponent<MeshRenderer>().bounds.size.x / 2 + 5 && distanceY >= -ring.GetComponent<MeshRenderer>().bounds.size.x / 2)
                    {
                        //Debug.Log("hit");
                    }
                    else if (distanceY > ring.GetComponent<MeshRenderer>().bounds.size.x / 2 + 5 || distanceY < -ring.GetComponent<MeshRenderer>().bounds.size.x / 2)
                    {
                        //Debug.Log("miss");
                        if (looseLife)
                        {
                            looseLife = false;
                            lives--;
                            Debug.Log(lives);
                        }

                    }
                }
                else if (distanceZ < -6 && distanceZ > -10) looseLife = true;

               
            }


            propellor.transform.Rotate(0f, -10f, 0f, Space.Self);

            if (transform.position.y <= 0 || transform.position.y >= 120 || lives == 0)
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
        speed = forwardSpeed;
        lives = 3;
        RestartPosition();

    }

    public void Pause()
    {
        play = false;
        speed = 0;
    }

    public void Doorgaan()
    {
        play = true;
        speed = forwardSpeed;
    }

    public void RestartPosition()
    {
        transform.position = startPos;
        Quaternion targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.0f);

    }

  
}
