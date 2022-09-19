using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthBar;
    private HealthSystem playerHealth;
    private Text healthText;

    private void Awake()
    {
        SpawnManager.onSpawnFinished +=
            () => playerHealth = GameObject.FindWithTag("Player").GetComponent<HealthSystem>();
    }

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthText = GetComponentInChildren<Text>();
        healthBar.maxValue = 100;
    }
    
    private void Update()
    {
        if (playerHealth == null) return;
        healthBar.value = playerHealth.health;
        healthText.text = playerHealth.health.ToString();
    }
}
