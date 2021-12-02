using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private Vector3 moveDelta;
    private Vector3 size;
    private MeshRenderer renderer;
    public int duration;
    private float moveSpeed;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        size = renderer.bounds.size;
        moveSpeed = (size.z+100) / (duration);
    }

    private void FixedUpdate()
    {
        if ((size.z / 2) > (transform.position.z * -1))
            transform.Translate(0, 0, -moveSpeed * Time.deltaTime, Space.World);
       
    }
}
