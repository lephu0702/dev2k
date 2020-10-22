namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Pool<T> {
        protected List<PoolContainer<T>> m_List;
        protected Dictionary<T, PoolContainer<T>> m_Lookup;
        protected IPoolFactory<T> m_Factory;
        protected int m_LastIndex;

        public int count {
            get { return m_List.Count; }
        }

        public int countUsedItems {
            get { return m_Lookup.Count; }
        }

        public Pool(IPoolFactory<T> factory, int initSize) {
            m_Factory = factory;
            m_List = new List<PoolContainer<T>>();
            m_Lookup = new Dictionary<T, PoolContainer<T>>();

            for (int i = 0; i < initSize; i++) {
                CreateContainer();
            }
        }

        private PoolContainer<T> CreateContainer() {
            var container = new PoolContainer<T> {item = m_Factory.Create()};
            m_List.Add(container);
            return container;
        }

        public T Get() {
            PoolContainer<T> container = null;
            for (int i = 0; i < m_List.Count; i++) {
                m_LastIndex++;

                if (m_LastIndex > m_List.Count - 1) {
                    m_LastIndex = 0;
                }

                if (m_List[m_LastIndex].used) {
                    continue;
                } else {
                    container = m_List[m_LastIndex];
                    break;
                }
            }

            if (container == null) {
                container = CreateContainer();
            }

            container.Consume();
            m_Lookup.Add(container.item, container);
            return container.item;
        }

        public void Release(object item) {
            Release((T) item);
        }

        public void Release(T item) {
            if (m_Lookup.ContainsKey(item)) {
                var container = m_Lookup[item];
                container.Release();
                m_Lookup.Remove(item);
            }
        }
    }
}