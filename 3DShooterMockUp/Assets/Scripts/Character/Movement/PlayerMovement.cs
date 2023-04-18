using ShooterMockUp.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterMockUp.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [field: Header(ProjectConstants.HEADER_REFERENCES)]
        [field: SerializeField]
        private Transform PlayerCamera { get; set; }
        [field: SerializeField]
        private Transform GroundCheckObject { get; set; }
        [field: SerializeField]
        private Rigidbody CurrentRigidbody { get; set; }

        [field: Header(ProjectConstants.HEADER_SETTINGS)]
        [field: SerializeField]
        private LayerMask GroundLayer { get; set; }
        [field: SerializeField]
        private float WalkSpeed { get; set; } = 10.0f;
        [field: SerializeField]
        private float JumpForce { get; set; } = 10.0f;
        [field: SerializeField]
        private float GroundCheckSphereRadius { get; set; } = 0.3f;

        private Vector2 MovementInput { get; set; }
        private float RotationAngel { get; set; } = 90.0f;
        private float CashedWalkSpeed { get; set; }
        private float CashedJumpForce { get; set; }

        public void ActivateMovementPowerUp (int powerUpPower)
        {
            ActivateSpeedPowerUp(powerUpPower);
            ActivateJumpPowerUp(powerUpPower);
        }

        public void DeactivatePowerUp ()
        {
            DeactivateSpeedPowerUp();
            DeactivateJumpPowerUp();
        }

        protected virtual void Awake ()
        {
            Initialize();
        }

        protected virtual void FixedUpdate ()
        {
            RotateRigidbodyToCameraForward();
            Run();
        }

        private void ActivateSpeedPowerUp (int powerUpPower)
        {
            WalkSpeed = GetPowerUpPower(WalkSpeed, powerUpPower);
        }

        private void ActivateJumpPowerUp (int powerUpPower)
        {
            JumpForce = GetPowerUpPower(JumpForce, powerUpPower);
        }

        private float GetPowerUpPower (float powerType, int powerUpPower)
        {
            powerType = (powerType * (ProjectConstants.HUNDRED_PERCENT + powerUpPower)) / ProjectConstants.HUNDRED_PERCENT;
            return powerType;
        }

        private void DeactivateSpeedPowerUp ()
        {
            WalkSpeed = CashedWalkSpeed;
        }

        private void DeactivateJumpPowerUp ()
        {
            JumpForce = CashedJumpForce;
        }

        private void Initialize ()
        {
            CashedWalkSpeed = WalkSpeed;
            CashedJumpForce = JumpForce;
        }

        private void RotateRigidbodyToCameraForward ()
        {
            Vector3 cameraForward = -PlayerCamera.forward;
            Quaternion desiredRotation = Quaternion.LookRotation(Vector3.up, cameraForward) * Quaternion.AngleAxis(RotationAngel, Vector3.right);

            CurrentRigidbody.rotation = desiredRotation;
        }

        private void Run ()
        {
            Vector3 playerVelocity = new Vector3(MovementInput.x * WalkSpeed, CurrentRigidbody.velocity.y, MovementInput.y * WalkSpeed);
            CurrentRigidbody.velocity = transform.TransformDirection(playerVelocity);
        }

        private void OnMove (InputValue inputValue)
        {
            MovementInput = inputValue.Get<Vector2>();
        }

        private void OnJump (InputValue inputValue)
        {
            if (inputValue.isPressed && IsGrounded() == true)
            {
                CurrentRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }

        private bool IsGrounded ()
        {
            return Physics.CheckSphere(GroundCheckObject.position, GroundCheckSphereRadius, GroundLayer);
        }

        private void OnDrawGizmos ()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(GroundCheckObject.position, GroundCheckSphereRadius);
        }
    }
}