using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController),
typeof(NavMeshAgent))]
public class BotController : MonoBehaviour
{
    private const float Gravity = -9.81f;
    private float jumpHeight = 2.5f;
    private Vector3 velocity;
    private CharacterController botController;
    private Weapon activeWeapon;

    private NavMeshAgent agent;
    private Transform player;
    [SerializeField] private LayerMask groundLayer, playerLayer;
    [SerializeField] private bool canSeePlayer, canShootPlayer;
    [SerializeField] private float sightRange = 200f;

    private void Awake()
    {
        botController = GetComponent<CharacterController>();
        activeWeapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        canSeePlayer = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        canShootPlayer = Physics.CheckSphere(transform.position, activeWeapon.range, playerLayer);
        if (canSeePlayer) ApproachEnemy();
        if (canSeePlayer && canShootPlayer) FightEnemy();
        CalculatePhysics();
    }

    private void ApproachEnemy()
    {
        agent.destination = player.position;
    }
    
    private void FightEnemy()
    {
        transform.LookAt(player);
        agent.destination = transform.position;
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
