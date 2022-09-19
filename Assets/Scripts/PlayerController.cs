using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private const float Gravity = -9.81f;
    private CharacterController characterController;
    [SerializeField] private float jumpHeight = 2.5f;
    [SerializeField] private Camera playerCamera;
    [Range(0f, 20f)] public float sensitivity = 5f;
    [Range(0f, 100f)] public float speed = 10f;
    private float xRotation;
    private float yRotation;
    
    public Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;
    private Vector3 velocity;

    [SerializeField] private Weapon activeWeapon;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        activeWeapon = GetComponentInChildren<Weapon>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MovementCalculation();
        WeaponCalculation();
    }

    private void MovementCalculation()
    {
        isGrounded = characterController.isGrounded;

        xRotation += Input.GetAxis("Mouse Y") * sensitivity * Time.timeScale;
        xRotation = Mathf.Clamp(xRotation, -60.0f, 60.0f);
        yRotation += Input.GetAxis("Mouse X") * sensitivity * Time.timeScale;
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(-xRotation, yRotation, 0);


        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        var curSpeed = Input.GetKey(KeyCode.LeftShift) && isGrounded ? speed * 2 : speed;
        characterController.Move(Quaternion.Euler(0, yRotation, 0) * move * (curSpeed * Time.deltaTime));
        
        // if grounded remove gravity
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        if (Input.GetKey(KeyCode.Space) && isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2 * Gravity);
        velocity.y += Gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void WeaponCalculation()
    {
        if (Input.GetMouseButton(0) && activeWeapon.canShoot)
        {
            activeWeapon.Shoot();
        }
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene("MainMenu");
    }
}