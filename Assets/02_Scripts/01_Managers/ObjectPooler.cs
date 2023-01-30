using System;
using System.Collections.Generic;
using System.Linq;
using LoxiGames.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LoxiGames.Manager
{
    public class ObjectPooler : MonoSingleton<ObjectPooler>
    {
        [Serializable]
        public class Pool
        {
            public string tag;
            public PoolObject poolObject;
            public int size;
        }

        public static event Action OnObjectSpawned;
        [SerializeField] private List<Pool> pools;
        private Dictionary<string, Queue<PoolObject>> _poolDictionary;

        protected override void Awake()
        {
            base.Awake();

            _poolDictionary = new Dictionary<string, Queue<PoolObject>>();

            foreach (var pool in pools)
            {
                var objectPool = new Queue<PoolObject>();

                for (var i = 0; i < pool.size; i++)
                {
                    var obj = Instantiate(pool.poolObject, transform);
                    obj.Deactivate();
                    objectPool.Enqueue(obj);
                }

                _poolDictionary.Add(pool.tag, objectPool);
            }
        }

        [Button, DisableInEditorMode]
        public PoolObject SpawnFromPool(string objectTag, Vector3 position, Transform parent = null)
        {
            if (!_poolDictionary.ContainsKey(objectTag))
            {
                Debug.LogError("Check the tag!: " + objectTag);
                return null;
            }

            var objectToSpawn = _poolDictionary[objectTag].Dequeue();

            var tr = objectToSpawn.transform;
            tr.position = position;
            //tr.rotation = rotation;
            tr.SetParent(parent);
            objectToSpawn.Activate();

            _poolDictionary[objectTag].Enqueue(objectToSpawn);
            OnObjectSpawned?.Invoke();

            return objectToSpawn;
        }

        [Button, DisableInEditorMode]
        public PoolObject SpawnFromPool(string objectTag, Vector3 position, Quaternion rotation,
            Transform parent = null)
        {
            if (!_poolDictionary.ContainsKey(objectTag))
            {
                Debug.LogError("Check the tag!: " + objectTag);
                return null;
            }

            var objectToSpawn = _poolDictionary[objectTag].Dequeue();

            var tr = objectToSpawn.transform;
            tr.position = position;
            tr.rotation = rotation;
            tr.SetParent(parent);
            objectToSpawn.Activate();

            _poolDictionary[objectTag].Enqueue(objectToSpawn);
            OnObjectSpawned?.Invoke();

            return objectToSpawn;
        }

        public void ResetPools()
        {
            foreach (var obj in pools.Select(pool => _poolDictionary[pool.tag]).SelectMany(objectPool => objectPool))
            {
                obj.Deactivate();
            }
        }
    }
}