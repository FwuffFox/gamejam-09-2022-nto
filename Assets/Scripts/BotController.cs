using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BotController : MonoBehaviour
{
    private const float Gravity = -9.81f;
    private float jumpHeight = 2.5f;
    private Vector3 velocity;
    private CharacterController botController;

    private void Awake()
    {
        botController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var isGrounded = botController.isGrounded;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        velocity.y += Gravity * Time.deltaTime;
        botController.Move(velocity * Time.deltaTime);
    }
}
