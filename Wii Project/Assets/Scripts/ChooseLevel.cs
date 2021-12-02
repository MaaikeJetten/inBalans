using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    public Button scene1;

    // Start is called before the first frame update
    void Start()
    {
        scene1.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick() {
        SceneManager.LoadScene (sceneName:"Level 1_Staan");
    }
}
