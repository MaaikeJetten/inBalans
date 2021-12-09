using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float forwardSpeed;
    [SerializeField] private float diveSpeed;
    private float diveInput;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        diveInput = Input.GetAxisRaw("Vertical") * diveSpeed;

        transform.Rotate(diveInput, 0f, 0f, Space.Self);
        transform.Translate(0f, 0f, forwardSpeed, Space.Self);

    }
}
