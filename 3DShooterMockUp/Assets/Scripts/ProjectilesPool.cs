using System.Collections.Generic;
using ShooterMockUp.Utilities;
using ShooterMockUp.Weapon.Projectiles;
using UnityEngine;

namespace ShooterMockUp.Tools
{
    public class ProjectilesPool : MonoBehaviour
    {
        [field: Header(ProjectConstants.HEADER_REFERENCES)]
        [field: SerializeField]
        private List<Projectile> ProjectilePrefabsCollection { get; set; }

        [field: Header(ProjectConstants.HEADER_SETTINGS)]
        [field: SerializeField]
        public int MaxPoolSize { get; set; } = 10;

        private Dictionary<ProjectileType, List<Rigidbody>> PooledObjects { get; set; } = new Dictionary<ProjectileType, List<Rigidbody>>();
        private Dictionary<ProjectileType, Rigidbody> Prefabs { get; set; } = new Dictionary<ProjectileType, Rigidbody>();

        protected virtual void Awake ()
        {
            foreach (Projectile projectile in ProjectilePrefabsCollection)
            {
                AddObjectToPool(projectile.CurrentProjectileType, projectile.CurrentRigidbody);
            }
        }

        private void AddObjectToPool (ProjectileType projectileType, Rigidbody prefab)
        {
            if (PooledObjects.ContainsKey(projectileType))
            {
                return;
            }

            PooledObjects.Add(projectileType, new List<Rigidbody>());
            Prefabs.Add(projectileType, prefab);
        }

        public Rigidbody GetObjectFromPool (ProjectileType projectileType)
        {
            if (PooledObjects.TryGetValue(projectileType, out List<Rigidbody> objectList))
            {
                if (objectList.Count > 0)
                {
                    Rigidbody currentObject = objectList[0];
                    objectList.RemoveAt(0);
                    currentObject.gameObject.SetActive(true);

                    return currentObject;
                }
                else
                {
                    return InstantiateNewObject(projectileType);
                }
            }

            return null;
        }

        private Rigidbody InstantiateNewObject (ProjectileType projectileType)
        {
            Rigidbody prefab = Prefabs[projectileType];
            Rigidbody rigidbody = prefab.GetComponent<Rigidbody>();

            if (prefab != null)
            {
                Rigidbody newObject = Instantiate(prefab, transform, true);
                newObject.name = prefab.name;
                SetReferenceToObjectPool(newObject);

                return rigidbody;
            }

            return null;
        }

        private void SetReferenceToObjectPool (Rigidbody projectile)
        {
            if (projectile.TryGetComponent(out Projectile currentProjectile))
            {
                if (currentProjectile.CurrentProjectilesPool == null)
                {
                    currentProjectile.CurrentProjectilesPool = this;
                }
            }
        }

        public void ReturnObjectToPool (ProjectileType projectileType, Rigidbody currentObject)
        {
            if (PooledObjects.ContainsKey(projectileType) && currentObject != null)
            {
                List<Rigidbody> objectList = PooledObjects[projectileType];
                currentObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                currentObject.velocity = Vector3.zero;

                if (objectList.Count < MaxPoolSize)
                {
                    currentObject.gameObject.SetActive(false);
                    objectList.Add(currentObject);
                }
                else
                {
                    Destroy(currentObject);
                }
            }
        }
    }
}