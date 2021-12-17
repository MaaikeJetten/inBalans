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

    public GameObject propellor;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        play = false;
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

            diveInput = Input.GetAxisRaw("Vertical") * diveSpeed;

            if (diveInput != 0)
            {
                transform.Rotate(diveInput, 0f, 0f, Space.Self);
            }
            else if (diveInput == 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2);
            }

            transform.Translate(0f, 0f, forwardSpeed, Space.Self);

            propellor.transform.Rotate(0f, -10f, 0f, Space.Self);

            if (transform.position.y <= 0 || transform.position.y >= 120)
            {
                popUp.failRestart.failureRestart = true;
                Pause();
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
