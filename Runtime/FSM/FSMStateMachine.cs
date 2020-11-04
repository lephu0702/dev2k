namespace dev2k {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FSMStateMachine<T> {
        public T m_Actor;

        private FSMState<T> m_CurrentState;
        private FSMState<T> m_PreviousState;
        private FSMState<T> m_GlobalState;
        private FSMStateFactory<T> m_StateFactory;
        private bool m_IsRunning = true;

        public FSMState<T> CurrentState {
            get { return m_CurrentState; }
        }

        public FSMState<T> GlobalState {
            get { return m_GlobalState; }
        }

        public FSMState<T> PreviousState {
            get { return m_PreviousState; }
        }

        public FSMStateFactory<T> StateFactory {
            get { return m_StateFactory; }
            set { m_StateFactory = value; }
        }

        public bool IsRunning {
            get { return m_IsRunning; }
            set { m_IsRunning = value; }
        }

        public FSMStateMachine(T actor) {
            m_Actor = actor;
            m_CurrentState = m_PreviousState = m_GlobalState = null;
        }

        public void ResetStateMachine(T actor, FSMStateFactory<T> factory) {
            m_Actor = actor;
            m_StateFactory = factory;
            m_CurrentState = m_PreviousState = m_GlobalState = null;
        }

        public void SetGlobalStateByID<K>(K key, bool forceCreate = false) where K : IConvertible {
            FSMState<T> state = GetStateFromFactory(key, forceCreate);
            if (state == null) {
                return;
            }

            SetGlobalState(state);
        }

        public void SetGlobalState(FSMState<T> state) {
            if (m_GlobalState != null) {
                m_GlobalState.Exit(m_Actor);
            }

            m_GlobalState = state;

            if (m_GlobalState != null) {
                m_GlobalState.Enter(m_Actor);
            }

            OnGlobalStateChange();
        }

        public void UpdateState(float dt) {
            if (!m_IsRunning) {
                return;
            }

            if (m_GlobalState != null) {
                m_GlobalState.Execute(m_Actor, dt);
            }

            if (m_CurrentState != null) {
                m_CurrentState.Execute(m_Actor, dt);
            }
        }

        public void SetCurrentStateByID<K>(K key, bool forceCreate = false) where K : IConvertible {
            FSMState<T> state = GetStateFromFactory(key, forceCreate);
            if (state == null) {
                Log.w("Not Find State By ID:" + key);
                return;
            }

            SetCurrentState(state);
        }

        public void SetCurrentState(FSMState<T> state) {
            if (state == m_CurrentState) {
                Log.i("Change To SameState!");
                return;
            }

            if (m_CurrentState != null) {
                m_CurrentState.Exit(m_Actor);
                m_PreviousState = m_CurrentState;
            }

            m_CurrentState = state;

            if (m_CurrentState != null) {
                m_CurrentState.Enter(m_Actor);
            }

            OnCurrentStateChange();
            OnPreviousStateChange();
        }

        public void RevertToPreviousState() {
            SetCurrentState(m_PreviousState);
        }

        public virtual void SendMsg(int key, params object[] args) {
        }

        protected virtual void OnGlobalStateChange() {
        }

        protected virtual void OnCurrentStateChange() {
        }

        protected virtual void OnPreviousStateChange() {
        }

        private FSMState<T> GetStateFromFactory<K>(K key, bool forceCreate) where K : IConvertible {
            if (m_StateFactory == null) {
                return null;
            }

            FSMState<T> state = m_StateFactory.GetState(key, forceCreate);
            return state;
        }
    }
}