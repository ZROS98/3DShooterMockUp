using System.Collections;
using ShooterMockUp.Input;
using ShooterMockUp.PowerUp;
using UnityEngine;

namespace ShooterMockUp.Player
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField]
        public PlayerMovement CurrentPlayerMovement { get; set; }
        [field: SerializeField]
        public PlayerLook CurrentPlayerLook { get; set; }
        [field: SerializeField]
        public PlayerShooting CurrentPlayerShooting { get; set; }
        [field: SerializeField]
        private PlayerUI CurrentPlayerUI { get; set; }

        private WaitForSeconds PowerUpDurationTimer { get; set; }
        private ShooterMockUpInputActions CurrentInputActions { get; set; }

        public void HandlePowerUp (PowerUpType powerUpType, float powerUpDuration, int powerUpPower)
        {
            PowerUpDurationTimer = new WaitForSeconds(powerUpDuration);
            StartCoroutine(PowerUpProcess(powerUpType, powerUpPower));
            CurrentPlayerUI.UpdatePlayerStateText(PlayerState.POWER_UPPED);
        }

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
            CurrentPlayerShooting.CurrentInputActions = CurrentInputActions;
            CurrentPlayerMovement.CurrentInputActions = CurrentInputActions;
        }

        private IEnumerator PowerUpProcess (PowerUpType powerUpType, int powerUpPower)
        {
            ActivatePowerUp(powerUpType, powerUpPower);
            yield return PowerUpDurationTimer;
            DeactivatePowerUp(powerUpType);
        }

        private void ActivatePowerUp (PowerUpType powerUpType, int powerUpPower)
        {
            switch (powerUpType)
            {
                case PowerUpType.DAMAGE_POWER_UP:
                    CurrentPlayerShooting.ActivateWeaponPowerUp(powerUpPower);
                    break;
                case PowerUpType.SPEED_POWER_UP:
                    CurrentPlayerMovement.ActivateMovementPowerUp(powerUpPower);
                    break;
            }
        }

        private void DeactivatePowerUp (PowerUpType powerUpType)
        {
            switch (powerUpType)
            {
                case PowerUpType.DAMAGE_POWER_UP:
                    CurrentPlayerShooting.DeactivatePowerUp();
                    break;
                case PowerUpType.SPEED_POWER_UP:
                    CurrentPlayerMovement.DeactivatePowerUp();
                    break;
            }
        }

        private void AttachEvents ()
        {
            CurrentPlayerMovement.OnPlayerStateChanged += CurrentPlayerUI.UpdatePlayerStateText;
            CurrentPlayerShooting.OnPlayerStateChanged += CurrentPlayerUI.UpdatePlayerStateText;
        }

        private void DetachEvents ()
        {
            CurrentPlayerMovement.OnPlayerStateChanged -= CurrentPlayerUI.UpdatePlayerStateText;
            CurrentPlayerShooting.OnPlayerStateChanged -= CurrentPlayerUI.UpdatePlayerStateText;
        }
    }
}