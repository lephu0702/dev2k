namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PrefabPoolFactory : IPoolFactory<GameObject> {
        protected string m_Name;
        protected GameObject m_Prefab;

        public PrefabPoolFactory(GameObject prefab) : this(prefab.name, prefab) {
        }

        public PrefabPoolFactory(string name, GameObject prefab) {
            m_Name = name;
            m_Prefab = prefab;
        }

        public GameObject Create() {
            return CreateNewGameObject();
        }

        private GameObject CreateNewGameObject() {
            var obj = Object.Instantiate(m_Prefab) as GameObject;
            obj.name = m_Name;
            return obj;
        }
    }
}