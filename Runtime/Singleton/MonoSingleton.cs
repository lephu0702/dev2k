namespace dev2k {
    using UnityEngine;

    public class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T> {
        protected static T m_Instance = null;

        private static bool m_IsApplicationQuit = false;

        public static bool isApplicationQuit {
            get { return m_IsApplicationQuit; }
            set { m_IsApplicationQuit = value; }
        }

        public static T S {
            get {
                if (m_Instance == null) {
                    m_Instance = MonoSingletonCreator.CreateMonoSingleton<T>();
                }

                return m_Instance;
            }
        }

        public virtual void OnSingletonInit() {
        }

        public virtual void Dispose() {
            Destroy(gameObject);
        }

        protected virtual void OnDestroy() {
            m_Instance = null;
        }
    }
}