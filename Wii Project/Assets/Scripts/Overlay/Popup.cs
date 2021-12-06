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

    public string eventTrigger;

    // Start is called before the first frame update
    void Start()
    {
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
