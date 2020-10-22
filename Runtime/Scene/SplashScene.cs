namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SplashScene : IScene {
        public SplashScene(SceneCtrl controller) : base(controller) {
            Name = BuildScene.Splash.ToString();
        }

        public override void Begin() {
            base.Begin();
        }

        public override void Update() {
            base.Update();
        }

        public override void FixedUpdate() {
            base.FixedUpdate();
        }

        public override void End() {
            base.End();
        }
    }
}