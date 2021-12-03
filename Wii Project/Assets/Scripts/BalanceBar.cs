using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceBar : MonoBehaviour
{
    public GameObject targetSection;
    public GameObject bar;
    public GameObject positionBar;


    private RectTransform rtBar;
    private RectTransform rtPosition;
    private RectTransform rtTarget;


    private Vector3 startingPosition;
    [SerializeField] private float speed;
    private Vector3 moveDirection;

    private float barWidth;
    private float positionBarWidth;



    private void Start()
    {
        rtBar = bar.transform.GetComponent<RectTransform>();
        rtPosition = positionBar.transform.GetComponent<RectTransform>();
        rtTarget = targetSection.transform.GetComponent<RectTransform>();

        

        startingPosition = rtPosition.transform.position;

        speed = 1f;

        barWidth = rtBar.sizeDelta.x;
        positionBarWidth = rtPosition.sizeDelta.x;
               
        
    }

    private void Update()
    {
        //rtPosition.transform.position = startingPosition + new Vector3(positionRef.transform.localPosition.x,0f,0f);
        MoveBar();

        Debug.Log(rtPosition.transform.position);
    }

    private void MoveBar()
    {
        float x = Input.GetAxis("Horizontal");

        if (x != 0)
        {
            if (rtPosition.transform.position.x + positionBarWidth / 2 < startingPosition.x + (barWidth / 2) && rtPosition.transform.position.x - positionBarWidth / 2 > startingPosition.x - barWidth / 2)
            {
                rtPosition.transform.Translate(new Vector3(x*speed, 0f, 0f));
            }
            else if (rtPosition.transform.position.x + positionBarWidth / 2 >= startingPosition.x + (barWidth / 2))
            {
                rtPosition.transform.Translate(new Vector3(-.1f, 0f, 0f));
            }
            else if (rtPosition.transform.position.x - positionBarWidth / 2 <= startingPosition.x - barWidth / 2)
            {
                rtPosition.transform.Translate(new Vector3(.1f, 0f, 0f));
            }
        }

        if (x == 0)
        {
            if (rtPosition.transform.position.x > startingPosition.x)
                rtPosition.transform.Translate(new Vector3(-1.3f, 0f, 0f));
            if (rtPosition.transform.position.x < startingPosition.x)
                rtPosition.transform.Translate(new Vector3(1.3f, 0f, 0f));

        }
    }
}
