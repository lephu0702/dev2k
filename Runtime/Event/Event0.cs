namespace dev2k {
    using System;
    using UnityEngine;

    internal class Event : IEvent {
        private Action m_Action;

        public Event(Action action) {
            m_Action = action;
        }

        public void Dispatch(string key, params object[] param) {
            if (param == null || param.Length == 0) {
                m_Action();
            } else {
                Log.e("Param can not match");
            }
        }
    }
}