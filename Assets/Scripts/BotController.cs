using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CharacterController))]
public class BotController : MonoBehaviour
{
    private const float Gravity = -9.81f;
    private float jumpHeight = 2.5f;
    private Vector3 velocity;
    private CharacterController botController;
    private Weapon activeWeapon;
    
    private GameObject player;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private bool canSeePlayer, canShootPlayer;
    [SerializeField] private float sightRange = 200f;
    [SerializeField] private float speed = 10f;

    private void Awake()
    {
        botController = GetComponent<CharacterController>();
        activeWeapon = GetComponentInChildren<Weapon>();
        SpawnManager.onSpawnFinished += AssignFields;
    }

    private void AssignFields()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (player == null) return;
        canSeePlayer = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        canShootPlayer = Physics.CheckSphere(transform.position, activeWeapon.range, playerLayer);
        if (canSeePlayer && !canShootPlayer) ApproachEnemy();
        if (canSeePlayer && canShootPlayer) FightEnemy();
        CalculatePhysics();
    }

    private void ApproachEnemy()
    {
        transform.LookAt(player.transform, Vector3.up);
        var step = speed * Time.deltaTime;
        botController.Move(Quaternion.Euler(0, transform.rotation.y, 0) * transform.forward * (speed * Time.deltaTime));
    }
    
    private void FightEnemy()
    {
        var playerPos = player.transform.position;
        if (Random.Range(0, 2) == 1)
            playerPos += new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        transform.LookAt(playerPos);
        if (activeWeapon.canShoot) activeWeapon.Shoot();
    }
    
    private void CalculatePhysics()
    {
        var isGrounded = botController.isGrounded; 
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        velocity.y += Gravity * Time.deltaTime;
        botController.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activeWeapon.range);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
