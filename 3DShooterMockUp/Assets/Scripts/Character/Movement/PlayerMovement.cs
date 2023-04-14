using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField]
    private float WalkSpeed = 10.0f;
    [field: SerializeField]
    private float JumpForce = 10.0f;
    [field: SerializeField]
    private Rigidbody CurrentRigidbody { get; set; }

    private Vector2 MovementInput;

    protected virtual void FixedUpdate ()
    {
        Run();
    }

    private void Run ()
    {
        Vector3 playerVelocity = new Vector3(MovementInput.x * WalkSpeed, CurrentRigidbody.velocity.y, MovementInput.y * WalkSpeed);
        CurrentRigidbody.velocity = transform.TransformDirection(playerVelocity);
    }

    private void OnJump (InputValue inputValue)
    {
        Vector3 velocityPossibleToJump = new Vector3(CurrentRigidbody.velocity.x, 0.0f, CurrentRigidbody.velocity.z); 
        
        if (inputValue.isPressed && CurrentRigidbody.velocity == velocityPossibleToJump)
        { 
            CurrentRigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void OnMove (InputValue inputValue)
    {
        MovementInput = inputValue.Get<Vector2>();
    }
}