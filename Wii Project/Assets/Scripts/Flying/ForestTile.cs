using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTile : MonoBehaviour
{
    public GameObject[] spawnList;
    private float spawnZ;
    public GameObject plane;
   // public float forwardSpeed;

    private MeshRenderer renderer;
    private Vector3 size;
    private Vector3 planePos;
    private bool rendered;
    [SerializeField] private int treeScale;
    [SerializeField] private int treeSpacing;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
        size = renderer.bounds.size;

        spawnZ = transform.position.z;
        //spawnItems();
       
    }

    // Update is called once per frame
    void Update()
    {
        planePos = plane.transform.position;
        if(transform.position.z - size.z/2 <= planePos.z + 500 && !rendered)
        {
            rendered = true;
            spawnItems();
        }


        Debug.Log(size);
        if (transform.position.z<= planePos.z - 5)
        {
           // destroyItems();
            //GameManager.instance.CreateNewTile();
            Destroy(this.gameObject);
        }

        //transform.Translate(0f, 0f, forwardSpeed, Space.Self);
    }

    void spawnItems()
    {
        for(int z = (int)(spawnZ+(size.z/2)); z > (int)(spawnZ-(size.z/2)); z -= treeSpacing)
        {
            for (int x = (int)-size.x/2; x<(int)size.x/2; x += treeSpacing)
            {
                int itemIndex = Random.Range(0, spawnList.Length);
                float randomSize = Random.Range(-.5f, .5f);
                GameObject newItem = Instantiate(spawnList[itemIndex], new Vector3(x, transform.position.y, z) + new Vector3(Random.Range(-1f,1f), 0f, Random.Range(-1f, 1f)), Quaternion.identity);
                newItem.transform.localScale = new Vector3(treeScale + randomSize, treeScale + randomSize, treeScale + randomSize);
                newItem.isStatic = true;
               // GameObject newItem = Instantiate(spawnList[itemIndex], new Vector3(0, 0, 3*size.z/4), Quaternion.identity);
                newItem.transform.SetParent(this.transform);
            }
        }
    }

    void destroyItems()
    {
        for (int i = 0; i < spawnList.Length; i++)
        {
            GameObject tree = spawnList[i];
            Destroy(tree, 0.0f);
        }
    }
}
