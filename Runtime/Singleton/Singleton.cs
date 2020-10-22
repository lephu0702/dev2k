namespace dev2k {
    public class Singleton<T> : ISingleton where T : Singleton<T> {
        protected static T m_Instance;
        protected static object m_Lock = new object();

        public static T S {
            get {
                if (m_Instance == null) {
                    lock (m_Lock) {
                        m_Instance = SingletonCreator.CreateSingleton<T>();
                    }
                }

                return m_Instance;
            }
        }

        public virtual void OnSingletonInit() {
        }

        public virtual void Dispose() {
            m_Instance = null;
        }
    }
}