using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    ObjectPooling objectPooling;

    public GameObject plane;
    private float spawnZ;
    private MeshRenderer renderer;
    private Vector3 size;
    public int treeSpacing;

    private void Start()
    {
        objectPooling = ObjectPooling.Instance;
        renderer = plane.GetComponent<MeshRenderer>();
        size = renderer.bounds.size;

        spawnZ = transform.position.z;

        foreach (ObjectPooling.Pool pool in objectPooling.pools)
        {
            for (int z = (int)(spawnZ + (size.z / 2)); z > (int)(spawnZ - (size.z / 2)); z -= treeSpacing)
            {
                for (int x = (int)-size.x / 2; x < (int)size.x / 2; x += treeSpacing)
                {
                    objectPooling.SpawnFromPool("Tree", new Vector3(x, 0f, z), 3f, Quaternion.identity);
                }
            }
        }

    }

    private void FixedUpdate()
    {
        
    }
}
