namespace dev2k {
    using UnityEngine;
    using TypeReferences;

    public class GameLoop : MonoSingleton<GameLoop> {
        [Inherits(typeof(Scene), ShortName = true)] [SerializeField]
        private TypeReference m_Scene;

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