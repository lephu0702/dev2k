namespace dev2k {
    public class FSMState<T> {
        public virtual string StateName {
            get { return this.GetType().Name; }
        }

        public virtual void Enter(T actor) {
        }

        public virtual void Execute(T actor, float dt) {
        }

        public virtual void Exit(T actor) {
        }

        public virtual void OnMsg(T actor, int key, params object[] args) {
        }
    }
}