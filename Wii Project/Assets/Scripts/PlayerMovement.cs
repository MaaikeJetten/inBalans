using UnityEngine;

//Player movement script for the balancing left-right movement on the boat
public class PlayerMovement : MonoBehaviour
{
    //VARIABLES
    [SerializeField] public float moveSpeed;

    private Vector3 moveDirection;

    private Vector3 forwardRelativeToSurfaceNormal;//For Look Rotation

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

        Debug.Log("Horizontal = " + x);
        Debug.Log("Vertical = " + y );

        moveDirection = new Vector3(x, y, 0);
        moveDirection *= moveSpeed * 1.5f;

        if (x != 0)
        {
            //Rotate limitation left/right with A & D
            if (transform.localEulerAngles.z <= 30 || transform.localEulerAngles.z >= 330)
            {
                transform.Rotate(0f, 0f, -moveDirection.x, Space.World);
            }
            else if (transform.localEulerAngles.z > 30 && transform.localEulerAngles.z < 100)
            {
                transform.Rotate(0f, 0f, -0.1f, Space.World);
            }
            else if (transform.localEulerAngles.z < 330)
            {
                transform.Rotate(0f, 0f, 0.1f, Space.World);
            }

            
        }

        if (y != 0)
        {
            //Rotate limitation front/back with W & S
            if (transform.localEulerAngles.x <= 30 || transform.localEulerAngles.x >= 330)
            {
                transform.Rotate(moveDirection.y, 0f, 0f, Space.Self);
            }
            else if (transform.localEulerAngles.x > 30 && transform.localEulerAngles.x < 100)
            {
                transform.Rotate(-0.1f, 0f, 0f, Space.Self);
            }
            else if (transform.localEulerAngles.x < 330)
            {
                transform.Rotate(0.1f, 0f, 0f, Space.Self);
            }
        }

        if (x == 0 && y == 0)
        {
            CharacterStandStraight();
           
        }

    }

    void CharacterStandStraight()
    {
        //For Detect The Base/Surface.
       
            Quaternion targetRotation = Quaternion.LookRotation(forwardRelativeToSurfaceNormal, Vector3.up); //check For target Rotation.
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2); //Rotate Character accordingly.
        
    }
}
