using ShooterMockUp.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    private readonly Weapon currentSlowWeapon = new SlowWeapon();
    private readonly Weapon currentFastWeapon = new FastWeapon();
    
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
        currentFastWeapon.Shoot();
    }
    
    private void OnRightMouseButtonActionUpdated (InputAction.CallbackContext context)
    {
        currentSlowWeapon.Shoot();
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