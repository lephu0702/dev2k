using System;

namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EventMgr : IGameMgr, IEventMgr {
        private Dictionary<string, List<IEvent>> m_EventMap;

        public EventMgr(GameMain gameMain) : base(gameMain) {
            m_EventMap = new Dictionary<string, List<IEvent>>();
        }

        private void RegisterHandler(string key, IEvent handler) {
            if (m_EventMap.ContainsKey(key)) {
                m_EventMap[key].Add(handler);
            } else {
                var events = m_EventMap[key];
                events.Add(handler);
                m_EventMap.Add(key, events);
            }
        }

        public void Register(string key, Action action) {
            RegisterHandler(key, new Event(action));
        }

        public void Register<T>(string key, Action<T> action) {
            RegisterHandler(key, new Event<T>(action));
        }

        public void Register<T, U>(string key, Action<T, U> action) {
            RegisterHandler(key, new Event<T, U>(action));
        }

        public void Register<T, U, V>(string key, Action<T, U, V> action) {
            RegisterHandler(key, new Event<T, U, V>(action));
        }

        public void Register<T, U, V, K>(string key, Action<T, U, V, K> action) {
            RegisterHandler(key, new Event<T, U, V, K>(action));
        }

        public void Dispatch(string key, params object[] param) {
            if (m_EventMap.TryGetValue(key, out var events)) {
                foreach (var e in events) {
                    e.Dispatch(key, param);
                }
            } else {
                Log.e("No contains the event: " + key);
            }
        }

        public void Remove(string key) {
            if (m_EventMap.ContainsKey(key)) {
                m_EventMap[key].Clear();
                m_EventMap.Remove(key);
            } else {
                Log.e("No contains the event: " + key);
            }
        }
    }
}