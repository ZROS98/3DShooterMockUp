using System.Collections;
using ShooterMockUp.Tools;
using ShooterMockUp.Weapon.Projectiles.Data;
using UnityEngine;

namespace ShooterMockUp.Weapon.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField]
        public ProjectileType CurrentProjectileType { get; set; }
        [field: SerializeField]
        private ProjectileSetup CurrentProjectileSetup { get; set; }
        
        public ObjectPool CurrentObjectPool { get; set; }
        
        private float TimeToAutoDestroy { get; set; } = 3.0f;

        protected virtual void OnEnable ()
        {
            StartAutoDestroy();
        }
        
        private void StartAutoDestroy ( )
        {
            StartCoroutine(AutoDestroyProcess());
        }

        private IEnumerator AutoDestroyProcess ()
        {
            yield return new WaitForSeconds(TimeToAutoDestroy);
            CurrentObjectPool.ReturnObjectToPool(CurrentProjectileType, gameObject);
            StopAllCoroutines();
        }

        private void OnCollisionEnter (Collision other)
        {
            CheckForEnemies();
            CurrentObjectPool.ReturnObjectToPool(CurrentProjectileType, gameObject);
        }

        private void CheckForEnemies ()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, CurrentProjectileSetup.DamageRadius);

            foreach (Collider currentCollider in colliders)
            {
                if (currentCollider.TryGetComponent<Enemy.Enemy>(out Enemy.Enemy enemy))
                {
                    enemy.HandleGettingDamage(CurrentProjectileSetup.Damage);
                }
            }
        }
    }
}