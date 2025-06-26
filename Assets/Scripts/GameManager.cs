using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private Balloon[] baloonPrefab;
    [SerializeField] private Balloon balloonPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeInterval = 2f;
    
    private float timeCounter;

    private void Start()
    {
        timeCounter = timeInterval;
    }

    private void Update()
    {
        timeCounter -= Time.deltaTime;

        if (timeCounter > 0) return;
        
        BalloonSpawnPoint();
        timeCounter = timeInterval;
    }

    // private int RandomNumber(int length)
    // {
    //     return Random.Range(0, length);
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