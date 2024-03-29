﻿using ShooterMockUp.Player;
using ShooterMockUp.PowerUp.Data;
using ShooterMockUp.Utilities;
using UnityEngine;

namespace ShooterMockUp.PowerUp
{
    public class PowerUp : MonoBehaviour
    {
        [field: Header(ProjectConstants.HEADER_REFERENCES)]
        [field: SerializeField]
        private PowerUpSetup CurrentPowerUpSetup { get; set; }

        protected virtual void OnCollisionEnter (Collision collider)
        {
            CheckForPlayer(collider);
        }

        private void CheckForPlayer (Collision collider)
        {
            if (collider.gameObject.TryGetComponent(out PlayerController playerController))
            {
                playerController.HandlePowerUp(CurrentPowerUpSetup.CurrentPowerUpType, CurrentPowerUpSetup.PowerUpDuration, CurrentPowerUpSetup.PowerUpPower);
                Destroy(gameObject);
            }
        }
    }
}