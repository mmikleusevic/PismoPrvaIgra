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
    [SerializeField] private float timeInterval = 2f;
    [SerializeField] private float difficultyMultiplier = 0.9f;

    private int score;
    private float multipliedTime;
    //private float timeCounter;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public void PlayGame()
    {
        multipliedTime = timeInterval;
        
        scoreText.text = $"Score: 0";
        StartCoroutine(BalloonSpawning());
    }

    private IEnumerator BalloonSpawning()
    {
        multipliedTime *= difficultyMultiplier;
        float timeValue = Mathf.Max(multipliedTime, 0.6f);
        
        yield return new WaitForSeconds(timeValue);
        
        BalloonSpawnPoint();
        
        StartCoroutine(BalloonSpawning());
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
}