namespace dev2k {
    public class SingletonProperty<T> where T : class, ISingleton {
        private static T m_Instance;
        private static readonly object m_Lock = new object();

        public static T S {
            get {
                lock (m_Lock) {
                    if (m_Instance == null) {
                        m_Instance = SingletonCreator.CreateSingleton<T>();
                    }
                }

                return m_Instance;
            }
        }

        public static void Dispose() {
            m_Instance = null;
        }
    }
}