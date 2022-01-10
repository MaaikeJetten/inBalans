using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float forwardSpeed;
    private float speed;
    [SerializeField] private float diveSpeed;
    [SerializeField] private float lowRing;
    [SerializeField] private float highRing;
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
    public bool high;
    public bool low;

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



        if (play)
        {
            
            diveInput = Input.GetAxisRaw("Vertical");


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
            
            transform.Translate(0f, 0f, speed, Space.Self);


            //Check type of ring and if player goes through
            foreach (GameObject ring in rings)
            {
                float distanceZ = ring.transform.position.z - transform.position.z;
                float distanceY = ring.transform.position.y - transform.position.y;

                if (distanceZ <= 200 & distanceZ >= -5)
                {
                    //Check if high or low
                    if (ring.transform.position.y > startPos.y + 5) {high = true; low = false; }
                    else if (ring.transform.position.y < startPos.y - 5) { high = false; low = true; }
                    else { high = false; low = false; }

                    Debug.Log("high = " + high + "    low = " + low);

                }

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

            //Flying incorrectly
            if (transform.position.y <= 10 || transform.position.y >= 120 || lives == 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.0f);
                popUp.failRestart.failureRestart = true;
                speed = 0;
                Pause();
                //play = false;

                //Reposition slightly to not immediately call failure again
                if (transform.position.y <= 10)
                {
                    Vector3 restartPosition = new Vector3(transform.position.x, lowRing+15, transform.position.z);
                    transform.position = restartPosition;
                }
                if (transform.position.y >= 120)
                { 
                    Vector3 restartPosition = new Vector3(transform.position.x, highRing-15, transform.position.z);
                    transform.position = restartPosition;
                }
                if(lives == 0)
                {
                    lives = 3;
                }
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
        Quaternion targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.0f);
        transform.position = startPos;
        diveInput = 0;
        

    }

  
}
