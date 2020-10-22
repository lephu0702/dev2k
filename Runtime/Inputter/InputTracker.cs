namespace dev2k {
    using UnityEngine;

    public class InputTracker : Singleton<InputTracker> {
        #region Singleton
        private InputTracker() {}
        #endregion
        
        #region Mouse Detection

        public bool HasClicked(MouseButton button) {
            return Input.GetMouseButton((int) button);
        }

        public bool HasClicking(MouseButton button) {
            return Input.GetMouseButton((int) button);
        }

        public bool HasReleasedClick(MouseButton button) {
            return Input.GetMouseButton((int) button);
        }

        public Vector3 GetMousePositionInWorldSpace() {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        #endregion

        #region Key Detection

        public bool HasPressedKey(KeyCode key) {
            return Input.GetKeyDown(key);
        }

        public bool IsPressingKey(KeyCode key) {
            return Input.GetKey(key);
        }

        public bool HasReleasedKey(KeyCode key) {
            return Input.GetKeyUp(key);
        }

        #endregion

        #region Object Detection

        public GameObject GetObjectUnderMouse(string layerName = "") {
            Vector3 wp = GetMousePositionInWorldSpace();
            Vector2 position = new Vector2(wp.x, wp.y);
            Collider2D col = null;

            col = (layerName == "")
                ? Physics2D.OverlapPoint(position)
                : Physics2D.OverlapPoint(position, 1 << LayerMask.NameToLayer(layerName));

            if (col != null) {
                return col.gameObject;
            }

            return null;
        }

        #endregion
    }
}