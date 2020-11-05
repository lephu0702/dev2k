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

    public class SceneCtrl : Singleton<SceneCtrl> {
        public IScene CurrentScene { get; private set; }

        private bool m_IsSceneBegin;

        #region Singleton

        private SceneCtrl() {
        }

        #endregion

        public override void OnSingletonInit() {
            m_IsSceneBegin = false;
        }

        public void SetScene(BuildScene buildScene, bool isNow = true, bool isAsync = false) {
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

            var sceneType = Enum.GetName(typeof(BuildScene), buildScene) + "Scene";
            Log.i(sceneType);
            var scene = (IScene) Activator.CreateInstance(Type.GetType(sceneType), this);

            Log.i("SetScene: " + scene?.ToString());
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