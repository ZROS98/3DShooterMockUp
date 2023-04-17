using System.Collections;
using ShooterMockUp.Tools;
using UnityEngine;

namespace ShooterMockUp.Weapon.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField]
        public ProjectileType CurrentProjectileType { get; set; }
        private float TimeToAutoDestroy { get; set; } = 3.0f;

        public void StartAutoDestroy (ObjectPool objectPool)
        {
            StartCoroutine(AutoDestroyProcess(objectPool));
        }

        private IEnumerator AutoDestroyProcess(ObjectPool objectPool)
        {
            yield return new WaitForSeconds(TimeToAutoDestroy);
            objectPool.ReturnObjectToPool(CurrentProjectileType, gameObject);
            StopAllCoroutines();
        }
    }
}