using ShooterMockUp.Utilities;
using UnityEngine;

namespace ShooterMockUp.PowerUp.Data
{
    [CreateAssetMenu(menuName = ProjectConstants.SHOOTER_MOCK_UP_MENU_PATH + ASSET_NAME)]
    public class PowerUpSetup : ScriptableObject
    {
        [field: Header(ProjectConstants.HEADER_SETTINGS)]
        [field: SerializeField]
        public PowerUpType CurrentPowerUpType { get; set; }
        [field: SerializeField]
        public int PowerUpPower { get; set; }
        [field: SerializeField]
        public float PowerUpDuration { get; set; }

        private const string ASSET_NAME = nameof(PowerUpSetup);
    }
}