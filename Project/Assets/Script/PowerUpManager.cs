using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private bool doublePoints;
    private bool safeMode;

    private bool powerUpActive;

    private float powerUpLengthCounter;

    private ScoreManager scoreManager;
    private PlatformGenerator platformGenerator;
    private GameManager gameManager;

    private float normalPointsPerSec;
    private float spikeRate;

    private PlatformDestroyer[] spikeList;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        platformGenerator = FindObjectOfType<PlatformGenerator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpActive)
        {
            powerUpLengthCounter -= Time.deltaTime;
            if(gameManager.powerupReset)
            {
                powerUpLengthCounter = 0;
                gameManager.powerupReset = false;
            }
            if(doublePoints)
            {
                scoreManager.pointsPerSecond = 2f * normalPointsPerSec;
                scoreManager.shoudDouble = true;

            }
            if(safeMode)
            {
                platformGenerator.spikeGenerateThreshold = 0;
            }
            if(powerUpLengthCounter <= 0)
            {
                scoreManager.pointsPerSecond = normalPointsPerSec;
                platformGenerator.spikeGenerateThreshold = spikeRate;
                powerUpActive = false;
                scoreManager.shoudDouble = false;
            }
        }
    }

    public void ActivatePowerUp(bool points, bool safe, float time)
    {
        doublePoints = points;
        safeMode = safe;
        powerUpLengthCounter = time;
        normalPointsPerSec = scoreManager.pointsPerSecond;
        spikeRate = platformGenerator.spikeGenerateThreshold;

        if(safeMode)
        {
            spikeList = FindObjectsOfType<PlatformDestroyer>();
            foreach (PlatformDestroyer platformDestroyer in spikeList)
            {
                if(platformDestroyer.gameObject.name.Contains("Spike"))
                {
                    platformDestroyer.gameObject.SetActive(false);
                }
                
            }
        }
        powerUpActive = true;
    }
}
