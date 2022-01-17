using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSpeed : MonoBehaviour
{
    public PlayerMovement player;
    private float speed = 0.15f;
    public Text speedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.moveSpeed = speed;
        speedText.text = speed.ToString("0.00");

        if (Input.GetKeyDown("k") && speed > 0.05f)
        {
            speed -= 0.01f;
        }

        if (Input.GetKeyDown("l") && speed < 0.20f)
        {
            speed += 0.01f;
        }
    }
}
