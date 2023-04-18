using ShooterMockUp.Tools;
using ShooterMockUp.Weapon.Data;
using UnityEngine;

namespace ShooterMockUp.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField]
        public WeaponSetup CurrentWeaponSetup { get; set; }
        [field: SerializeField]
        private Transform BulletSpawnTransform { get; set; }
        [field: SerializeField]
        private ProjectilesPool CurrentProjectilesPool { get; set; }

        public virtual void Shoot ()
        {
            Rigidbody projectile = GenerateProjectile();
            projectile.AddForce(BulletSpawnTransform.forward * CurrentWeaponSetup.ShootingForce, ForceMode.Impulse);
        }

        private Rigidbody GenerateProjectile ()
        {
            Rigidbody projectile = CurrentProjectilesPool.GetObjectFromPool(CurrentWeaponSetup.Projectile.CurrentProjectileType);
            projectile.transform.SetPositionAndRotation(BulletSpawnTransform.position, BulletSpawnTransform.rotation);
            
            return projectile;
        }
    }
}