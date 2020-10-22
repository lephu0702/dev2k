namespace dev2k {
    using System;

    internal class Event<T, U, V, K> : IEvent {
        private Action<T, U, V, K> m_Action;

        public Event(Action<T, U, V, K> action) {
            m_Action = action;
        }

        public void Dispatch(string key, params object[] param) {
            if (param != null && param.Length == 1 && param[0] is T && param[1] is U && param[2] is V &&
                param[3] is K) {
                m_Action((T) param[0], (U) param[1], (V) param[2], (K) param[3]);
            } else {
                Log.e("Param can not match");
            }
        }
    }
}