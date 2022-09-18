using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject playerPreset;
    public GameObject botPreset;

    private void Start()
    {
        spawnPoints = gameObject.GetComponentsInChildren<Transform>();
        var randomSpawn = Random.Range(0, spawnPoints.Length);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i == randomSpawn) continue;
            if (spawnPoints[i].transform.position == transform.position) continue;
            Instantiate(botPreset, spawnPoints[i]);
        }
        Instantiate(playerPreset, spawnPoints[randomSpawn]);
    }
}
