namespace dev2k {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IPoolFactory<T> {
        T Create();
    }
}