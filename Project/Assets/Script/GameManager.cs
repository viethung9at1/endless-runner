using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayerController thePlayer;
    private Vector3 playerStartPoint;

    private PlatformDestroyer[] platformDestroyers;
    private ScoreManager scoreManager;
    public DeathMenu deathMenu;

    public bool powerupReset;
    // Start is called before the first frame update
    void Start()
    {
        platformStartPoint = platformGenerator.transform.position;
        playerStartPoint = thePlayer.transform.position;
        scoreManager = FindObjectOfType<ScoreManager>();
        deathMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        scoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);
        deathMenu.gameObject.SetActive(true);
    }
    public void Reset()
    {
        deathMenu.gameObject.SetActive(false);
        platformDestroyers = FindObjectsOfType<PlatformDestroyer>();
        foreach (PlatformDestroyer platformDestroyer in platformDestroyers)
        {
            platformDestroyer.gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);

        scoreManager.scoreCount = 0;
        scoreManager.scoreIncreasing = true;
        powerupReset = true;
    }
   /* public IEnumerator RestartGameCo()
    {
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        platformDestroyers = FindObjectsOfType<PlatformDestroyer>();
        foreach(PlatformDestroyer platformDestroyer in platformDestroyers)
        {
            platformDestroyer.gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);

        scoreManager.scoreCount = 0;
        scoreManager.scoreIncreasing = true;
    }*/
}
