namespace dev2k {
    using UnityEngine;

    public enum UIType {
        Normal,
        Fixed,
        Popup
    }

    public enum UIShowMode {
        Normal,
        ReverseChange,
        HideOther
    }

    public enum UILucenyType {
        Lucency,
        Translucence,
        ImPenetrable,
        Pentrate
    }

    public abstract class IView : MonoBehaviour {
        private UIType m_FormType = UIType.Normal;
        private UIShowMode m_FormShowMode = UIShowMode.Normal;
        private UILucenyType m_FormLucenyType = UILucenyType.Lucency;

        public UIMgr UIMgr { get; set; }
        public UIMask UIMask { get; set; }

        public UIType Type {
            get { return m_FormType; }
            set { m_FormType = value; }
        }

        public UIShowMode ShowMode {
            get { return m_FormShowMode; }
            set { m_FormShowMode = value; }
        }

        public UILucenyType LucenyType {
            get { return m_FormLucenyType; }
            set { m_FormLucenyType = value; }
        }


        public virtual void UpdateUI() {
        }

        public virtual void Display() {
        }

        public virtual void Hiding() {
        }

        public virtual void Redisplay() {
        }

        public virtual void Freeze() {
        }
    }
}