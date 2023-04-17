using ShooterMockUp.Utilities;
using UnityEngine;

namespace ShooterMockUp.Enemies.Data
{
    [CreateAssetMenu(menuName = ProjectConstants.SHOOTER_MOCK_UP_MENU_PATH + ASSET_NAME)]
    public class EnemySetup: ScriptableObject
    {
        [field: SerializeField]
        public int HealthPoints {get; set; }
        
        private const string ASSET_NAME = nameof(EnemySetup);
    }
}