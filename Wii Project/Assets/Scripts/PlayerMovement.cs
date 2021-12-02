using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //VARIABLES
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;

    //REFERENCES

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveDirection = new Vector3(x, y, 0);
        moveDirection *= moveSpeed;


        //Rotate limitation left/right with A & D
        if (transform.localEulerAngles.z <= 30 || transform.localEulerAngles.z >= 330)
        {
            transform.Rotate(0f, 0f, -moveDirection.x, Space.World);
        } 
        else if (transform.localEulerAngles.z > 30 && transform.localEulerAngles.z < 100)
        {
            transform.Rotate(0f, 0f, -5f, Space.World);
        }
        else if (transform.localEulerAngles.z < 330)
        {
            transform.Rotate(0f, 0f, 5f, Space.World);
        }

        //Rotate limitation front/back with W & S
        if (transform.localEulerAngles.x <= 30 || transform.localEulerAngles.x >= 330)
        {
            transform.Rotate(moveDirection.y, 0f, 0f, Space.Self);
        }
        else if (transform.localEulerAngles.x > 30 && transform.localEulerAngles.x < 100)
        {
            transform.Rotate(-5f, 0f, 0f, Space.Self);
        }
        else if (transform.localEulerAngles.x < 330)
        {
            transform.Rotate(5f, 0f, 0f, Space.Self);
        }
        
        //Debug.Log(transform.localEulerAngles);

    }

    void Rotate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveDirection = new Vector3(x, y, 0);
        moveDirection *= moveSpeed;

        if (x > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 2f * Time.deltaTime);
        else if (x < 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 2f * Time.deltaTime);
        if (y > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 2f * Time.deltaTime);
        else if (y < 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 2f * Time.deltaTime);
    }
}
