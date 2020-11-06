namespace dev2k {
    using UnityEngine;

    public class Scene : MonoBehaviour, IScene {
        private GameMain m_Main;

        public virtual string Name {
            get { return string.Empty; }
        }

        public GameMain GameMain {
            get { return m_Main; }
        }

        public SceneCtrl Controller { get; set; }

        public virtual void Begin() {
            m_Main = GameMain.S;
            GameMain.Initialize();
        }

        public virtual void End() {
            GameMain.Dispose();
        }

        public virtual void FixedUpdate() {
            GameMain.FixedUpdate();
        }

        public virtual void Update() {
            GameMain.Update();
        }

        public override string ToString() {
            return Name;
        }
    }
}