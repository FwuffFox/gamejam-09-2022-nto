using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;
    public GameObject playerPreset;
    public GameObject botPreset;

    public static Action onSpawnFinished;

    private void Start()
    {
        spawnPoints = gameObject.GetComponentsInChildren<SpawnPoint>();
        var randomSpawn = Random.Range(0, spawnPoints.Length);
        for (var i = 0; i < spawnPoints.Length; i++)
        {
            if (i == randomSpawn) continue;
            if (spawnPoints[i].transform.position == Vector3.zero) continue;
            Instantiate(botPreset, spawnPoints[i].transform);
        }
        Instantiate(playerPreset, spawnPoints[randomSpawn].transform);
        onSpawnFinished?.Invoke();
    }
}
