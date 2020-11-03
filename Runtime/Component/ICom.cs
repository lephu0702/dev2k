namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface ICom {
        AbstractActor Actor { get; }

        int ComOrder { get; }

        void AwakeCom(AbstractActor actor);
        void OnComStart();
        void OnComEnable();
        void OnComUpdate(float dt);
        void OnComLateUpdate(float dt);
        void OnComDisable();
        void DestroyCom();
    }
}