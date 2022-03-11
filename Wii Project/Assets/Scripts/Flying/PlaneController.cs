using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float forwardSpeed;
    private float speed;
    public float diveSpeed;
    private float lowRing;
    private float highRing;
    private float diveInput;
    private Vector3 startPos;
    [HideInInspector] public bool play;
    private string eventTrigger = "";
    public PopupPlane popUp;

    private Vector3 forwardRelativeToSurfaceNormal; //For Look Rotation
    private Quaternion targetRotation;

    public GameObject propellor;

    public RingGeneration ringG;
    private GameObject[] rings;
    [HideInInspector] public int lives;
    private bool looseLife;
    [HideInInspector] public bool high;
    [HideInInspector] public bool low;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(transform.position.x, ringG.ringMiddle-3, transform.position.z);
        play = false;
        speed = 0;

        lives = 3;
        looseLife = true;  //player able to loose lives

        //assign ring values
        lowRing = ringG.ringLow;
        highRing = ringG.ringHigh;
        rings = ringG.ringArray;
    }

    // Update is called once per frame
    void Update()
    {
        eventTrigger = popUp.eventTrigger;
        
        if (play)
        {
            
            diveInput = Input.GetAxisRaw("Vertical");

            //No specific input
            if (diveInput < 0.5 && diveInput > -0.5)
            {
                //Diving motion to center
                if(transform.position.y > startPos.y)
                {
                    targetRotation = Quaternion.Euler(45, 0, 0);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);

                    transform.Translate(new Vector3(0f, -diveSpeed, 0f), Space.World);
                }

                //Rising motion to center
                if (transform.position.y < startPos.y)
                {
                    targetRotation = Quaternion.Euler(-45, 0, 0);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);

                    transform.Translate(new Vector3(0f, diveSpeed, 0f), Space.World);
                }

                //Straighten out
                if(transform.position.y == startPos.y)
                {
                    targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
                }
                

            }
            //Rising
           else if (diveInput <= -0.7)
            {
                //turn plane
                if (transform.position.y < highRing - 5) 
                { 
                    targetRotation = Quaternion.Euler(-45, 0, 0);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
                }
                else //Straighten out
                {
                    targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
                }

                if(transform.position.y < highRing - 3)
                {
                    transform.Translate(new Vector3(0f, diveSpeed, 0f), Space.World);
                }


            }
            //Diving
            else if (diveInput >= 0.7)
            {
                //turn plane
                if (transform.position.y > lowRing + 5)
                {
                    targetRotation = Quaternion.Euler(45, 0, 0);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
                }
                else //Straighten out
                {
                    targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
                }

                if (transform.position.y > lowRing - 3)
                {
                    transform.Translate(new Vector3(0f, -diveSpeed, 0f), Space.World);
                }
            }

            transform.Translate(0f, 0f, speed, Space.World);


            //Check type of ring and if player goes through
            foreach (GameObject ring in rings)
            {
                float distanceZ = ring.transform.position.z - transform.position.z;
                float distanceY = ring.transform.position.y - transform.position.y;

                if (distanceZ <= ringG.distanceBetween & distanceZ >= -5)
                {
                    //Check if high or low
                    if (ring.transform.position.y >= startPos.y + 5) {high = true; low = false; }
                    else if (ring.transform.position.y <= startPos.y - 5) { high = false; low = true; }
                    else { high = false; low = false; }

                }

                //Check if plane close to ring
                if (distanceZ <= 5 && distanceZ >= -5)
                {
                    //Debug.Log(ring + "close by");
                    if (distanceY <= ring.GetComponent<MeshRenderer>().bounds.size.x / 2 + 7 && distanceY >= -ring.GetComponent<MeshRenderer>().bounds.size.x / 2 - 7)
                    {
                        //Plane passes through ring
                    }
                    else if (distanceY > ring.GetComponent<MeshRenderer>().bounds.size.x / 2 + 7 || distanceY < -ring.GetComponent<MeshRenderer>().bounds.size.x / 2 - 7)
                    {
                        //Plane misses ring
                        if (looseLife)
                        {
                            looseLife = false;
                            lives--;
                            Debug.Log(lives);
                        }

                    }
                }

                else if (distanceZ < -10 && distanceZ > -20) looseLife = true; //player can loose lives again

               
            }


            propellor.transform.Rotate(0f, -10f, 0f, Space.Self);

            //Flying incorrectly
            if (lives == 0)
            {
                popUp.failRestart.failureRestart = true;
                speed = 0;
                Pause();

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
