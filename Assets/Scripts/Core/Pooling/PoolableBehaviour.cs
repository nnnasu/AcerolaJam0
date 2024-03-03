using UnityEngine;
using UnityEngine.Pool;

public class PoolableBehaviour : MonoBehaviour, IPoolable {
    private IObjectPool<GameObject> pool;
    public IObjectPool<GameObject> Pool { get => pool; set => pool = value; }    

    public virtual void ReturnToPool() {
        pool?.Release(gameObject);
    }

}