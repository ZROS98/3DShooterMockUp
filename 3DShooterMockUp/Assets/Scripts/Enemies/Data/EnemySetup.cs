using ShooterMockUp.Utilities;
using UnityEngine;

namespace ShooterMockUp.Enemies.Data
{
    [CreateAssetMenu(menuName = ProjectConstants.SHOOTER_MOCK_UP_MENU_PATH + ASSET_NAME)]
    public class EnemySetup: ScriptableObject
    {
        [field: SerializeField]
        public int HealthPoints {get; set; }
        [field: SerializeField]
        public float ColorLerpDuration { get; set; } = 10.0f;
        
        [field: SerializeField]
        public Color HighHealthPointsLevelColor { get; set; } = Color.green;
        [field: SerializeField]
        public Color MediumHealthPointsLevelColor { get; set; } = Color.yellow;
        [field: SerializeField]
        public Color LowHealthPointsLevelColor { get; set; } = Color.red;
        
        private const string ASSET_NAME = nameof(EnemySetup);
    }
}