namespace dev2k {
    public abstract class IGameMgr {
        private GameMain m_GameMain;

        public GameMain GameMgr => m_GameMain;

        protected IGameMgr(GameMain gameMain) {
            m_GameMain = gameMain;
        }

        public virtual void Initialize() {
        }

        public virtual void Release() {
        }

        public virtual void Update() {
        }

        public virtual void FixedUpdate() {
        }
    }
}