using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class StructureBase : MonoBehaviour, IPoolable {

    private IObjectPool<GameObject> pool;
    public IObjectPool<GameObject> Pool { get => pool; set => pool = value; }

    public void ReturnToPool() {
        pool?.Release(gameObject);
    }
}
