using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGeneration : MonoBehaviour {

    [HideInInspector] public float ringHigh;
    [HideInInspector] public float ringLow;
    public float ringMiddle;
    [SerializeField] private float heightDiff;
    [SerializeField] private float startZ;
    public bool twoPositions; //different for level 6 (true) or 7 and 8 (false)
    public float distanceBetween;
    private float distanceLevel;
    public Terrain finalTerrain;

    private int numRings;
    private float posTerrain;
    private float sizeTerrain;

    [HideInInspector] public GameObject[] ringArray;
    public GameObject _ringPrefab;
    private bool ringPosTwo;
    private bool ringPosThreeHigh;
    private bool ringPosThreeLow;

    // Start is called before the first frame update
    void Start()
    {
        //starting in the middle
        ringPosTwo = true;
        //starting in the middle going high
        ringPosThreeHigh = false;
        ringPosThreeLow = false;

        //get terrain data
        posTerrain = finalTerrain.transform.position.z;
        sizeTerrain = finalTerrain.terrainData.size.z;
        distanceLevel = posTerrain + sizeTerrain;
        numRings = Mathf.RoundToInt(distanceLevel / distanceBetween); //calculate how many rings needed

        //correct ring positions
        ringHigh = ringMiddle + heightDiff;
        ringLow = ringMiddle - heightDiff;

        
        ringArray = new GameObject[numRings];

        for (int i= 0; i < ringArray.Length; i++)
        {
            //For Level 6 only 2 positions
            if (twoPositions)
            {
                if (ringPosTwo)
                {
                    ringArray[i] = Instantiate(_ringPrefab, new Vector3(0f, ringMiddle, startZ), Quaternion.identity);
                }
                else if (!ringPosTwo)
                {
                    ringArray[i] = Instantiate(_ringPrefab, new Vector3(0f, ringLow, startZ), Quaternion.identity);
                }
                ringPosTwo = !ringPosTwo;
            } //For Level 7 and 8 3 positions
            else
            {
                //Highest ring
                if(ringPosThreeHigh && !ringPosThreeLow)
                {
                    ringArray[i] = Instantiate(_ringPrefab, new Vector3(0f, ringHigh, startZ), Quaternion.identity);
                    ringPosThreeHigh = true;
                    ringPosThreeLow = true;
                } 
                //Lowest ring
                else if (!ringPosThreeHigh && ringPosThreeLow)
                {
                    ringArray[i] = Instantiate(_ringPrefab, new Vector3(0f, ringLow, startZ), Quaternion.identity);
                    ringPosThreeHigh = false;
                    ringPosThreeLow = false;
                }
                //Middle ring comming from low
                else if (!ringPosThreeHigh && !ringPosThreeLow)
                {
                    ringArray[i] = Instantiate(_ringPrefab, new Vector3(0f, ringMiddle, startZ), Quaternion.identity);
                    ringPosThreeHigh = true;
                    ringPosThreeLow = false;
                }
                //Middle ring comming from high
                else if (ringPosThreeHigh && ringPosThreeLow)
                {
                    ringArray[i] = Instantiate(_ringPrefab, new Vector3(0f, ringMiddle, startZ), Quaternion.identity);
                    ringPosThreeHigh = false;
                    ringPosThreeLow = true;
                }
            }

            startZ += distanceBetween;
            
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
}
