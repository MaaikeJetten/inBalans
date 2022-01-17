using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    public Button[] scene;

    // Start is called before the first frame update
    void Start()
    {
        scene[0].onClick.AddListener(Load1);
        scene[1].onClick.AddListener(Load2);
        scene[2].onClick.AddListener(Load3);
        scene[3].onClick.AddListener(Load6);
        scene[4].onClick.AddListener(Load7);
        scene[5].onClick.AddListener(Load8);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Load1() {
        SceneManager.LoadScene (sceneName:"Level 1_Staan");
    }
    void Load2() {
        SceneManager.LoadScene (sceneName:"Level 2_Staan");
    }
    void Load3() {
        SceneManager.LoadScene (sceneName:"Level 3_Staan");
    }
    void Load6()
    {
        SceneManager.LoadScene(sceneName: "Level 6_Hielen heffen");
    }
    void Load7()
    {
        SceneManager.LoadScene(sceneName: "Level 7_Hielen tenen heffen");
    }
    void Load8()
    {
        SceneManager.LoadScene(sceneName: "Level 8_Hielen heffen");
    }
}
