using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class playerMoving : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 15.0f;
    public float lookXLimit = 60.0f;
	
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
	private float rotationY=0;
	private float curSpeedX;
	private float curSpeedY;
	private float movementDirectionY;
	public bool canMove=true;
	private bool startAnimMove=false;
	private bool isRunning=false;
	
    void Start()
    {
        characterController = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {   		
		// We are grounded, so recalculate move direction based on axes
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		Vector3 right = transform.TransformDirection(Vector3.right);
		// Press Left Shift to run
		isRunning = Input.GetKey(KeyCode.LeftShift);
		
		#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.LeftAlt)) UnityEditor.EditorWindow.focusedWindow.maximized=!UnityEditor.EditorWindow.focusedWindow.maximized;
		#endif
		
		curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
		curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
		
		movementDirectionY = moveDirection.y;
		moveDirection = (forward * curSpeedX) + (right * curSpeedY);
		if (Input.GetKeyDown(KeyCode.Space)&&characterController.isGrounded)
		{
			moveDirection.y = jumpSpeed;
		}
		else
		{
			moveDirection.y = movementDirectionY;
		}

		// Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
		// when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
		// as an acceleration (ms^-2)
		if (!characterController.isGrounded)
		{
			moveDirection.y -= gravity * Time.deltaTime;
			startAnimMove=false;
		}
		else startAnimMove=true;

		// anim
		if (curSpeedX<1&&curSpeedY<1&&curSpeedX>-1&&curSpeedY>-1) startAnimMove=false;
		//if (startAnimMove==true) model.Play();
		//else model.Stop();
	
		if (canMove)
		{
			//pos
			characterController.Move(moveDirection * Time.deltaTime);
			//rot
			rotationX += Input.GetAxis("Mouse Y") * lookSpeed*Time.timeScale;
			rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
			rotationY += Input.GetAxis("Mouse X") * lookSpeed*Time.timeScale;
			playerCamera.transform.localRotation = Quaternion.Euler(-rotationX, rotationY, 0);
			//playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
			//transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed*Time.timeScale, 0);
		}
    }
}
