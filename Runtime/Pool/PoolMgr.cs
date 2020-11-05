namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PoolMgr : IGameMgr {
        private Dictionary<GameObject, Pool<GameObject>> m_PrefabLookup;
        private Dictionary<GameObject, Pool<GameObject>> m_InstanceLookup;

        public PoolMgr(GameMain gameMain) : base(gameMain) {
            m_PrefabLookup = new Dictionary<GameObject, Pool<GameObject>>();
            m_InstanceLookup = new Dictionary<GameObject, Pool<GameObject>>();
        }

        private void createPool(GameObject prefab, int size) {
            if (m_PrefabLookup.ContainsKey(prefab)) {
                Log.w("Pool for prefab " + prefab.name + " has already been created");
            }

            var pool = new Pool<GameObject>(new PrefabPoolFactory(prefab), size);
            m_PrefabLookup[prefab] = pool;
        }

        private GameObject spawnObject(GameObject prefab) {
            return spawnObject(prefab, Vector3.zero, Quaternion.identity);
        }

        private GameObject spawnObject(GameObject prefab, Vector3 position, Quaternion rotation) {
            if (!m_PrefabLookup.ContainsKey(prefab)) {
                createPool(prefab, 1);
            }

            var pool = m_PrefabLookup[prefab];
            var clone = pool.Get();
            clone.transform.position = position;
            clone.transform.rotation = rotation;
            clone.SetActive(true);
            m_InstanceLookup.Add(clone, pool);
            return clone;
        }

        private void despawnObject(GameObject obj) {
            obj.SetActive(false);
            if (m_InstanceLookup.ContainsKey(obj)) {
                m_InstanceLookup[obj].Release(obj);
                m_InstanceLookup.Remove(obj);
            } else {
                Log.w("No pool contains the object: " + obj.name);
            }
        }

        #region Method API

        public void CreatePool(GameObject prefab, int size) {
            createPool(prefab, size);
        }

        public GameObject Spawn(GameObject prefab) {
            return spawnObject(prefab);
        }

        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation) {
            return spawnObject(prefab, position, rotation);
        }

        public void Despawn(GameObject obj) {
            despawnObject(obj);
        }

        #endregion
    }
}