namespace dev2k {
    public abstract class IScene {
        public string Name { set; get; }
        public SceneCtrl Controller { set; get; }
        public GameMain GameMain { get; private set; }

        protected IScene(SceneCtrl controller) {
            Controller = controller;
        }
        
        public virtual void Begin() {
            GameMain = GameMain.S;
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