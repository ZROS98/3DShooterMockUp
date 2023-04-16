using ShooterMockUp.Utilities;
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
           // GameObject projectile = Instantiate(CurrentWeaponSetup.Projectile.gameObject, BulletSpawnTransform.position, BulletSpawnTransform.rotation);
            GameObject projectile = CurrentObjectPool.GetObjectFromPool(CurrentWeaponSetup.Projectile.PoolIndex);
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
            CurrentObjectPool.AddObjectToPool(CurrentWeaponSetup.Projectile.gameObject);
        }
    }
}