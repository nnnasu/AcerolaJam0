using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IPoolable {

    public IObjectPool<GameObject> Pool { get; set; }
    public void ReturnToPool();

}
