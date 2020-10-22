namespace dev2k {
    using System;
    using UnityEngine;

    public interface IEventMgr {
        void Register(string key, Action action);
        void Register<T>(string key, Action<T> action);
        void Register<T, U>(string key, Action<T, U> action);
        void Register<T, U, V>(string key, Action<T, U, V> action);
        void Register<T, U, V, K>(string key, Action<T, U, V, K> action);
        void Dispatch(string key, params object[] param);
        void Remove(string key);
    }
}