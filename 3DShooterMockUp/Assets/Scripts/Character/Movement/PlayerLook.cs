using SUtilities.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerLook : MonoBehaviour
{
    [field: Header(ProjectConstants.HEADER_REFERENCES)]
    [field: SerializeField]
    private Transform PlayerCamera { get; set; }
    [FormerlySerializedAs("cameraAttachPoint")]
    [SerializeField]
    private Transform CameraAttachPoint;

    [field: Header(ProjectConstants.HEADER_SETTINGS)]
    [field: SerializeField]
    private float MouseSensitivity { get; set; } = 100.0f;

    private float RotationAxisX { get; set; } = 0.0f;
    private float RotationAxisY { get; set; } = 0.0f;
    private float MinAngleValue { get; set; } = -90.0f;
    private float MaxAngleValue { get; set; } = 90.0f;

    protected virtual void Start ()
    {
        HandleCursor();
    }

    protected virtual void LateUpdate ()
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
        RotationAxisY += mouseX;

        PlayerCamera.rotation = Quaternion.Euler(RotationAxisX, RotationAxisY, 0f);
        PlayerCamera.transform.position = CameraAttachPoint.position;
    }
}