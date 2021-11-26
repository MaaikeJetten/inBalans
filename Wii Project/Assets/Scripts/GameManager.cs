using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject tilePrefab;

    Vector3 spawnPS = new Vector3(0, 0, 123);

    private void Awake()
    {
        instance = this;
    }

    public void CreateNewTile()
    {
        Instantiate(tilePrefab, spawnPS, Quaternion.identity);
    }
}
