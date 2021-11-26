using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    float spawnZ = 20;
    public GameObject[] spawnList;

    float speed = -4;
    private void Start()
    {
        SpawnItems();
    }
    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if(transform.position.z < -25)
        {
           
            GameManager.instance.CreateNewTile();
            Destroy(gameObject);
        }
    }

    void SpawnItems()
    {
        int itemIndex = Random.Range(0, spawnList.Length);
        float randomX = -10;
       
        GameObject newItem = Instantiate(spawnList[itemIndex], new Vector3(randomX, 0, transform.position.z+spawnZ), Quaternion.identity);
        newItem.transform.SetParent(this.transform);
    }
}
