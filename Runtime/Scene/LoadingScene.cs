namespace dev2k {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class LoadingScene : MonoBehaviour {
        public static string NextScene;

        public Image fillImg;
        public Text percentageText;

        private void Start() {
            StartCoroutine(LoadingSceneAsync(NextScene));
        }

        private IEnumerator LoadingSceneAsync(string sceneName) {
            float displayProgress = 0;
            float toProgress = 0;
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = false;
            while (op.progress < 0.9f) {
                toProgress = (int) op.progress;
                while (displayProgress < toProgress) {
                    displayProgress += 0.01f;
                    percentageText.text = (int) (displayProgress * 100) + "%";
                    fillImg.fillAmount = displayProgress;
                    yield return new WaitForEndOfFrame();
                }
            }

            toProgress = 1;
            while (displayProgress < toProgress) {
                displayProgress += 0.01f;
                percentageText.text = (int) (displayProgress * 100) + "%";
                fillImg.fillAmount = displayProgress;
                yield return new WaitForEndOfFrame();
            }

            op.allowSceneActivation = true;
        }
    }
}