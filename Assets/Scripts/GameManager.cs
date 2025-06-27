using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private Balloon[] baloonPrefab;
    [SerializeField] private Balloon balloonPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeInterval = 2f;
    [SerializeField] private float difficultyMultiplier = 0.9f;
    private float multipliedTime;
    
    private float timeCounter;

    private void Start()
    {
        timeCounter = timeInterval;
        multipliedTime = timeInterval;
        
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
}