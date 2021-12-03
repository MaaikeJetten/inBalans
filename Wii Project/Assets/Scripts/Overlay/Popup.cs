using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public GameObject popup;
    public GameObject uitleg;
    public GameObject pause;
    public GameObject success;
    public GameObject failure;

    public Button begin;

    public string eventTrigger;

    // Start is called before the first frame update
    void Start()
    {
        popup.SetActive(true);
        uitleg.SetActive(true);
        pause.SetActive(false);
        success.SetActive(false);
        failure.SetActive(false);

        begin.onClick.AddListener(BeginGame);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void BeginGame() {
        popup.SetActive(false);
        eventTrigger = "begin";
    }
}
