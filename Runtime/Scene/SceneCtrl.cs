namespace dev2k {
    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneCtrl {
        public Scene CurrentScene { get; private set; }

        private bool m_IsSceneBegin;

        public SceneCtrl() {
            m_IsSceneBegin = false;
        }

        public void SetScene(Type type, bool isNow = true, bool isAsync = false) {
            var scene = (Scene) Activator.CreateInstance(type, this);

            if (isNow) {
                if (isAsync) {
                    LoadingScene.NextScene = scene?.Name;
                    LoadScene("Loading");
                } else {
                    LoadScene(scene?.Name);
                }
            }

            m_IsSceneBegin = false;
            CurrentScene = scene;
        }

        private bool LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single) {
            try {
                SceneManager.LoadScene(sceneName, mode);
            } catch (Exception e) {
                Log.e("SceneManager LoadScene Failed! SceneName:" + sceneName);
                Log.e(e);
                return false;
            }

            return true;
        }

        public void FixedUpdate() {
            if (CurrentScene != null && m_IsSceneBegin) {
                CurrentScene.FixedUpdate();
            }
        }

        public void Update() {
            if (CurrentScene != null && m_IsSceneBegin == false) {
                CurrentScene.Begin();
                m_IsSceneBegin = true;
            }

            if (CurrentScene != null && m_IsSceneBegin) {
                CurrentScene.Update();
            }
        }

        public void ExitApplication() {
            m_IsSceneBegin = false;
            if (CurrentScene != null) {
                CurrentScene.End();
            }

            Application.Quit();
        }
    }
}