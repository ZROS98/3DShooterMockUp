using System.Collections.Generic;
using ShooterMockUp.Weapon.Projectiles;
using UnityEngine;

namespace ShooterMockUp.Tools
{
    public class ObjectPool : MonoBehaviour
    {
        private Dictionary<ProjectileType, List<GameObject>> PooledObjects { get; set; } = new Dictionary<ProjectileType, List<GameObject>>();
        private Dictionary<ProjectileType, GameObject> Prefabs { get; set; } = new Dictionary<ProjectileType, GameObject>();

        public int maxPoolSize = 10;

        public void AddObjectToPool (ProjectileType projectileType, GameObject prefab)
        {
            if (PooledObjects.ContainsKey(projectileType))
            {
                return;
            }

            PooledObjects.Add(projectileType, new List<GameObject>());
            Prefabs.Add(projectileType, prefab);
        }

        public GameObject GetObjectFromPool (ProjectileType projectileType)
        {
            if (PooledObjects.TryGetValue(projectileType, out List<GameObject> gameObjectList))
            {
                if (gameObjectList.Count > 0)
                {
                    GameObject currentGameObject = gameObjectList[0];
                    gameObjectList.RemoveAt(0);
                    currentGameObject.SetActive(true);
                    return currentGameObject;
                }
                else
                {
                    return InstantiateNewObject(projectileType);
                }
            }

            return null;
        }

        private GameObject InstantiateNewObject (ProjectileType projectileType)
        {
            GameObject prefab = Prefabs[projectileType];

            if (prefab != null)
            {
                GameObject newObject = Instantiate(prefab);
                newObject.name = prefab.name;
                return newObject;
            }

            return null;
        }

        public void ReturnObjectToPool (ProjectileType projectileType, GameObject currentGameObject)
        {
            if (PooledObjects.ContainsKey(projectileType) && currentGameObject != null)
            {
                List<GameObject> objectList = PooledObjects[projectileType];

                if (objectList.Count < maxPoolSize)
                {
                    currentGameObject.SetActive(false);
                    objectList.Add(currentGameObject);
                }
                else
                {
                    Destroy(currentGameObject);
                }
            }
        }
    }
}