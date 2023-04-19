using ShooterMockUp.Utilities;
using UnityEngine;

namespace ShooterMockUp.Weapon.Projectiles.Data
{
    [CreateAssetMenu(menuName = ProjectConstants.SHOOTER_MOCK_UP_MENU_PATH + ASSET_NAME)]
    public class ProjectileSetup : ScriptableObject
    {
        [field: Header(ProjectConstants.HEADER_SETTINGS)]
        [field: SerializeField]
        public float DamageRadius { get; set; }
        [field: SerializeField]
        public float TimeToAutoDestroy { get; set; } = 3.0f;
        
        private const string ASSET_NAME = nameof(ProjectileSetup);
    }
}