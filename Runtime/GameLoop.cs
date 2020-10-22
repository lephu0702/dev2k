namespace dev2k {
    using UnityEngine;

    public class GameLoop : MonoSingleton<GameLoop> {
        [SerializeField] private BuildScene m_BuildScene;

        public SceneCtrl Scene { get; private set; }

        private void Awake() {
            Scene = SceneCtrl.S;
        }

        private void Start() {
            Scene.SetScene(m_BuildScene, false);
        }

        private void FixedUpdate() {
            Scene.FixedUpdate();
        }

        private void Update() {
            Scene.Update();
        }
    }
}