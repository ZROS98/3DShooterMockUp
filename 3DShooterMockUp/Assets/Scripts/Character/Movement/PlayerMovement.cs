using SUtilities.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private float WalkSpeed = 10.0f;
    [field: SerializeField]
    private float JumpForce = 10.0f;
    [field: SerializeField]
    private float GroundCheckSphereRadius { get; set; } = 0.3f;

    private Vector2 MovementInput { get; set; }
    private float RotationAngel { get; set; } = 90.0f;
    
    protected virtual void FixedUpdate ()
    {
        RotateRigidbodyToCameraForward();
        Run();
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