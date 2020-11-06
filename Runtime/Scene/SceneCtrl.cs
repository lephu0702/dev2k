namespace dev2k {
    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public enum BuildScene {
        Splash,
        Menu,
        Main,
        Game
    }

    public class SceneCtrl {
        public Scene CurrentScene { get; private set; }

        private bool m_IsSceneBegin;

        public SceneCtrl() {
            m_IsSceneBegin = false;
        }

        // public void SetScene<T>(bool isNow = true, bool isAsync = false) where T : IScene, new() {
        //     IScene scene = new T();
        //     scene.Controller = this;
        //     m_IsSceneBegin = false;
        //
        //     if (isNow) {
        //         if (isAsync) {
        //             LoadingScene.NextScene = scene?.Name;
        //             LoadScene("Loading");
        //         } else {
        //             LoadScene(scene?.Name);
        //         }
        //     }
        //
        //     CurrentScene = scene;
        // }

        public void SetScene(Scene scene, bool isNow = true, bool isAsync = false) {
            scene.Controller = this;
            m_IsSceneBegin = false;

            if (isNow) {
                if (isAsync) {
                    LoadingScene.NextScene = scene?.Name;
                    LoadScene("Loading");
                } else {
                    LoadScene(scene?.Name);
                }
            }

            CurrentScene = scene;
        }

        // public void SetScene(BuildScene buildScene, bool isNow = true, bool isAsync = false) {
        // IScene scene = null;
        // switch (buildScene) {
        //     case BuildScene.Splash:
        //         scene = new SplashScene(this);
        //         break;
        //     case BuildScene.Menu:
        //         break;
        //     case BuildScene.Main:
        //         break;
        //     case BuildScene.Game:
        //         break;
        //     default:
        //         return;
        // }

        // IScene scene = new Scene(this);
        // Log.i("SetScene: " + scene?.ToString());
        // m_IsSceneBegin = false;
        //
        // if (isNow) {
        //     if (isAsync) {
        //         LoadingScene.NextScene = scene?.Name;
        //         LoadScene("Loading");
        //     } else {
        //         LoadScene(scene?.Name);
        //     }
        // }
        //
        // CurrentScene = scene;
        // }

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