using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class GlobalPool : MonoBehaviour {

    private static GlobalPool instance;
    public static GlobalPool Current => instance;

    private Dictionary<GameObject, ObjectPool<GameObject>> pool = new();

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public GameObject GetObject(GameObject prefab) {
        if (!pool.ContainsKey(prefab)) {
            ObjectPool<GameObject> newPool = new(
                createFunc: () => {
                    GameObject obj = Instantiate(prefab);
                    var poolable = obj.GetComponent<IPoolable>();
                    poolable.Pool = pool[prefab];
                    obj.gameObject.SetActive(true);
                    return obj;
                },
                actionOnRelease: OnReturnedToPool
            );
            pool.Add(prefab, newPool);
        }

        var item = pool[prefab].Get();
        return item;
    }

    private void OnReturnedToPool(GameObject gameObject) {
        gameObject.SetActive(false);
    }




}
