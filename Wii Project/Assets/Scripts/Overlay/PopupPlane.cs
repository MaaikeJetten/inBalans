using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupPlane : MonoBehaviour
{
    //script for the overlay in the scenes level 6, 7 and 8

    //popup and different parts of it - uitleg (explanation), pause, success and failure
    public GameObject popup;
    public GameObject uitleg;
    public GameObject pause;
    public GameObject success;
    public GameObject failure;

    //all the buttons in the various types of popups
    public Button backButton;
    public Button beginButton;
    public Button doorgaanButton;
    public Button volgendeButton;
    public Button volgendeButton2;
    public Button negerenButton;
    public Button opnieuwButton;
    //all the buttons visible while playing the game
    public Button pauseButton;
    public Button restartButton;

    string currentScene; //saves which level is currently being played

    //variables used to keep track of the time in the game and on-screen output in the timer
    public int timeLeftMin = 2;
    public int timeLeftSec = 0;
    private string timeLeftMinu = "2";
    private string timeLeftSeco = "00";
    public Text countdown;
    float countdownTime = 120.00f;
    bool countingDown = false;
    public Slider timeFill;

    public BalanceBarVertical failRestart; //check in BalanceBar script if the level was failed and needs to reset

    public string eventTrigger; //communicate what needs to happen

    public PlaneController plane; //call in plane from PlaneController script

    // Start is called before the first frame update
    void Start()
    {
        //set the time for the countdown of the scene
        Debug.Log(failRestart);
        countdownTime = (timeLeftMin * 60) + timeLeftSec;
        timeFill.maxValue = countdownTime;
        timeFill.value = countdownTime;

        //save which scene is currently open
        Scene scene = SceneManager.GetActiveScene();
        currentScene = scene.name;

        //show the explanation popup
        popup.SetActive(true);
        uitleg.SetActive(true);
        pause.SetActive(false);
        success.SetActive(false);
        failure.SetActive(false);

        //check whether any button has been clicked
        beginButton.onClick.AddListener(BeginGame);
        backButton.onClick.AddListener(Back);
        pauseButton.onClick.AddListener(Pause);
        doorgaanButton.onClick.AddListener(Doorgaan);
        volgendeButton.onClick.AddListener(LoadNext);
        restartButton.onClick.AddListener(Restart);
        volgendeButton2.onClick.AddListener(LoadNext);
        negerenButton.onClick.AddListener(Doorgaan);
        opnieuwButton.onClick.AddListener(Restart);
    }

    // Update is called once per frame
    void Update()
    {
        //if the countdown time reaches 0, the success popup should be activated
        if (countdownTime <= 0)
        if (countdownTime <= 0)
        {
            countdown.text = "0:00";
            popup.SetActive(true);
            success.SetActive(true);
            plane.Pause();
        }
        //if countingdown is true, the game has started, just like the countdown
        else if (countingDown)
        {
            countdownTime -= Time.deltaTime;
            timeLeftMin = (int)(countdownTime / 60);
            timeLeftSec = (int)(countdownTime - (timeLeftMin * 60));
            timeLeftMinu = timeLeftMin.ToString("0");
            timeLeftSeco = timeLeftSec.ToString("00");
            countdown.text = timeLeftMinu + ":" + timeLeftSeco;
            timeFill.value = countdownTime;
        }

        //if the player failed the level, the failure popup is activated and countingdown has paused
        if (failRestart.failureRestart)
        {
            popup.SetActive(true);
            failure.SetActive(true);
            countingDown = false;
        }
    }

    //if the begin button has been clicked, the game begins and the countdown starts
    void BeginGame()
    {
        popup.SetActive(false);
        uitleg.SetActive(false);
        countingDown = true;
        eventTrigger = "begin";
    }

    //if the back button has been clicked, the "menu" scene is loaded
    void Back()
    {
        SceneManager.LoadScene(sceneName: "Menu");
    }

    //if the pause button has been clicked, the pause popup is activated and countdown is paused
    void Pause()
    {
        popup.SetActive(true);
        pause.SetActive(true);
        countingDown = false;
        eventTrigger = "pause";
    }

    //if the doorgaan button has been clicked, the game continues counting down
    void Doorgaan()
    {
        popup.SetActive(false);
        pause.SetActive(false);
        failure.SetActive(false);
        failRestart.failureRestart = false;
        eventTrigger = "doorgaan";
        countingDown = true;
    }

    //if volgende has been clicked, the next scene (level) is loaded based on the current scene
    void LoadNext()
    {
        switch (currentScene)
        {
            case "Level 1_Staan":
                SceneManager.LoadScene(sceneName: "Level 2_Staan");
                break;
            case "Level 2_Staan":
                SceneManager.LoadScene(sceneName: "Level 3_Staan");
                break;
            case "Level 3_Staan":
                SceneManager.LoadScene(sceneName: "Level 6_Hielen heffen");
                break;
            case "Level 6_Hielen heffen":
                SceneManager.LoadScene(sceneName: "Level 7_Hielen tenen heffen");
                break;
            case "Level 7_Hielen tenen heffen":
                SceneManager.LoadScene(sceneName: "Level 8_Hielen heffen");
                break;
            case "Level 8_Hielen heffen":
                SceneManager.LoadScene(sceneName: "Menu");
                break;
            default:
                SceneManager.LoadScene(sceneName: "Menu");
                break;
        }
    }

    //if the level is restarted, the countdown is reset, and the plane is told it needs to start again
    void Restart()
    {
        popup.SetActive(false);
        failure.SetActive(false);
        failRestart.failureRestart = false;
        timeLeftMin = 2;
        timeLeftSec = 1;
        countdownTime = (timeLeftMin * 60) + timeLeftSec;
        countingDown = true;
        eventTrigger = "restart";
        plane.Begin();
    }
}
