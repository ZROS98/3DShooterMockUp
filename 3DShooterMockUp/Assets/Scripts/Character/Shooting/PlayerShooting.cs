using ShooterMockUp.Utilities;
using ShooterMockUp.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterMockUp.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [field: SerializeField]
        public SlowWeapon CurrentSlowWeapon { get; set; }
        [field: SerializeField]
        public FastWeapon CurrentFastWeapon { get; set; }

        public ShooterMockUpInputActions CurrentInputActions { get; set; }

        public void ActivateWeaponPowerUp (int powerUpPower)
        {
            CurrentSlowWeapon.CurrentWeaponSetup.Projectile.LocalDamage = (CurrentSlowWeapon.CurrentWeaponSetup.Projectile.LocalDamage * (ProjectConstants.HUNDRED_PERCENT + powerUpPower)) / ProjectConstants.HUNDRED_PERCENT;
        }

        public void DeactivatePowerUp ()
        {
            CurrentSlowWeapon.CurrentWeaponSetup.Projectile.LocalDamage = CurrentSlowWeapon.CurrentWeaponSetup.Projectile.CurrentProjectileSetup.Damage;
        }

        protected virtual void OnEnable ()
        {
            AttachEvents();
        }

        protected virtual void OnDisable ()
        {
            DetachEvents();
        }

        private void OnLeftMouseButtonActionUpdated (InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                CurrentFastWeapon.Shoot();
            }
        }

        private void OnRightMouseButtonActionUpdated (InputAction.CallbackContext context)
        {
            CurrentSlowWeapon.Shoot();
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
}