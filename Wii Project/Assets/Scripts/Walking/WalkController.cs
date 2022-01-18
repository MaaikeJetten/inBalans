using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour
{
    public float forwardSpeed;
    private float speed;
    public float diveSpeed;
    //[SerializeField] private float diveSpeed;
    private float lowRing;
    private float highRing;
    private float walkInput;
    private float mappedHeight;
    [SerializeField] private float modelComp;
    private Vector3 startPos;
    [HideInInspector] public bool play;
    private string eventTrigger = "";
   // public PopupPlane popUp;

    private Vector3 forwardRelativeToSurfaceNormal; //For Look Rotation
    private Quaternion targetRotation;

   // public GameObject propellor;

    //public RingGeneration ringG;
    private GameObject[] rings;
    [HideInInspector] public int lives;
    private bool looseLife;
    [HideInInspector] public bool high;
    [HideInInspector] public bool low;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        play = false;
        speed = 0;

        lives = 3;
        looseLife = true;

       // lowRing = ringG.ringLow;
      //  highRing = ringG.ringHigh;
      //  rings = ringG.ringArray;
    }

    // Update is called once per frame
    void Update()
    {
       // eventTrigger = popUp.eventTrigger;
        


        if (play)
        {
            
            walkInput = Input.GetAxisRaw("Horizontal");
            Debug.Log(walkInput);


            if (walkInput < 0.5 && walkInput > -0.5)
            {
                //Diving motion to center
                if(transform.position.x > startPos.x)
                {
                    targetRotation = Quaternion.Euler(0, 45, 0);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);

                   // transform.Translate(new Vector3(0f, -speed, 0f), Space.World);
                }

                //Rising motion to center
                if (transform.position.y < startPos.y)
                {
                    targetRotation = Quaternion.Euler(0, -45, 0);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);

                    //transform.Translate(new Vector3(0f, speed, 0f), Space.World);
                }

                //Straighten out
                if(transform.position.y == startPos.y)
                {
                    targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
                }
                

            }
            //Rising
           else if (walkInput <= -0.7)
            {
                if (transform.position.x < 30) 
                { 
                    targetRotation = Quaternion.Euler(0, -45, 0);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
                }
                else //Straighten out
                {
                    targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
                }

                if(transform.position.y < 33)
                {
                   // transform.Translate(new Vector3(0f, speed, 0f), Space.Self);
                }


            }
            //Diving
            else if (walkInput >= 0.7)
            {
                if (transform.position.x > -30)
                {
                    targetRotation = Quaternion.Euler(0, 45, 0);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
                }
                else //Straighten out
                {
                    targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
                }

                if (transform.position.y > -33)
                {
                    //transform.Translate(new Vector3(0f, -diveSpeed, 0f), Space.World);
                }
            }

           // mappedHeight = walkInput.Map(1, -1, lowRing-modelComp, highRing-modelComp);
           //    mappedHeight = 50;
            

            //Vector3 newPosition = new Vector3(transform.position.x, mappedHeight, transform.position.z);
            //transform.position = newPosition;

            transform.Translate(0f, 0f, speed, Space.Self);


            


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
        walkInput = 0;
        

    }

  
}
