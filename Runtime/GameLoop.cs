namespace dev2k {
    using UnityEngine;

    public class GameLoop : MonoSingleton<GameLoop> {
        // [SerializeField] private BuildScene m_BuildScene;
        [SerializeField] private Scene m_Scene;

        public SceneCtrl Scene { get; private set; }

        private void Awake() {
            Scene = new SceneCtrl();
        }

        private void Start() {
            Scene.SetScene(m_Scene, false);
        }

        private void FixedUpdate() {
            Scene.FixedUpdate();
        }

        private void Update() {
            Scene.Update();
        }
    }
}