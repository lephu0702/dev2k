namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ViewBase : IView {
        public override void Display() {
            gameObject.SetActive(true);
            if (Type == UIType.Popup) {
                if (UIMask != null) {
                    UIMask.SetMaskWindow(gameObject, LucenyType);
                }
            }
        }

        public override void Hiding() {
            gameObject.SetActive(false);
            if (Type == UIType.Popup) {
                if (UIMask != null) {
                    UIMask.CancelMaskWindow();
                }
            }
        }

        public override void Redisplay() {
            gameObject.SetActive(true);
            if (UIMask != null) {
                UIMask.SetMaskWindow(gameObject, LucenyType);
            }
        }

        public override void Freeze() {
            gameObject.SetActive(true);
        }

        public void OpenUI(string name) {
            if (UIMgr != null) {
                UIMgr.Show(name);
            } else {
                GameMgr.S.UIMgr.Show(name);
            }
        }

        public void CloseUI() {
            int intPosition = -1;
            string uiName = GetType().ToString();
            intPosition = uiName.IndexOf('.');
            if (intPosition != -1) {
                uiName = uiName.Substring(intPosition + 1);
            }

            if (UIMgr != null) {
                UIMgr.CloseUI(uiName);
            } else {
                GameMgr.S.UIMgr.CloseUI(uiName);
            }
        }
    }
}