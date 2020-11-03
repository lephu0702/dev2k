namespace dev2k {
    using UnityEngine;

    public class WorldRoot : MonoBehaviour {
        [SerializeField] private Camera m_WorldCamera;
        [SerializeField] private Transform m_EnviromentRoot;

        public Camera WorldCamera {
            get { return m_WorldCamera; }
        }

        public Transform EnviromentRoot {
            get { return m_EnviromentRoot; }
        }
    }
}