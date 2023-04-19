using ShooterMockUp.Tools;
using ShooterMockUp.Utilities;
using ShooterMockUp.Weapon.Data;
using ShooterMockUp.Weapon.Projectiles;
using UnityEngine;

namespace ShooterMockUp.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [field: Header(ProjectConstants.HEADER_REFERENCES)]
        [field: SerializeField]
        public WeaponSetup CurrentWeaponSetup { get; set; }
        [field: SerializeField]
        private Transform BulletSpawnTransform { get; set; }
        [field: SerializeField]
        private ProjectilesPool CurrentProjectilesPool { get; set; }
        
        [field: SerializeField]
        public int LocalDamage { get; set; }

        public virtual void Shoot ()
        {
            Rigidbody projectile = GenerateProjectile();
            SetProjectileDamage(projectile);
            projectile.AddForce(BulletSpawnTransform.forward * CurrentWeaponSetup.ShootingForce, ForceMode.Impulse);
        }

        protected virtual void Awake ()
        {
            Initialize();
        }

        private void Initialize ()
        {
            LocalDamage = CurrentWeaponSetup.Damage;
        }

        private Rigidbody GenerateProjectile ()
        {
            Rigidbody projectile = CurrentProjectilesPool.GetObjectFromPool(CurrentWeaponSetup.Projectile.CurrentProjectileType);
            projectile.transform.SetPositionAndRotation(BulletSpawnTransform.position, BulletSpawnTransform.rotation);
            
            return projectile;
        }

        private void SetProjectileDamage (Rigidbody projectile)
        {
            projectile.GetComponent<Projectile>().LocalDamage = LocalDamage;
        }
    }
}