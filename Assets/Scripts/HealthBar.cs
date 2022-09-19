using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthBar;
    private HealthSystem playerHealth;

    private void Awake()
    {
        SpawnManager.onSpawnFinished +=
            () => playerHealth = GameObject.FindWithTag("Player").GetComponent<HealthSystem>();
    }

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = 100;
    }
    
    private void Update()
    {
        if (playerHealth != null) healthBar.value = playerHealth.health;
    }
}
