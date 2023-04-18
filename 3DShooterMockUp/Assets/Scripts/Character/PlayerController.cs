using System.Collections;
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

        private WaitForSeconds PowerUpDurationTimer { get; set; }
        private ShooterMockUpInputActions CurrentInputActions { get; set; }

        public void HandlePowerUp (PowerUpType powerUpType, float powerUpDuration, int powerUpPower)
        {
            PowerUpDurationTimer = new WaitForSeconds(powerUpDuration);
            StartCoroutine(PowerUpProcess(powerUpType, powerUpPower));
        }
        
        protected virtual void Awake ()
        {
            ManageInputs();
        }
        
        private void ManageInputs ()
        {
            CurrentInputActions = new ShooterMockUpInputActions();
            CurrentInputActions.Player.Enable();
            CurrentInputActions.Player.Look.Enable();
            CurrentPlayerShooting.CurrentInputActions = CurrentInputActions;
            CurrentPlayerLook.CurrentInputActions = CurrentInputActions;
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
                case PowerUpType.DamagePowerUp:
                    CurrentPlayerShooting.ActivateWeaponPowerUp(powerUpPower);
                    break;
                case PowerUpType.SpeedPowerUp:
                    CurrentPlayerMovement.ActivateMovementPowerUp(powerUpPower);
                    break;
            }
        }

        private void DeactivatePowerUp (PowerUpType powerUpType)
        {
            switch (powerUpType)
            {
                case PowerUpType.DamagePowerUp:
                    CurrentPlayerShooting.DeactivatePowerUp();
                    break;
                case PowerUpType.SpeedPowerUp:
                    CurrentPlayerMovement.DeactivatePowerUp();
                    break;
            }
        }
    }
}