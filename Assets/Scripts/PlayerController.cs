using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float jumpHeight = 5.0f;
    [SerializeField] private Camera camera;
    [Range(0f, 20f)] public float sensitivity = 10.0f;
    [Range(0f, 100f)] public float speed = 20.0f;
    private float xRotation;
    private float yRotation;

    private const float Gravity = -9.81f;
    public Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;
    private Vector3 velocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);

        xRotation += Input.GetAxis("Mouse Y") * sensitivity * Time.timeScale;
        xRotation = Mathf.Clamp(xRotation, -60.0f, 60.0f);
        yRotation += Input.GetAxis("Mouse X") * sensitivity * Time.timeScale;
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        camera.transform.localRotation = Quaternion.Euler(-xRotation, yRotation, 0);


        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        characterController.Move(Quaternion.Euler(0, yRotation, 0) * move * (speed * Time.deltaTime));
        
        // if grounded remove gravity
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        if (Input.GetKey(KeyCode.Space) && isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2 * Gravity);
        velocity.y += Gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

}