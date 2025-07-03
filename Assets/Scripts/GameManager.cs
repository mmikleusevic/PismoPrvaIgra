using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    //[SerializeField] private Balloon[] baloonPrefab;
    [SerializeField] private Balloon balloonPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gamePauseMenu;
    [SerializeField] private float timeInterval = 2f;
    [SerializeField] private float difficultyMultiplier = 0.9f;

    private IEnumerator spawnCoroutine;
    private float multipliedTime;
    private int score;
    private int lives = 5;
    private bool isPaused;
    //private float timeCounter;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainMenu.activeInHierarchy)
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                gamePauseMenu.SetActive(false);
            }
            else
            {
                gamePauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            
            isPaused = !isPaused;
        }
    }

    public void PlayGame()
    {
        multipliedTime = timeInterval;
        lives = 5;
        score = 0;
        scoreText.text = $"Score: 0";
        spawnCoroutine = BalloonSpawning();
        StartCoroutine(spawnCoroutine);
    }

    private void KillAllBalloons()
    {
        Balloon[] ballons = FindObjectsByType<Balloon>(FindObjectsSortMode.None);
        foreach (Balloon ballon in ballons)
        {
            ballon.DestroyBalloon();
        }
    }

    private IEnumerator BalloonSpawning()
    {
        multipliedTime *= difficultyMultiplier;
        float timeValue = Mathf.Max(multipliedTime, 0.6f);
        
        yield return new WaitForSeconds(timeValue);
        
        BalloonSpawnPoint();
        
        yield return BalloonSpawning();
    }

    // private void Update()
    // {
    //     timeCounter -= Time.deltaTime;
    //
    //     if (timeCounter > 0) return;
    //     
    //     BalloonSpawnPoint();
    //     timeCounter = timeInterval;
    // }

    // private Balloon RandomBalloon()
    // {
    //     int random = RandomNumber(baloonPrefab.Length);
    //     return baloonPrefab[random];
    // }
    
    private Transform RandomSpawnPoint()
    {
        int random = RandomGeneratedNumber.RandomNumber(spawnPoints.Length);
        return spawnPoints[random];
    }

    private void BalloonSpawnPoint()
    {
        Balloon balloonClone = Instantiate(balloonPrefab, RandomSpawnPoint().position, Quaternion.identity);
        balloonClone.ChangeMaterial();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }

    public void DecreaseScore()
    {
        score--;
        lives--;
        scoreText.text = $"Score: {score}";

        if (lives <= 0)
        {
            StopCoroutine(spawnCoroutine);
            StartCoroutine(LoseScreen());
        }
    }

    public IEnumerator LoseScreen()
    {
        KillAllBalloons();
        mainMenu.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(3f);
        
        gameOverText.gameObject.SetActive(false);
    }
}