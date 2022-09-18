using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float cameraXRotation = 0f;
    [Range(0f, 1f)] [SerializeField] private float mouseSensitivity = 1f;
    public Transform playerBody;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraXRotation -= mouseY;
        cameraXRotation = Mathf.Clamp(cameraXRotation, -90f, 90f);
        transform.localRotation = quaternion.Euler(cameraXRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
