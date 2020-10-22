namespace dev2k {
    using System;

    internal class Event<T, U, V> : IEvent {
        private Action<T, U, V> m_Action;

        public Event(Action<T, U, V> action) {
            m_Action = action;
        }

        public void Dispatch(string key, params object[] param) {
            if (param != null && param.Length == 1 && param[0] is T && param[1] is U && param[2] is V) {
                m_Action((T) param[0], (U) param[1], (V) param[2]);
            } else {
                Log.e("Param can not match");
            }
        }
    }
}