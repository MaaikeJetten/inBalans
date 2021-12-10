using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float forwardSpeed;
    [SerializeField] private float diveSpeed;
    private float diveInput;

    private Vector3 forwardRelativeToSurfaceNormal; //For Look Rotation


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
    }
}
