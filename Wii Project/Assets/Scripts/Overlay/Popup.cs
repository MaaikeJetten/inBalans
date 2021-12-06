using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Popup : MonoBehaviour
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

    public int timeLeftMin = 2;
    public int timeLeftSec = 0;
    private string timeLeftMinu = "2";
    private string timeLeftSeco = "00";
    public Text countdown;
    float countdownTime = 120.00f;
    string time;
    bool countingDown = false;

    public string eventTrigger;

    // Start is called before the first frame update
    void Start()
    {
        countdownTime = (timeLeftMin*60)+timeLeftSec;

        popup.SetActive(true);
        uitleg.SetActive(true);
        pause.SetActive(false);
        success.SetActive(false);
        failure.SetActive(false);

        beginButton.onClick.AddListener(BeginGame);
        backButton.onClick.AddListener(Back);
        pauseButton.onClick.AddListener(Pause);
        doorgaanButton.onClick.AddListener(Doorgaan);
        volgendeButton.onClick.AddListener(Back);
        restartButton.onClick.AddListener(Restart);
        volgendeButton2.onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownTime <= 0)
        {
            countdown.text = "0:00";
            popup.SetActive(true);
            success.SetActive(true);
        }
        else if (countingDown)
        {
            countdownTime -= Time.deltaTime;
            timeLeftMin = (int)(countdownTime/60);
            timeLeftSec = (int)(countdownTime-(timeLeftMin*60));
            timeLeftMinu = timeLeftMin.ToString("0");
            timeLeftSeco = timeLeftSec.ToString("00");
            countdown.text = timeLeftMinu + ":" + timeLeftSeco;
        }
    }

    void BeginGame() {
        popup.SetActive(false);
        uitleg.SetActive(false);
        countingDown = true;
        eventTrigger = "begin";
    }

    void Back() {
        SceneManager.LoadScene (sceneName:"Menu");
    }

    void Pause() {
        popup.SetActive(true);
        pause.SetActive(true);
        countingDown = false;
    }

    void Doorgaan() {
        popup.SetActive(false);
        pause.SetActive(false);
        eventTrigger = "doorgaan";
        countingDown = true;
    }

    void Restart() {
        timeLeftMin = 2;
        timeLeftSec = 1;
        countdownTime = (timeLeftMin*60)+timeLeftSec;
        countingDown = true;
        eventTrigger = "restart";
    }
}
