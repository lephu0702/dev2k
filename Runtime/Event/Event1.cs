namespace dev2k {
    using System;
    using UnityEngine;

    internal class Event<T> : IEvent {
        private Action<T> m_Action;

        public Event(Action<T> action) {
            m_Action = action;
        }

        public void Dispatch(string key, params object[] param) {
            if (param != null && param.Length == 1 && param[0] is T) {
                m_Action((T) param[0]);
            } else {
                Log.e("Param can not match");
            }
        }
    }
}