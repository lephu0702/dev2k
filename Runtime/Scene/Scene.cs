using UnityEngine;

namespace dev2k {
    public abstract class Scene {
        public string Name { get; set; }
        public SceneCtrl Controller { set; get; }

        protected GameMgr GameMgr;

        protected Scene(SceneCtrl controller) {
            Controller = controller;
        }

        public virtual void Begin() {
            GameMgr = GameMgr.S;
            GameMgr.Initialize();
        }

        public virtual void End() {
            GameMgr.Dispose();
        }

        public virtual void FixedUpdate() {
            GameMgr.FixedUpdate();
        }

        public virtual void Update() {
            GameMgr.Update();
        }

        public override string ToString() {
            return Name;
        }
    }
}