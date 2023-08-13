using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;
    public Transform generationPoint;
    private float distanceBetween;

    //private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public ObjectPooler objectPooler;

    private int platformSelector;
    //public GameObject[] thePlatforms;

    private float[] platformWidths;
    public ObjectPooler[] objectPoolers;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    public CoinGenerator coinGenerator;
    public int coinGenerateThreshold;

    public float spikeGenerateThreshold;
    public ObjectPooler spikePool;

    public float powerupHeight;
    public ObjectPooler powerupPool;
    public float powerupThreshold;

    // Start is called before the first frame update
    void Start()
    {
        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
        platformWidths = new float[objectPoolers.Length];
        for(int i = 0; i < objectPoolers.Length; ++i)
        {
            platformWidths[i] = objectPoolers[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }
        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
        coinGenerator = FindObjectOfType<CoinGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
           
            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            heightChange = heightChange > maxHeight ? maxHeight : heightChange;
            heightChange = heightChange < minHeight ? minHeight : heightChange;

            if (Random.Range(0f, 100f) < powerupThreshold)
            {
                GameObject newPowerup = powerupPool.GetPooledObject();

                newPowerup.transform.position = transform.position + new Vector3(distanceBetween/2f,Random.Range(powerupHeight / 2f, powerupHeight), 0f);
                newPowerup.SetActive(true);
            }

                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            //Instantiate(thePlatforms[platformSelector], transform.position, transform.rotation);
            platformSelector = Random.Range(0, objectPoolers.Length);
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, transform.position.y, transform.position.z);

            GameObject newPlatform = objectPoolers[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
               
            if(Random.Range(0f,100f) < coinGenerateThreshold)
            {
                coinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }
            if(Random.Range(0f, 100f) < spikeGenerateThreshold)
            {
                GameObject newSpike = spikePool.GetPooledObject();
                float spikeXPosition = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f);
                Vector3 spikePosition = new Vector3(spikeXPosition, 0.5f, 0f);
                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }

        }
    }
}
