using ShooterMockUp.Tools;
using ShooterMockUp.Weapon.Data;
using ShooterMockUp.Weapon.Projectiles;
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
        private ObjectPool CurrentObjectPool { get; set; }

        public virtual void Shoot ()
        {
            GameObject projectile = CurrentObjectPool.GetObjectFromPool(CurrentWeaponSetup.Projectile.CurrentProjectileType);
            projectile.transform.position = BulletSpawnTransform.position;
            projectile.transform.rotation = BulletSpawnTransform.rotation;
            Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
            projectileRigidbody.AddForce(BulletSpawnTransform.forward * CurrentWeaponSetup.ShootingForce, ForceMode.Impulse);
            
            projectile.GetComponent<Projectile>().StartAutoDestroy(CurrentObjectPool);
        }

        protected virtual void Start ()
        {
            AddBulletToPool();
        }

        private void AddBulletToPool ()
        {
            Projectile targetProjectile = CurrentWeaponSetup.Projectile;
            CurrentObjectPool.AddObjectToPool(targetProjectile.CurrentProjectileType, targetProjectile.gameObject);
        }
    }
}