namespace dev2k {
    using System;

    public interface IEvent {
        void Dispatch(string key, params object[] param);
    }
}