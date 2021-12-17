using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceBarVertical : MonoBehaviour
{
    public GameObject targetSection;
    public GameObject bar;
    public GameObject positionBar;
    // public Boat boat;
    public PlaneController plane;


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

        speed = 2f;

        barWidth = rtBar.sizeDelta.x;
        positionBarWidth = rtPosition.sizeDelta.x;
        targetWidth = rtTarget.sizeDelta.x;

        positionBar.GetComponent<Image>().material.SetColor("_Color", Color.white);

        //green = new Color(0.012f, 0.807f, 0.643f);
        green = new Color(0.157f, 0.4f, 0.431f);

        errorTimer = 0;
    }

    private void Update()
    {
        //rtPosition.transform.position = startingPosition + new Vector3(positionRef.transform.localPosition.x,0f,0f);
        MoveBar();

        if (rtPosition.transform.position.y < targetPosition.y + (targetWidth / 2) && rtPosition.transform.position.y > targetPosition.y - (targetWidth / 2))
        {
            positionBar.GetComponentInChildren<Image>().color = Color.white;

        }
        else if (rtPosition.transform.position.y - positionBarWidth / 2 > startingPosition.y + (barWidth / 2) - targetWidth && rtPosition.transform.position.y + positionBarWidth / 2 < startingPosition.y + (barWidth / 2) + 5)
        {
            // positionBar.GetComponentInChildren<Image>().color = Color.red;
            errorTimer++;
            // Debug.Log(errorTimer);

            if (errorTimer * Time.deltaTime >= timer)
            {
                Restart();
                errorTimer = 0;
            }
        }
        else if (rtPosition.transform.position.y - positionBarWidth / 2 < startingPosition.y - (barWidth / 2) + targetWidth && rtPosition.transform.position.y - positionBarWidth / 2 > startingPosition.y - (barWidth / 2) - 5)
        {
            // positionBar.GetComponentInChildren<Image>().color = Color.red;
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
            positionBar.GetComponentInChildren<Image>().color = green;
            errorTimer = 0;
        }
        //Debug.Log(rtPosition.transform.position);

    }

    private void MoveBar()
    {
        float x = Input.GetAxis("Vertical");

        if (x != 0)
        {
            if (rtPosition.transform.position.y + positionBarWidth / 2 < startingPosition.y + (barWidth / 2) && rtPosition.transform.position.y - positionBarWidth / 2 > startingPosition.y - barWidth / 2)
            {
                rtPosition.transform.Translate(new Vector3(x * speed, 0f, 0f));
            }
            else if (rtPosition.transform.position.y + positionBarWidth / 2 >= startingPosition.y + (barWidth / 2))
            {
                rtPosition.transform.Translate(new Vector3(-.001f, 0f, 0f));
            }
            else if (rtPosition.transform.position.y - positionBarWidth / 2 <= startingPosition.y - barWidth / 2)
            {
                rtPosition.transform.Translate(new Vector3(.001f, 0f, 0f));
            }
        }

        if (x == 0)
        {
            if (rtPosition.transform.position.y < startingPosition.y)
                rtPosition.transform.Translate(new Vector3(-1.3f*2, 0f, 0f));
            if (rtPosition.transform.position.y > startingPosition.y)
                rtPosition.transform.Translate(new Vector3(1.3f*2, 0f, 0f));

        }
    }

    public void Restart()
    {
        plane.Pause();
        failureRestart = true;
    }
}
