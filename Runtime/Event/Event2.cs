namespace dev2k {
    using System;

    internal class Event<T, U> : IEvent {
        private Action<T, U> m_Action;

        public Event(Action<T, U> action) {
            m_Action = action;
        }

        public void Dispatch(string key, params object[] param) {
            if (param != null && param.Length == 1 && param[0] is T && param[1] is U) {
                m_Action((T) param[0], (U) param[1]);
            } else {
                Log.e("Param can not match");
            }
        }
    }
}