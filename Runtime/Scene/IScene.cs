namespace dev2k {
    public interface IScene {
        string Name { get; }
        GameMain GameMain { get; }

        SceneCtrl Controller { set; get; }
        // protected IScene(SceneCtrl controller) {
        //     Controller = controller;
        // }
        //
        // public virtual void Begin() {
        //     GameMain = GameMain.S;
        //     GameMain.Initialize();
        // }
        //
        // public virtual void End() {
        //     GameMain.Dispose();
        // }
        //
        // public virtual void FixedUpdate() {
        //     GameMain.FixedUpdate();
        // }
        //
        // public virtual void Update() {
        //     GameMain.Update();
        // }
        //
        // public override string ToString() {
        //     return Name;
        // }
    }
}