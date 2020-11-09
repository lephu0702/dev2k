namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class WorldMgr : IGameMgr {
        private WorldRoot m_WorldRoot;

        public WorldRoot WorldRoot {
            get { return m_WorldRoot; }
        }

        public WorldMgr(GameMgr gameMgr) : base(gameMgr) {
            if (m_WorldRoot == null) {
                WorldRoot root = GameObject.FindObjectOfType<WorldRoot>();
                if (root == null) {
                    Log.e("Failed to Find WorldRoot!");
                    return;
                }

                m_WorldRoot = root;
            }
        }
    }
}