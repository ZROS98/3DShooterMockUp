using ShooterMockUp.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [field: SerializeField]
    private SlowWeapon CurrentSlowWeapon { get; set; }
    [field: SerializeField]
    private SlowWeapon CurrentFastWeapon { get; set; }
    
    private ShooterMockUpInputActions CurrentInputActions { get; set; }
    
    protected virtual void Awake ()
    {
        ManageInputs();
    }
    
    protected virtual void OnEnable ()
    {
        AttachEvents();
    }

    protected virtual void OnDisable ()
    {
        DetachEvents();
    }
    
    private void ManageInputs ()
    {
        CurrentInputActions = new ShooterMockUpInputActions();
        CurrentInputActions.Player.Enable();
    }
    
    private void OnLeftMouseButtonActionUpdated (InputAction.CallbackContext context)
    {
        CurrentSlowWeapon.Shoot();
    }
    
    private void OnRightMouseButtonActionUpdated (InputAction.CallbackContext context)
    {
        CurrentFastWeapon.Shoot();
    }
    
    private void AttachEvents ()
    {
        CurrentInputActions.Player.LeftMouseButton.performed += OnLeftMouseButtonActionUpdated;
        CurrentInputActions.Player.RightMouseButton.performed += OnRightMouseButtonActionUpdated;
    }

    private void DetachEvents ()
    {
        CurrentInputActions.Player.LeftMouseButton.performed -= OnLeftMouseButtonActionUpdated;
        CurrentInputActions.Player.RightMouseButton.performed -= OnRightMouseButtonActionUpdated;
    }
}