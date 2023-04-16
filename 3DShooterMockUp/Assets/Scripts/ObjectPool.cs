using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShooterMockUp.Utilities
{
    public class ObjectPool : MonoBehaviour
    {
        [field: SerializeField]
        private List<List<GameObject>> PooledObjects = new List<List<GameObject>>();
        
        [field: SerializeField]
        private List<GameObject> Prefabs = new List<GameObject>();

        public int maxPoolSize = 10;

        public void AddObjectToPool (GameObject prefab)
        {
            PooledObjects.Add(new List<GameObject>());
            Prefabs.Add(prefab);
        }

        public GameObject GetObjectFromPool (int index)
        {
            if (index >= 0 && index < PooledObjects.Count)
            {
                List<GameObject> objectList = PooledObjects[index];

                if (objectList.Count > 0)
                {
                    GameObject obj = objectList[0];
                    objectList.RemoveAt(0);
                    obj.SetActive(true);
                    return obj;
                }
                else
                {
                    GameObject prefab = Prefabs[index];

                    if (prefab != null)
                    {
                        GameObject newObj = Instantiate(prefab);
                        newObj.name = prefab.name;
                        return newObj;
                    }
                }
            }

            return null;
        }

        public void ReturnObjectToPool (int index, GameObject obj)
        {
            if (index >= 0 && index < PooledObjects.Count && obj != null)
            {
                List<GameObject> objectList = PooledObjects[index];

                if (objectList.Count < maxPoolSize)
                {
                    obj.SetActive(false);
                    objectList.Add(obj);
                }
                else
                {
                    Destroy(obj);
                }
            }
        }
    }
}