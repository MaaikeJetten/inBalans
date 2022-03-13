using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //script for "menu" scene

    public GameObject start; //start screen
    public GameObject select; //select level screen
    public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        start.SetActive(true); //start screen is shown when the "menu" scene is loaded
        select.SetActive(false);

        startButton.onClick.AddListener(TaskOnClick); //when start is clicked, the select level screen is shown
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick() {
        start.SetActive(false);
        select.SetActive(true);
    }
}
