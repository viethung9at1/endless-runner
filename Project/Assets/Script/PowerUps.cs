using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public bool doublePoints;
    public bool safeMode;

    public float powerUpLength;
    private PowerUpManager powerUpManager;

    public Sprite[] powerupSprites;
    // Start is called before the first frame update
    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }

    // Update is called once per frame
    void Awake()
    {
        int powerupSelector = Random.Range(0, 2);
        switch(powerupSelector)
        {
            case 0: doublePoints = true;
                break;
            case 1: safeMode = true;
                break;
        }
        GetComponent<SpriteRenderer>().sprite = powerupSprites[powerupSelector]; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            powerUpManager.ActivatePowerUp(doublePoints, safeMode, powerUpLength);
        }
        gameObject.SetActive(false);
    }
}
