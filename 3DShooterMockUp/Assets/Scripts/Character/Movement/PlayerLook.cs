using ShooterMockUp.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterMockUp.Player
{
    public class PlayerLook : MonoBehaviour
    {
        [field: Header(ProjectConstants.HEADER_REFERENCES)]
        [field: SerializeField]
        private Transform PlayerCamera { get; set; }

        [field: Header(ProjectConstants.HEADER_SETTINGS)]
        [field: SerializeField]
        private float MouseSensitivity { get; set; } = 100.0f;

        public ShooterMockUpInputActions CurrentInputActions { get; set; }
        
        private float RotationAxisX { get; set; } = 0.0f;
        private float RotationAxisY { get; set; } = 0.0f;
        private float MinAngleValue { get; set; } = -90.0f;
        private float MaxAngleValue { get; set; } = 90.0f;
        
        private Vector2 MouseDelta { get; set; }

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
            float mouseX = MouseDelta.x * MouseSensitivity * Time.deltaTime;
            float mouseY = MouseDelta.y * MouseSensitivity * Time.deltaTime;

            RotationAxisX -= mouseY;
            RotationAxisX = Mathf.Clamp(RotationAxisX, MinAngleValue, MaxAngleValue);
            RotationAxisY += mouseX;

            PlayerCamera.rotation = Quaternion.Euler(RotationAxisX, RotationAxisY, 0f);
        }

        private void OnLook (InputValue inputValue)
        {
            MouseDelta = inputValue.Get<Vector2>();
        }
    }
}