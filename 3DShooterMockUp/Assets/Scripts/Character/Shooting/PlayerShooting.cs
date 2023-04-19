using System;
using ShooterMockUp.Input;
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
            HandleWeaponPowerUp(CurrentSlowWeapon, powerUpPower);
            HandleWeaponPowerUp(CurrentFastWeapon, powerUpPower);
        }

        public void DeactivatePowerUp ()
        {
            HandleWeaponPowerDown(CurrentSlowWeapon);
            HandleWeaponPowerDown(CurrentFastWeapon);
        }

        protected virtual void Start ()
        {
            AttachEvents();
        }

        protected virtual void OnDisable ()
        {
            DetachEvents();
        }

        private void HandleWeaponPowerUp (Weapon.Weapon weapon, int powerUpPower)
        {
            weapon.LocalDamage = (weapon.LocalDamage * (ProjectConstants.HUNDRED_PERCENT + powerUpPower)) / ProjectConstants.HUNDRED_PERCENT;
        }

        private void HandleWeaponPowerDown (Weapon.Weapon weapon)
        {
            weapon.LocalDamage = weapon.CurrentWeaponSetup.Damage;
        }

        private void OnLeftMouseButtonActionUpdated (InputAction.CallbackContext context)
        {
            CurrentFastWeapon.Shoot();
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