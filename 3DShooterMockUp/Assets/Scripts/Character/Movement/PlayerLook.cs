using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [field: SerializeField]
    private float MouseSensitivity { get; set; } = 100.0f;
    
    [field: SerializeField]
    private Transform PlayerCamera { get; set; }
    [field: SerializeField]
    private Rigidbody PlayerRigidbody { get; set; }

    private float RotationAxisX { get; set; } = 0.0f;
    
    private float MinAngleValue { get; set; } = -90.0f;
    private float MaxAngleValue { get; set; } = 90.0f;

    protected virtual void Start ()
    {
        HandleCursor();
    }
    
    protected virtual void FixedUpdate ()
    {
        HandleRotation();
    }
    
    private void HandleCursor ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void HandleRotation ()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        
        RotationAxisX -= mouseY;
        RotationAxisX = Mathf.Clamp(RotationAxisX, MinAngleValue, MaxAngleValue);

        PlayerCamera.localRotation = Quaternion.Euler(RotationAxisX, 0f, 0f);
        //Player.Rotate(Vector3.up * mouseX, Space.World);
        Quaternion deltaRotation = Quaternion.Euler(0f, mouseX, 0f);
        PlayerRigidbody.rotation = (PlayerRigidbody.rotation * deltaRotation);

    }
}
