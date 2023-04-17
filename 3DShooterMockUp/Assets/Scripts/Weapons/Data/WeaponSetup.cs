using ShooterMockUp.Utilities;
using ShooterMockUp.Weapon.Projectiles;
using UnityEngine;

namespace ShooterMockUp.Weapon.Data
{
    [CreateAssetMenu(menuName = ProjectConstants.SHOOTER_MOCK_UP_MENU_PATH + ASSET_NAME)]
    public class WeaponSetup : ScriptableObject
    {
        [field: Header(ProjectConstants.HEADER_REFERENCES)]
        [field: SerializeField]
        public Projectile Projectile { get; set; }

        [field: Header(ProjectConstants.HEADER_SETTINGS)]
        [field: SerializeField]
        public float ShootingForce { get; set; }
        [field: SerializeField]
        public float ReloadTime { get; set; }
        [field: SerializeField]
        public float MagazineCapacity { get; set; }

        private const string ASSET_NAME = nameof(WeaponSetup);
    }
}