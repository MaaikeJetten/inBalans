using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceBar : MonoBehaviour
{
    public GameObject targetSection;
    public GameObject bar;
    public GameObject positionBar;
    public Boat boat;


    private RectTransform rtBar; //full size of bar
    private RectTransform rtPosition; //current position bar
    private RectTransform rtTarget; //target bar


    private Vector3 startingPosition;
    private Vector3 targetPosition;
    [SerializeField] private float speed;
    private Vector3 moveDirection;

    private float barWidth;
    private float positionBarWidth;
    private float targetWidth;

    Color green;

    private float errorTimer;
    [SerializeField] private float timer;

    public bool failureRestart = false;

    private void Start()
    {
        //get components
        rtBar = bar.transform.GetComponent<RectTransform>();
        rtPosition = positionBar.transform.GetComponent<RectTransform>();
        rtTarget = targetSection.transform.GetComponent<RectTransform>();

        

        startingPosition = rtPosition.transform.position;
        targetPosition = rtTarget.transform.position;

        speed = 2f;

        //get correct sizes
        barWidth = rtBar.sizeDelta.x;
        positionBarWidth = rtPosition.sizeDelta.x;
        targetWidth = rtTarget.sizeDelta.x;

        positionBar.GetComponent<Image>().material.SetColor("_Color", Color.white);

        green = new Color(0.157f, 0.4f, 0.431f);

        errorTimer = 0;
    }

    private void Update()
    {
        MoveBar();

        //if position bar is within the target bar, the position bar is white
        if (rtPosition.transform.position.x < targetPosition.x + (targetWidth / 2) && rtPosition.transform.position.x > targetPosition.x - (targetWidth / 2))
        {
            positionBar.GetComponentInChildren<Image>().color = Color.white;

        }
        //if position bar is at the ends of the larger bar a.k.a far from the target in a wrong position a timer starts
        else if (rtPosition.transform.position.x - positionBarWidth/2 > startingPosition.x + (barWidth /2) - targetWidth && rtPosition.transform.position.x + positionBarWidth / 2 < startingPosition.x + (barWidth / 2) + 5)
        {
            errorTimer++;

            if (errorTimer * Time.deltaTime >= timer)
            {
                Restart();
                errorTimer = 0;
            }
        }
        else if (rtPosition.transform.position.x - positionBarWidth / 2 < startingPosition.x - (barWidth / 2) + targetWidth && rtPosition.transform.position.x - positionBarWidth / 2 > startingPosition.x - (barWidth / 2) - 5)
        {
            errorTimer++;

            if (errorTimer * Time.deltaTime >= timer)
            {
                Restart();
                errorTimer = 0;
            }
        }
        else //if position bar is not in the target, the position bar is green
        {
            positionBar.GetComponentInChildren<Image>().color = green;
            errorTimer = 0;
        }
            //Debug.Log(rtPosition.transform.position);
        
    }

    private void MoveBar()
    {
        float x = Input.GetAxis("Horizontal");

        if (x != 0)
        {
            //move position bar according to player input --> THIS SHOULD BE MORE SENSITIVE
            if (rtPosition.transform.position.x + positionBarWidth / 2 < startingPosition.x + (barWidth / 2) && rtPosition.transform.position.x - positionBarWidth / 2 > startingPosition.x - barWidth / 2)
            {
                rtPosition.transform.Translate(new Vector3(x*speed, 0f, 0f));
            }
            //position bar is limited to the bounds of the overall bar
            else if (rtPosition.transform.position.x + positionBarWidth / 2 >= startingPosition.x + (barWidth / 2))
            {
                rtPosition.transform.Translate(new Vector3(-.1f, 0f, 0f));
            }
            else if (rtPosition.transform.position.x - positionBarWidth / 2 <= startingPosition.x - barWidth / 2)
            {
                rtPosition.transform.Translate(new Vector3(.1f, 0f, 0f));
            }
        }

        //return position bar to the center without player input
        if (x == 0)
        {
            if (rtPosition.transform.position.x > startingPosition.x)
                rtPosition.transform.Translate(new Vector3(-1.3f*2, 0f, 0f));
            if (rtPosition.transform.position.x < startingPosition.x)
                rtPosition.transform.Translate(new Vector3(1.3f*2, 0f, 0f));

        }
    }

    public void Restart()
    {
        boat.Pause();
        failureRestart = true;
    }
}
