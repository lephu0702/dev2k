namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AbstractMonoCom : MonoBehaviour, ICom {
        private AbstractActor m_AbstractActor;

        public virtual AbstractActor Actor {
            get { return m_AbstractActor; }
        }

        public virtual int ComOrder {
            get { return ComOrderDefine.DEFAULT; }
        }

        public void AwakeCom(AbstractActor actor) {
            m_AbstractActor = actor;
            OnComAwake();
        }

        public virtual void OnComStart() {
        }

        public virtual void OnComEnable() {
        }

        public virtual void OnComUpdate(float dt) {
        }

        public virtual void OnComLateUpdate(float dt) {
        }

        public virtual void OnComDisable() {
        }

        public virtual void DestroyCom() {
            OnComDestroy();
            m_AbstractActor = null;
        }

        protected virtual void OnComAwake() {
        }

        protected virtual void OnComDestroy() {
        }
    }
}