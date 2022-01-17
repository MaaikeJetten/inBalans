using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSpeed : MonoBehaviour
{
    public PlayerMovement player;
    private float speed = 0.15f;
    public Text speedText = "0.15";
    private string moveSpeedText;

    // Start is called before the first frame update
    void Start()
    {
        player.moveSpeed = speed;
        moveSpeedText = speed.ToString("0.00");
        speedText.text = moveSpeedText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
