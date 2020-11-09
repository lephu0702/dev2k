using System;

namespace dev2k {
    using System.Collections.Generic;
    using UnityEngine;

    public delegate void InputDelegate();

    public struct MouseInputData {
        public float posX;
        public float posY;
    }

    public class InputEvent {
        public InputPhase phase;
    }

    public class KeyEvent : InputEvent {
        public KeyCode key;
        public event InputDelegate callback;

        public void InvokeCallback() {
            if (callback != null) {
                callback();
            }
        }
    }

    public class MouseEvent : InputEvent {
        public MouseButton button;
        public PointerInputPlace inputPlace;
        public MouseInputData inputData;
        public event InputDelegate callback;

        public void InvokeCallback() {
            if (callback != null) {
                callback();
            }
        }
    }

    public class MouseOverObjectEvent : MouseEvent {
        public GameObject goInput;
        public string layerName;
    }

    public class InputMgr : IGameMgr {
        private List<KeyEvent> m_KeyEventList;
        private List<MouseEvent> m_MouseEventList;
        private List<MouseOverObjectEvent> m_MouseOverObjectEventList;

        public InputMgr(GameMgr gameMgr) : base(gameMgr) {
            m_KeyEventList = new List<KeyEvent>();
            m_MouseEventList = new List<MouseEvent>();
            m_MouseOverObjectEventList = new List<MouseOverObjectEvent>();
        }

        #region Key Event

        public void RegisterKey(KeyCode key, InputPhase phase, InputDelegate callback) {
            if (m_KeyEventList == null) {
                m_KeyEventList = new List<KeyEvent>();
            }

            var existingEvent = GetKeyEvent(key, phase);
            if (existingEvent == null) {
                existingEvent = CreateKeyEvent(key, phase);
                m_KeyEventList.Add(existingEvent);
            }

            existingEvent.callback += callback;
        }

        private KeyEvent GetKeyEvent(KeyCode key, InputPhase phase) {
            if (m_KeyEventList != null) {
                foreach (var e in m_KeyEventList) {
                    if (e.key == key && e.phase == phase) {
                        return e;
                    }
                }
            }

            return null;
        }

        private KeyEvent CreateKeyEvent(KeyCode key, InputPhase phase) {
            KeyEvent newEvent = new KeyEvent {key = key, phase = phase};
            return newEvent;
        }

        #endregion

        #region Mouse Event

        public void RegisterMouseEvent(MouseButton button, InputPhase phase, PointerInputPlace inputPlace,
            InputDelegate callback) {
            if (m_MouseEventList == null) {
                m_MouseEventList = new List<MouseEvent>();
            }

            var existingEvent = GetMouseEvent(button, phase, inputPlace);
            if (existingEvent == null) {
                existingEvent = CreateMouseEvent(button, phase, inputPlace);
                m_MouseEventList.Add(existingEvent);
            }

            existingEvent.callback += callback;
        }

        private MouseEvent GetMouseEvent(MouseButton button, InputPhase phase, PointerInputPlace inputPlace) {
            if (m_MouseEventList != null) {
                foreach (var e in m_MouseEventList) {
                    if (e.button == button && e.phase == phase && e.inputPlace == inputPlace) {
                        return e;
                    }
                }
            }

            return null;
        }

        private MouseEvent CreateMouseEvent(MouseButton button, InputPhase phase, PointerInputPlace inputPlace) {
            MouseEvent mouseEvent = new MouseEvent {button = button, phase = phase, inputPlace = inputPlace};
            return mouseEvent;
        }

        #endregion

        #region Mouse Over Object Event

        public MouseOverObjectEvent RegisterMouseOverObjectEvent(MouseButton button, InputPhase phase, PointerInputPlace
            inputPlace, GameObject goInput, string layerName, InputDelegate callback) {
            if (m_MouseOverObjectEventList == null) {
                m_MouseOverObjectEventList = new List<MouseOverObjectEvent>();
            }

            var existingEvent = GetMouseOverObjectEvent(button, phase, inputPlace, goInput, layerName);
            if (existingEvent == null) {
                existingEvent = CreateMouseOverObjectEvent(button, phase, inputPlace, goInput, layerName);
                m_MouseEventList.Add(existingEvent);
            }

            existingEvent.callback += callback;
            return existingEvent;
        }

        private MouseOverObjectEvent GetMouseOverObjectEvent(MouseButton button, InputPhase phase, PointerInputPlace
            inputPlace, GameObject goInput, string layerName) {
            if (m_MouseOverObjectEventList != null) {
                foreach (var e in m_MouseOverObjectEventList) {
                    if (e.button == button && e.phase == phase && e.inputPlace == inputPlace && e.goInput == goInput
                        && e.layerName == layerName) {
                        return e;
                    }
                }
            }

            return null;
        }

        public MouseOverObjectEvent CreateMouseOverObjectEvent(MouseButton button, InputPhase phase, PointerInputPlace
            inputPlace, GameObject goInput, string layerName) {
            var mouseOverObjectEvent = new MouseOverObjectEvent {
                button = button,
                phase = phase,
                inputPlace = inputPlace,
                goInput = goInput,
                layerName = layerName
            };
            return mouseOverObjectEvent;
        }

        #endregion

        public override void Update() {
            CheckMouseInput();
            CheckKeyboardInput();
            CheckMouseOverObjectInput();
        }

        private void CheckKeyboardInput() {
            foreach (var e in m_KeyEventList) {
                bool shouldCallMethod = false;

                switch (e.phase) {
                    case InputPhase.onPressed:
                        shouldCallMethod = InputTracker.S.HasPressedKey(e.key);
                        break;
                    case InputPhase.onHold:
                        shouldCallMethod = InputTracker.S.IsPressingKey(e.key);
                        break;
                    case InputPhase.onReleased:
                        shouldCallMethod = InputTracker.S.HasReleasedKey(e.key);
                        break;
                }

                if (shouldCallMethod) {
                    e.InvokeCallback();
                }
            }
        }

        private void CheckMouseInput() {
            if (m_MouseEventList == null) return;

            bool shouldCheckClick = false;
            foreach (var e in m_MouseEventList) {
                switch (e.phase) {
                    case InputPhase.onPressed:
                        shouldCheckClick = InputTracker.S.HasClicked(e.button);
                        break;
                    case InputPhase.onHold:
                        shouldCheckClick = InputTracker.S.HasClicking(e.button);
                        break;
                    case InputPhase.onReleased:
                        shouldCheckClick = InputTracker.S.HasReleasedClick(e.button);
                        break;
                }

                if (shouldCheckClick) {
                    if (e.inputPlace == PointerInputPlace.onAnything) {
                        e.InvokeCallback();
                    } else {
                        var goInput = InputTracker.S.GetObjectUnderMouse();
                        if (e.inputPlace == PointerInputPlace.onEmptySpace && goInput == null) {
                            e.InvokeCallback();
                        }
                    }
                }
            }
        }

        private void CheckMouseOverObjectInput() {
            if (m_MouseOverObjectEventList == null) return;

            bool shouldCheckClick = false;

            foreach (var e in m_MouseOverObjectEventList) {
                switch (e.phase) {
                    case InputPhase.onPressed:
                        shouldCheckClick = InputTracker.S.HasClicked(e.button);
                        break;
                    case InputPhase.onHold:
                        shouldCheckClick = InputTracker.S.HasClicking(e.button);
                        break;
                    case InputPhase.onReleased:
                        shouldCheckClick = InputTracker.S.HasReleasedClick(e.button);
                        break;
                }

                if (shouldCheckClick) {
                    if (e.inputPlace == PointerInputPlace.onObject) {
                        GameObject goInput = InputTracker.S.GetObjectUnderMouse(e.layerName);

                        e.inputData = new MouseInputData {
                            posX = InputTracker.S.GetMousePositionInWorldSpace().x,
                            posY = InputTracker.S.GetMousePositionInWorldSpace().y
                        };

                        if (e.goInput == null && goInput != null) {
                            e.InvokeCallback();
                        } else if (e.goInput != null && e.goInput == goInput) {
                            e.InvokeCallback();
                        }
                    }
                }
            }
        }
    }
}