using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupPlane : MonoBehaviour
{
    public GameObject popup;
    public GameObject uitleg;
    public GameObject pause;
    public GameObject success;
    public GameObject failure;

    public Button backButton;
    public Button beginButton;
    public Button pauseButton;
    public Button doorgaanButton;
    public Button volgendeButton;
    public Button restartButton;
    public Button volgendeButton2;
    public Button negerenButton;
    public Button opnieuwButton;

    string currentScene;

    public int timeLeftMin = 2;
    public int timeLeftSec = 0;
    private string timeLeftMinu = "2";
    private string timeLeftSeco = "00";
    public Text countdown;
    float countdownTime = 120.00f;
    bool countingDown = false;
    public Slider timeFill;

    public string countdownBool = "true";
    bool countDownBool;
    public float countdownBeginMax = 5;
    float countdownBegin;
    public float countdownRestartMax = 5;
    float countdownRestart;
    public Slider beginFill;
    public GameObject beginSlider;
    public Slider volgendeFill;
    public GameObject volgendeSlider;
    public Slider opnieuwFill;
    public GameObject opnieuwSlider;

    public BalanceBarVertical failRestart;

    public string eventTrigger;

    public PlaneController plane;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(failRestart);
        countdownTime = (timeLeftMin * 60) + timeLeftSec;
        timeFill.maxValue = countdownTime;
        timeFill.value = countdownTime;

        countDownBool = "true" == countdownBool;
        countdownBegin = countdownBeginMax;
        countdownRestart = countdownRestartMax;
        beginFill.maxValue = countdownBegin;
        beginFill.value = countdownBegin;
        volgendeFill.maxValue = countdownBegin;
        volgendeFill.value = countdownBegin;
        opnieuwFill.maxValue = countdownRestart;
        opnieuwFill.value = countdownRestart;

        Scene scene = SceneManager.GetActiveScene();
        currentScene = scene.name;

        popup.SetActive(true);
        uitleg.SetActive(true);
        pause.SetActive(false);
        success.SetActive(false);
        failure.SetActive(false);

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
        if (countDownBool)
        {
            if (uitleg.activeSelf)
            {
                BeginTimer();
            }
            else if (success.activeSelf)
            {
                VolgendeTimer();
            }
            else if (failure.activeSelf)
            {
                OpnieuwTimer();
            }
        }
        else
        {
            beginSlider.SetActive(false);
            volgendeSlider.SetActive(false);
            opnieuwSlider.SetActive(false);
        }

        if (countdownTime <= 0)
        {
            countdown.text = "0:00";
            popup.SetActive(true);
            success.SetActive(true);
            plane.Pause();
        }
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

        if (failRestart.failureRestart)
        {
            popup.SetActive(true);
            failure.SetActive(true);
            countingDown = false;
        }
    }

    void BeginTimer()
    {
        if (countdownBegin <= 0)
        {
            BeginGame();
        }
        else
        {
            countdownBegin -= Time.deltaTime;
            beginFill.value = countdownBegin;
        }
    }

    void VolgendeTimer()
    {
        if (countdownBegin <= 0)
        {
            LoadNext();
        }
        else
        {
            countdownBegin -= Time.deltaTime;
            volgendeFill.value = countdownBegin;
        }
    }

    void OpnieuwTimer()
    {
        if (countdownRestart <= 0)
        {
            Restart();
        }
        else
        {
            countdownRestart -= Time.deltaTime;
            opnieuwFill.value = countdownRestart;
        }
    }

    void BeginGame()
    {
        popup.SetActive(false);
        uitleg.SetActive(false);
        countingDown = true;
        eventTrigger = "begin";
        countdownBegin = countdownBeginMax;
        //plane.Begin();
    }

    void Back()
    {
        SceneManager.LoadScene(sceneName: "Menu");
    }

    void Pause()
    {
        popup.SetActive(true);
        pause.SetActive(true);
        countingDown = false;
        eventTrigger = "pause";
    }

    void Doorgaan()
    {
        popup.SetActive(false);
        pause.SetActive(false);
        failure.SetActive(false);
        failRestart.failureRestart = false;
        eventTrigger = "doorgaan";
        countingDown = true;
    }

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
                SceneManager.LoadScene(sceneName: "Menu");
                break;
            default:
                SceneManager.LoadScene(sceneName: "Menu");
                break;
        }
    }

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
        countdownRestart = countdownRestartMax;
    }
}
