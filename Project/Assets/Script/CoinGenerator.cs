using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public ObjectPooler coinPool;
    public float distanceBetweenCoin;

    public void SpawnCoins(Vector3 startPosition)
    {
        GameObject coin = coinPool.GetPooledObject();
        coin.transform.position = startPosition;
        coin.SetActive(true);

        GameObject coin2 = coinPool.GetPooledObject();
        coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoin, startPosition.y, startPosition.z);
        coin2.SetActive(true);

        GameObject coin3 = coinPool.GetPooledObject();
        coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoin, startPosition.y, startPosition.z);
        coin3.SetActive(true);
    }
}
