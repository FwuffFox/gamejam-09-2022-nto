using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float jumpPower;



    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && characterController.isGrounded) 
            characterController.Move(new Vector3(0, jumpPower, 0));

        
        
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");
    }
}