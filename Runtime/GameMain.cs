namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameMain : Singleton<GameMain> {
        public UIMgr UIMgr;
        public ResMgr ResMgr;
        public EventMgr EventMgr;
        public InputMgr InputMgr;
        public PoolMgr PoolMgr;
        public WorldMgr WorldMgr;

        #region Singleton

        protected GameMain() {
        }

        #endregion

        public override void OnSingletonInit() {
            ResMgr = new ResMgr(this);
            UIMgr = new UIMgr(this);
            EventMgr = new EventMgr(this);
            InputMgr = new InputMgr(this);
            PoolMgr = new PoolMgr(this);
            WorldMgr = new WorldMgr(this);
        }

        public override void Dispose() {
            base.Dispose();
            ResMgr.Release();
            UIMgr.Release();
            EventMgr.Release();
            InputMgr.Release();
            PoolMgr.Release();
            WorldMgr.Release();
        }

        public virtual void Initialize() {
            UIMgr.Initialize();
            ResMgr.Initialize();
            EventMgr.Initialize();
            InputMgr.Initialize();
            PoolMgr.Initialize();
            WorldMgr.Initialize();
        }

        public virtual void Update() {
            UIMgr.Update();
            ResMgr.Update();
            EventMgr.Update();
            InputMgr.Update();
            PoolMgr.Update();
            WorldMgr.Update();
        }

        public virtual void FixedUpdate() {
            UIMgr.FixedUpdate();
            ResMgr.FixedUpdate();
            EventMgr.FixedUpdate();
            InputMgr.FixedUpdate();
            PoolMgr.FixedUpdate();
            WorldMgr.FixedUpdate();
        }
    }
}