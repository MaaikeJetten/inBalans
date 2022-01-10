using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGeneration : MonoBehaviour { 

    public float ringHigh;
    public float ringLow;
    public float ringMiddle;
    [SerializeField] private float heightDiff;
    [SerializeField] private float startZ;
    public bool twoPositions;
    [SerializeField] private float distanceBetween;
    private float distanceLevel;
    public Terrain finalTerrain;

    private int numRings;
    private float posTerrain;
    private float sizeTerrain;

    public GameObject[] ringArray;
    public GameObject _ringPrefab;
    private bool ringPos;

    // Start is called before the first frame update
    void Start()
    {
        ringPos = true;
        posTerrain = finalTerrain.transform.position.z;
        sizeTerrain = finalTerrain.terrainData.size.z;
        distanceLevel = posTerrain + sizeTerrain;
        numRings = Mathf.RoundToInt(distanceLevel / distanceBetween);

        ringHigh = ringMiddle + heightDiff;
        ringLow = ringMiddle - heightDiff;

        
        ringArray = new GameObject[numRings];

        for (int i= 0; i < ringArray.Length; i++)
        {
            if (ringPos)
            {
                ringArray[i] = Instantiate(_ringPrefab, new Vector3(0f, ringMiddle, startZ), Quaternion.identity);
            }
            else if (!ringPos)
            {
                ringArray[i] = Instantiate(_ringPrefab, new Vector3(0f, ringLow, startZ), Quaternion.identity);
            }
            startZ += distanceBetween;
            ringPos = !ringPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ringArray.Length);


    }
}
