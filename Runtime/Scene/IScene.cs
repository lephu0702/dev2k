namespace dev2k {
    public abstract class IScene {
        public string Name { set; get; }
        public SceneCtrl Controller { set; get; }
        public GameMain Main { get; private set; }

        protected IScene(SceneCtrl controller) {
            Controller = controller;
        }

        public virtual void Begin() {
            Main = GameMain.S;
            Main.Initialize();
        }

        public virtual void End() {
            Main.Dispose();
        }

        public virtual void FixedUpdate() {
            Main.FixedUpdate();
        }

        public virtual void Update() {
            Main.Update();
        }

        public override string ToString() {
            return Name;
        }
    }
}