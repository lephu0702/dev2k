using System;

namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIMask : Singleton<UIMask> {
        public GameMgr GameMgr { set; private get; }
        private UIMgr m_UIMgr;
        private GameObject m_CanvasGo;
        private GameObject m_TopPanelGo;
        private GameObject m_MaskPanelGo;
        private Camera m_UICamera;
        private float m_OriginalUICameralDepth;

        #region Singleton

        protected UIMask() {
        }

        #endregion

        public override void OnSingletonInit() {
            m_UIMgr = GameMgr.S.UIMgr;
            m_CanvasGo = m_UIMgr.CanvasGo;
            m_TopPanelGo = m_CanvasGo;
            m_MaskPanelGo = GameObjectHelper.FindGameObject(m_CanvasGo, "UIMaskPanel", false, false);
            m_UICamera = m_UIMgr.UICamera;
            if (m_UICamera != null) {
                m_OriginalUICameralDepth = m_UICamera.depth;
            }
        }

        public void SetMaskWindow(GameObject ui, UILucenyType lucenyType = UILucenyType.Lucency) {
            m_TopPanelGo.transform.SetAsLastSibling();
            switch (lucenyType) {
                case UILucenyType.Lucency:
                    m_MaskPanelGo.SetActive(true);
                    Color newColor1 = new Color(255 / 255F, 255 / 255F, 255 / 255F, 0F / 255F);
                    m_MaskPanelGo.GetComponent<Image>().color = newColor1;
                    break;
                case UILucenyType.Translucence:
                    m_MaskPanelGo.SetActive(true);
                    Color newColor2 = new Color(220 / 255F, 220 / 255F, 220 / 255F, 50 / 255F);
                    m_MaskPanelGo.GetComponent<Image>().color = newColor2;
                    break;
                case UILucenyType.ImPenetrable:
                    m_MaskPanelGo.SetActive(true);
                    Color newColor3 = new Color(50 / 255F, 50 / 255F, 50 / 255F, 200F / 255F);
                    m_MaskPanelGo.GetComponent<Image>().color = newColor3;
                    break;
                case UILucenyType.Pentrate:
                    if (m_MaskPanelGo.activeInHierarchy) {
                        m_MaskPanelGo.SetActive(false);
                    }

                    break;
                default:
                    break;
            }

            m_MaskPanelGo.transform.SetAsLastSibling();
            ui.transform.SetAsLastSibling();
            if (m_UICamera != null) {
                m_UICamera.depth = m_UICamera.depth + 100;
            }
        }

        public void CancelMaskWindow() {
            m_TopPanelGo.transform.SetAsFirstSibling();
            if (m_MaskPanelGo.activeInHierarchy) {
                m_MaskPanelGo.SetActive(false);
            }

            if (m_UICamera != null) {
                m_UICamera.depth = m_OriginalUICameralDepth;
            }
        }
    }
}