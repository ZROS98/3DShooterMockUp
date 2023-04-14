using SUtilities.Utilities;
using UnityEngine;

namespace ShooterMockUp.Weapon
{
    [CreateAssetMenu(menuName = ProjectConstants.SHOOTER_MOCK_UP_MENU_PATH + ASSET_NAME)]
    public class WeaponSetup : ScriptableObject
    {
        [field: Header(ProjectConstants.HEADER_REFERENCES)]
        [field: SerializeField]
        private GameObject Projectile { get; set; }

        [field: Header(ProjectConstants.HEADER_SETTINGS)]
        [field: SerializeField,]
        private float Damage { get; set; }
        [field: SerializeField]
        private float Speed { get; set; }
        [field: SerializeField]
        private float ReloadTime { get; set; }
        [field: SerializeField]
        private float MagazineCapacity { get; set; }

        private const string ASSET_NAME = nameof(WeaponSetup);
    }
}