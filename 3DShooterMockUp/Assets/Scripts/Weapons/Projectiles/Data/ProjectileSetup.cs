using ShooterMockUp.Utilities;
using UnityEngine;

namespace ShooterMockUp.Weapon.Projectiles.Data
{
    [CreateAssetMenu(menuName = ProjectConstants.SHOOTER_MOCK_UP_MENU_PATH + ASSET_NAME)]
    public class ProjectileSetup : ScriptableObject
    {
        [field: SerializeField]
        public int Damage { get; set; }
        [field: SerializeField]
        public float DamageRadius { get; set; }
        
        private const string ASSET_NAME = nameof(ProjectileSetup);
    }
}