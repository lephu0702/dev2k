namespace dev2k {
    using UnityEngine;
    using System.Reflection;

    public static class MonoSingletonCreator {
        private static bool m_IsApplicationQuit = false;

        public static bool isApplicationQuit {
            get { return m_IsApplicationQuit; }
            set { m_IsApplicationQuit = value; }
        }

        public static T CreateMonoSingleton<T>() where T : MonoBehaviour, ISingleton {
            T instance = null;
            if (instance == null && !m_IsApplicationQuit) {
                instance = GameObject.FindObjectOfType<T>();
                if (instance == null) {
                    MemberInfo info = typeof(T);
                    var attributes = info.GetCustomAttributes(true);
                    foreach (var attribute in attributes) {
                        var defineAttri = attribute as MonoSingletonAttribute;
                        if (defineAttri == null) {
                            continue;
                        }

                        instance = CreateComponentOnGameObject<T>(defineAttri.AbsolutePath, true);
                        break;
                    }
                }
            }

            return instance;
        }

        private static T CreateComponentOnGameObject<T>(string path, bool dontDestroy) where T : MonoBehaviour {
            var obj = GameObjectHelper.FindGameObject(null, path, true, dontDestroy);
            if (obj == null) {
                obj = new GameObject("Singleton of " + typeof(T).Name);
                if (dontDestroy) {
                    Object.DontDestroyOnLoad(obj);
                }
            }

            return obj.AddComponent<T>();
        }
    }
}