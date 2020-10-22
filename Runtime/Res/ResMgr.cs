namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ResMgr : IGameMgr {
        private Hashtable m_ResHashtable;
        private Dictionary<string, GameObject> m_GoCacheDict;

        public ResMgr(GameMgr gameMgr) : base(gameMgr) {
            m_ResHashtable = new Hashtable();
            m_GoCacheDict = new Dictionary<string, GameObject>();
        }

        public override void Release() {
            m_ResHashtable.Clear();
            m_GoCacheDict.Clear();
        }

        public T LoadRes<T>(string path, bool isCache = false) where T : Object {
            if (m_ResHashtable.Contains(path)) {
                return m_ResHashtable[path] as T;
            }

            T res = Resources.Load<T>(path);
            if (res == null) {
                Log.e("Not Found Resources! Path = " + path);
            } else if (isCache) {
                m_ResHashtable.Add(path, res);
            }

            return res;
        }

        public GameObject LoadPrefab(string path, bool isCache = false) {
            if (m_GoCacheDict.ContainsKey(path)) {
                m_GoCacheDict[path].SetActive(true);
                return m_GoCacheDict[path];
            }

            GameObject go = Resources.Load<GameObject>(path);
            if (go == null) {
                Log.e("Not Found Prefab! Path = " + path);
            } else {
                go = Object.Instantiate(go);
                if (isCache) {
                    m_GoCacheDict.Add(path, go);
                }
            }

            return go;
        }

        public GameObject LoadPrefab(string path, Vector3 position, Quaternion rotation, bool isCache = false) {
            if (m_GoCacheDict.ContainsKey(path)) {
                m_GoCacheDict[path].transform.position = position;
                m_GoCacheDict[path].transform.rotation = rotation;
                m_GoCacheDict[path].SetActive(true);
                return m_GoCacheDict[path];
            }

            GameObject go = Resources.Load<GameObject>(path);
            if (go == null) {
                Log.e("Not Found Prefab! Path = " + path);
            } else {
                go = Object.Instantiate(go, position, rotation);
                if (isCache) {
                    m_GoCacheDict.Add(path, go);
                }
            }

            return go;
        }
    }
}