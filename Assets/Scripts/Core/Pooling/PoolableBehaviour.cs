using UnityEngine;
using UnityEngine.Pool;

public class PoolableBehaviour : MonoBehaviour, IPoolable {
    private IObjectPool<GameObject> pool;
    public IObjectPool<GameObject> Pool { get => pool; set => pool = value; }
    IObjectPool<GameObject> IPoolable.Pool { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public virtual void ReturnToPool() {
        pool?.Release(gameObject);
    }

}