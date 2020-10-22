namespace dev2k {
    using UnityEngine;

    public static class GameObjectExtension {
        public static bool TryGetComponent<T>(this GameObject selfObj) where T : Component {
            return selfObj.GetComponent<T>() != null;
        }

        public static GameObject Show(this GameObject selfObj) {
            selfObj.SetActive(true);
            return selfObj;
        }

        public static T Show<T>(T selfComponent) where T : Component {
            selfComponent.gameObject.SetActive(true);
            return selfComponent;
        }
    }
}