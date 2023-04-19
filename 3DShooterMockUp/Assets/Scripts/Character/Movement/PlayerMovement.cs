using System;
using ShooterMockUp.Input;
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
        private float WalkSpeedAcceleration { get; set; } = 1.3f;
        [field: SerializeField]
        private float JumpForce { get; set; } = 10.0f;
        [field: SerializeField]
        private float GroundCheckSphereRadius { get; set; } = 0.3f;

        public ShooterMockUpInputActions CurrentInputActions { get; set; }

        public event Action<PlayerState> OnPlayerStateChanged = delegate { };

        private Vector2 MovementInput { get; set; }
        private float RotationAngle { get; set; } = 90.0f;
        private float CachedWalkSpeed { get; set; }
        private float CachedJumpForce { get; set; }
        private bool IsPowerUpActivated { get; set; }
        private bool IsPlayerWalking { get; set; }
        private bool IsPlayerSprinting { get; set; }
        private bool IsPlayerJumping { get; set; }

        public void ActivateMovementPowerUp (int powerUpPower)
        {
            ActivateSpeedPowerUp(powerUpPower);
            ActivateJumpPowerUp(powerUpPower);
            IsPowerUpActivated = true;
        }

        public void DeactivatePowerUp ()
        {
            DeactivateSpeedPowerUp();
            DeactivateJumpPowerUp();
            IsPowerUpActivated = false;
        }

        protected virtual void Awake ()
        {
            Initialize();
        }

        protected virtual void Start ()
        {
            AttachEvents();
        }

        protected virtual void OnDisable ()
        {
            DetachEvents();
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
            WalkSpeed = CachedWalkSpeed;
        }

        private void DeactivateJumpPowerUp ()
        {
            JumpForce = CachedJumpForce;
        }

        private void Initialize ()
        {
            CachedWalkSpeed = WalkSpeed;
            CachedJumpForce = JumpForce;
        }

        private void RotateRigidbodyToCameraForward ()
        {
            Vector3 cameraForward = PlayerCamera.forward * -1.0f;
            Quaternion desiredRotation = Quaternion.LookRotation(Vector3.up, cameraForward) * Quaternion.AngleAxis(RotationAngle, Vector3.right);

            CurrentRigidbody.rotation = desiredRotation;
        }

        private void Run ()
        {
            Vector3 playerVelocity = new Vector3(MovementInput.x * WalkSpeed, CurrentRigidbody.velocity.y, MovementInput.y * WalkSpeed);
            CurrentRigidbody.velocity = transform.TransformDirection(playerVelocity);

            if (IsPlayerWalking == true && IsPlayerSprinting == false && IsPlayerJumping == false)
            {
                OnPlayerStateChanged.Invoke(PlayerState.MOVING);
            }
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

        private void HandleSprint (InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                WalkSpeed *= WalkSpeedAcceleration;
                OnPlayerStateChanged.Invoke(PlayerState.SPRINTING);
                IsPlayerSprinting = true;
            }
            else if (callbackContext.canceled)
            {
                if (IsPowerUpActivated == true)
                {
                    WalkSpeed /= WalkSpeedAcceleration;
                }
                else
                {
                    WalkSpeed = CachedWalkSpeed;
                }

                OnPlayerStateChanged.Invoke(IsPlayerWalking == false ? PlayerState.IDLE : PlayerState.MOVING);
                IsPlayerSprinting = false;
            }
        }

        private void HandlePlayerStateOnMove (InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                IsPlayerWalking = true;
            }
            else if (callbackContext.canceled)
            {
                IsPlayerWalking = false;
                OnPlayerStateChanged.Invoke(PlayerState.IDLE);
            }
        }

        private void HandleStateOnJump (InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                IsPlayerJumping = true;
                OnPlayerStateChanged.Invoke(PlayerState.JUMPING);
            }
            else if (callbackContext.canceled)
            {
                IsPlayerJumping = false;

                if (IsPlayerSprinting == true)
                {
                    OnPlayerStateChanged.Invoke(PlayerState.SPRINTING);
                }
                else if (IsPlayerWalking == true)
                {
                    OnPlayerStateChanged.Invoke(PlayerState.MOVING);
                }
                else
                {
                    OnPlayerStateChanged.Invoke(PlayerState.IDLE);
                }
            }
        }

        private void AttachEvents ()
        {
            CurrentInputActions.Player.Sprint.performed += HandleSprint;
            CurrentInputActions.Player.Sprint.canceled += HandleSprint;
            CurrentInputActions.Player.Move.performed += HandlePlayerStateOnMove;
            CurrentInputActions.Player.Move.canceled += HandlePlayerStateOnMove;
            CurrentInputActions.Player.Jump.performed += HandleStateOnJump;
            CurrentInputActions.Player.Jump.canceled += HandleStateOnJump;
        }

        private void DetachEvents ()
        {
            CurrentInputActions.Player.Sprint.performed -= HandleSprint;
            CurrentInputActions.Player.Sprint.canceled -= HandleSprint;
            CurrentInputActions.Player.Move.performed -= HandlePlayerStateOnMove;
            CurrentInputActions.Player.Move.canceled -= HandlePlayerStateOnMove;
            CurrentInputActions.Player.Jump.performed -= HandleStateOnJump;
            CurrentInputActions.Player.Jump.canceled -= HandleStateOnJump;
        }
    }
}