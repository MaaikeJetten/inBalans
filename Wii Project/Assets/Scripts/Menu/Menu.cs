using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject start;
    public GameObject select;
    public Button startButton;

    public Button trainingButton;
    public Button losseButton;

    // Start is called before the first frame update
    void Start()
    {
        start.SetActive(true);
        select.SetActive(false);

        startButton.onClick.AddListener(TaskOnClick);

        trainingButton.onClick.AddListener(TrainingMode);
        losseButton.onClick.AddListener(LosseMode);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick() {
        start.SetActive(false);
        select.SetActive(true);
        trainingButton.Select();
    }

    void TrainingMode() {
        trainingButton.Select();
    }

    void LosseMode() {
        losseButton.Select();
    }
}
