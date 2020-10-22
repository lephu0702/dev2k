namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PoolContainer<T> {
        private T m_Item;
        
        public bool used { get; private set; }

        public T item {
            get { return m_Item; }
            set { m_Item = value; }
        }

        public void Consume() {
            used = true;
        }

        public void Release() {
            used = false;
        }
    }
}