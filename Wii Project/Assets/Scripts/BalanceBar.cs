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


    private RectTransform rtBar;
    private RectTransform rtPosition;
    private RectTransform rtTarget;


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
        rtBar = bar.transform.GetComponent<RectTransform>();
        rtPosition = positionBar.transform.GetComponent<RectTransform>();
        rtTarget = targetSection.transform.GetComponent<RectTransform>();

        

        startingPosition = rtPosition.transform.position;
        targetPosition = rtTarget.transform.position;

        speed = 1.5f;

        barWidth = rtBar.sizeDelta.x;
        positionBarWidth = rtPosition.sizeDelta.x;
        targetWidth = rtTarget.sizeDelta.x;

        positionBar.GetComponent<Image>().material.SetColor("_Color", Color.white);

        green = new Color(0.4f, 0.8f, 0.6f);

        errorTimer = 0;
    }

    private void Update()
    {
        //rtPosition.transform.position = startingPosition + new Vector3(positionRef.transform.localPosition.x,0f,0f);
        MoveBar();

        if (rtPosition.transform.position.x < targetPosition.x + (targetWidth / 2) && rtPosition.transform.position.x > targetPosition.x - (targetWidth / 2))
        {
            positionBar.GetComponentInChildren<Image>().color = green;

        }
        else if (rtPosition.transform.position.x - positionBarWidth/2 > startingPosition.x + (barWidth /2) - targetWidth && rtPosition.transform.position.x + positionBarWidth / 2 < startingPosition.x + (barWidth / 2) + 5)
        {
            positionBar.GetComponentInChildren<Image>().color = Color.red;
            errorTimer++;
           // Debug.Log(errorTimer);

            if (errorTimer * Time.deltaTime >= timer)
            {
                Restart();
                errorTimer = 0;
            }
        }
        else if (rtPosition.transform.position.x - positionBarWidth / 2 < startingPosition.x - (barWidth / 2) + targetWidth && rtPosition.transform.position.x - positionBarWidth / 2 > startingPosition.x - (barWidth / 2) - 5)
        {
            positionBar.GetComponentInChildren<Image>().color = Color.red;
            errorTimer++;
           // Debug.Log(errorTimer);

            if (errorTimer * Time.deltaTime >= timer)
            {
                Restart();
                errorTimer = 0;
            }
        }
        else
        {
            positionBar.GetComponentInChildren<Image>().color = Color.black;
            errorTimer = 0;
        }
            //Debug.Log(rtPosition.transform.position);
        
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

    public void Restart()
    {
        boat.Pause();
        failureRestart = true;
    }
}
