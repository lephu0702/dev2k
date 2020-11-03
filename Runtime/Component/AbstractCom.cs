namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AbstractCom : ICom {
        private AbstractActor m_Actor;

        public AbstractActor Actor {
            get { return m_Actor; }
        }

        public virtual int ComOrder {
            get { return ComOrderDefine.DEFAULT; }
        }

        public void AwakeCom(AbstractActor actor) {
            m_Actor = actor;
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
            m_Actor = null;
        }

        protected virtual void OnComAwake() {
        }

        protected virtual void OnComDestroy() {
        }
    }
}