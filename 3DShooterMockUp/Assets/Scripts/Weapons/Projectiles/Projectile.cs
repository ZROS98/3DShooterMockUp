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
        [field: SerializeField]
        private float DamageRadius { get; set; }
        
        public ObjectPool CurrentObjectPool { get; set; }
        
        private float TimeToAutoDestroy { get; set; } = 3.0f;

        public void StartAutoDestroy (ObjectPool objectPool)
        {
            StartCoroutine(AutoDestroyProcess(objectPool));
        }

        private IEnumerator AutoDestroyProcess (ObjectPool objectPool)
        {
            yield return new WaitForSeconds(TimeToAutoDestroy);
            objectPool.ReturnObjectToPool(CurrentProjectileType, gameObject);
            StopAllCoroutines();
        }

        private void OnCollisionEnter (Collision other)
        {
            CheckForEnemies();
        }

        private void CheckForEnemies ()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, DamageRadius);

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