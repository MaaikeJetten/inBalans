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

    public int timeLeftMin = 2;
    public int timeLeftSec = 0;
    private string timeLeftMinu = "2";
    private string timeLeftSeco = "00";
    public Text countdown;
    float countdownTime = 120.00f;
    string time;

    public string eventTrigger;

    // Start is called before the first frame update
    void Start()
    {
        countdownTime = (timeLeftMin*60)+timeLeftSec;
        countdown.text = timeLeftMin.ToString("0") + ":" + timeLeftSec.ToString("00");

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
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownTime <= 0)
        {
            countdown.text = "0:00";
        }
        else
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
        eventTrigger = "begin";
    }

    void Back() {
        SceneManager.LoadScene (sceneName:"Menu");
    }

    void Pause() {
        popup.SetActive(true);
        pause.SetActive(true);
    }

    void Doorgaan() {
        popup.SetActive(false);
        pause.SetActive(false);
        eventTrigger = "doorgaan";
    }

    void Restart() {
        eventTrigger = "restart";
    }
}
