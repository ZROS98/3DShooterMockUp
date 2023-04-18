using ShooterMockUp.Player;
using ShooterMockUp.PowerUp.Data;
using UnityEngine;

namespace ShooterMockUp.PowerUp
{
    public class PowerUp : MonoBehaviour
    {
        [field: SerializeField]
        private PowerUpSetup CurrenPowerUpSetup { get; set; }

        protected virtual void OnCollisionEnter (Collision collider)
        {
            CheckForPlayer(collider);
        }

        private void CheckForPlayer (Collision collider)
        {
            if (collider.gameObject.TryGetComponent(out PlayerController playerController))
            {
                playerController.HandlePowerUp(CurrenPowerUpSetup.CurrentPowerUpType, CurrenPowerUpSetup.PowerUpDuration, CurrenPowerUpSetup.PowerUpPower);
                
                Destroy(gameObject);
            }
        }
    }
}