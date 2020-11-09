namespace dev2k {
    using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UIMgr : IGameMgr {
        public GameObject CanvasGo { get; private set; }
        public Camera UICamera { get; private set; }
        public UIMask UIMask { get; private set; }

        private Dictionary<string, string> m_UIPathDict;
        private Dictionary<string, ViewBase> m_UIDict;
        private Dictionary<string, ViewBase> m_UIShowDict;
        private Stack<ViewBase> m_UIStack;

        private Transform m_CanvasTransform;
        private Transform m_NormalTransform;
        private Transform m_FixedTransform;
        private Transform m_PopupTransform;

        public UIMgr(GameMgr gameMgr) : base(gameMgr) {
            m_UIPathDict = new Dictionary<string, string>();
            m_UIDict = new Dictionary<string, ViewBase>();
            m_UIShowDict = new Dictionary<string, ViewBase>();
            m_UIStack = new Stack<ViewBase>();
        }

        public override void Initialize() {
            UIMask = UIMask.S;
            InitUIPath();
            InitUICanvas();
        }

        public override void Release() {
            m_UIDict.Clear();
            m_UIShowDict.Clear();
            m_UIStack.Clear();
            Object.Destroy(CanvasGo);
        }

        private void InitUIPath() {
            if (m_UIPathDict != null) {
                DirectoryInfo dir = new DirectoryInfo(Application.dataPath + UICommon.SYS_PATH_UI);
                FileInfo[] files = dir.GetFiles("*.prefab");
                foreach (FileInfo fi in files) {
                    string str = fi.Name.Remove(fi.Name.LastIndexOf('.'));
                    m_UIPathDict.Add(str, @"UI\" + str);
                    Log.i("UI: " + str);
                }
            }
        }

        private void InitUICanvas() {
            CanvasGo = GameMgr.ResMgr.LoadPrefab(UICommon.SYS_PATH_UI_CANVAS);
            m_CanvasTransform = CanvasGo.transform;
            m_NormalTransform = GameObjectHelper.FindGameObject(CanvasGo, UICommon.SYS_NORMAL_NODE, false, false)
                .transform;
            m_FixedTransform = GameObjectHelper.FindGameObject(CanvasGo, UICommon.SYS_FIXED_NODE, false, false)
                .transform;
            m_PopupTransform = GameObjectHelper.FindGameObject(CanvasGo, UICommon.SYS_POPUP_NODE, false, false)
                .transform;
            UICamera = GameObjectHelper.FindGameObject(CanvasGo, "UICamera", false, false).GetComponent<Camera>();
        }

        public int UICount() {
            if (m_UIDict != null) {
                return m_UIDict.Count;
            } else {
                return 0;
            }
        }

        public int ShowUICount() {
            if (m_UIShowDict != null) {
                return m_UIShowDict.Count;
            } else {
                return 0;
            }
        }

        public int StackUICount() {
            if (m_UIStack != null) {
                return m_UIStack.Count;
            } else {
                return 0;
            }
        }

        public void Show(string name) {
            if (string.IsNullOrEmpty(name)) return;

            ViewBase baseUI = LoadUIFromCache(name);
            if (baseUI == null) return;

            switch (baseUI.ShowMode) {
                case UIShowMode.Normal:
                    LoadUIToCurrentCache(name);
                    break;
                case UIShowMode.ReverseChange:
                    PushUIToStack(name);
                    break;
                case UIShowMode.HideOther:
                    EnterUIAndHideOther(name);
                    break;
                default:
                    break;
            }
        }

        public void CloseUI(string name) {
            if (string.IsNullOrEmpty(name)) return;
            m_UIDict.TryGetValue(name, out var baseUI);
            if (baseUI == null) return;

            switch (baseUI.ShowMode) {
                case UIShowMode.Normal:
                    ExitUI(name);
                    break;
                case UIShowMode.ReverseChange:
                    PopUI();
                    break;
                case UIShowMode.HideOther:
                    ExitUIAndDisplayOther(name);
                    break;
                default:
                    break;
            }
        }

        private ViewBase LoadUIFromCache(string name) {
            m_UIDict.TryGetValue(name, out var uiResult);
            if (uiResult == null) {
                uiResult = LoadUI(name);
                uiResult.UIMgr = this;
                uiResult.UIMask = UIMask;
            }

            return uiResult;
        }

        private ViewBase LoadUI(string name) {
            GameObject clonePrefab = null;

            m_UIPathDict.TryGetValue(name, out var path);
            if (!string.IsNullOrEmpty(path)) {
                clonePrefab = GameMgr.ResMgr.LoadPrefab(path);
            }

            if (m_CanvasTransform != null && clonePrefab != null) {
                var baseUI = clonePrefab.GetComponent<ViewBase>();
                if (baseUI == null) {
                    Log.e("ViewBase==null! UIName: " + name);
                    return null;
                }

                switch (baseUI.Type) {
                    case UIType.Normal:
                        clonePrefab.transform.SetParent(m_NormalTransform, false);
                        break;
                    case UIType.Fixed:
                        clonePrefab.transform.SetParent(m_FixedTransform, false);
                        break;
                    case UIType.Popup:
                        clonePrefab.transform.SetParent(m_PopupTransform, false);
                        break;
                    default:
                        break;
                }

                clonePrefab.SetActive(false);
                m_UIDict.Add(name, baseUI);
                return baseUI;
            } else {
                Log.e("CanvasTransform==null Or clonePrefab==null!!, Please Check!, UIName= " + name);
            }

            Log.e("Error!!!, UIName= " + name);
            return null;
        }

        private void LoadUIToCurrentCache(string name) {
            m_UIShowDict.TryGetValue(name, out var baseUI);
            if (baseUI != null) return;

            m_UIDict.TryGetValue(name, out var baseUIFromCache);
            if (baseUIFromCache != null) {
                m_UIShowDict.Add(name, baseUIFromCache);
                baseUIFromCache.Display();
            }
        }

        private void PushUIToStack(string name) {
            if (m_UIStack.Count > 0) {
                ViewBase topUI = m_UIStack.Peek();
                topUI.Freeze();
            }

            m_UIDict.TryGetValue(name, out var baseUI);
            if (baseUI != null) {
                baseUI.Display();
                m_UIStack.Push(baseUI);
            } else {
                Log.e("BaseUI==null, Please Check, UIName=" + name);
            }
        }

        private void ExitUI(string name) {
            m_UIShowDict.TryGetValue(name, out var baseUI);
            if (baseUI == null) return;
            baseUI.Hiding();
            m_UIShowDict.Remove(name);
        }

        private void PopUI() {
            if (m_UIStack.Count >= 2) {
                ViewBase topUI = m_UIStack.Pop();
                topUI.Hiding();
                ViewBase nextUI = m_UIStack.Peek();
                nextUI.Redisplay();
            } else if (m_UIStack.Count == 1) {
                ViewBase topUI = m_UIStack.Pop();
                topUI.Hiding();
            }
        }

        private void EnterUIAndHideOther(string name) {
            if (string.IsNullOrEmpty(name)) return;

            m_UIShowDict.TryGetValue(name, out var baseUIShow);
            if (baseUIShow != null) return;

            foreach (ViewBase baseUI in m_UIShowDict.Values) {
                baseUI.Hiding();
            }

            foreach (ViewBase stackUI in m_UIStack) {
                stackUI.Hiding();
            }

            m_UIDict.TryGetValue(name, out var baseUIFromALL);
            if (baseUIFromALL != null) {
                m_UIShowDict.Add(name, baseUIFromALL);
                baseUIFromALL.Display();
            }
        }

        private void ExitUIAndDisplayOther(string name) {
            if (string.IsNullOrEmpty(name)) return;

            m_UIShowDict.TryGetValue(name, out var baseUIShow);
            if (baseUIShow == null) return;

            baseUIShow.Hiding();
            m_UIShowDict.Remove(name);

            foreach (ViewBase baseUI in m_UIShowDict.Values) {
                baseUI.Redisplay();
            }

            foreach (ViewBase staUI in m_UIStack) {
                staUI.Redisplay();
            }
        }
    }
}