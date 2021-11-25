using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Vector3 moveDelta;

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float moveSpeed = 0.5f;

        moveDelta = new Vector3(x, y, 0);

        transform.Rotate(0f, 0f, -moveDelta.x*moveSpeed, Space.World);
    }
}
