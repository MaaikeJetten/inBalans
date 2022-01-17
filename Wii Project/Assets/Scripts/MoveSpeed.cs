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
    }
}
