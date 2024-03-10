using UnityEngine;
using UnityEngine.Pool;

public class PoolableBehaviour : MonoBehaviour, IPoolable {
    private IObjectPool<GameObject> pool;
    public IObjectPool<GameObject> Pool { get => pool; set => pool = value; }
    public bool HasBeenReturned { get; protected set; } = false;
    public void MarkAsUnreturned() {
        HasBeenReturned = false;
    }

    public void ReturnToPool() {
        if (!HasBeenReturned && gameObject) {
            pool?.Release(gameObject);
            HasBeenReturned = true;
        }

    }

}