using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartLives : MonoBehaviour
{
    public PlaneController plane;
    public GameObject[] hearts;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //set icons of hearts to current lives
        switch (plane.lives) {
            case 0:
                hearts[0].SetActive(false);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                break;
            case 1:
                hearts[0].SetActive(true);
                hearts[1].SetActive(false);
                hearts[2].SetActive(false);
                break;
            case 2:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(false);
                break;
            case 3:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
                break;
            default:
                hearts[0].SetActive(true);
                hearts[1].SetActive(true);
                hearts[2].SetActive(true);
                break;
        }
    }
}
