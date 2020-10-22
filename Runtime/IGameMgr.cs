namespace dev2k {
    public abstract class IGameMgr {
        private GameMgr m_GameMgr;

        public GameMgr GameMgr => m_GameMgr;

        protected IGameMgr(GameMgr gameMgr) {
            m_GameMgr = gameMgr;
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