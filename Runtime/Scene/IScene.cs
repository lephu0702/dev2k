namespace dev2k {
    public abstract class IScene {
        public string Name { set; get; }
        public SceneCtrl Controller { set; get; }
        public GameMgr Mgr { get; private set; }

        protected IScene(SceneCtrl controller) {
            Controller = controller;
        }

        public virtual void Begin() {
            Mgr = GameMgr.S;
            Mgr.Initialize();
        }

        public virtual void End() {
            Mgr.Dispose();
        }

        public virtual void FixedUpdate() {
            Mgr.FixedUpdate();
        }

        public virtual void Update() {
            Mgr.Update();
        }

        public override string ToString() {
            return Name;
        }
    }
}