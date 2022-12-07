using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool {
    public string tag;
    public GameObject prefab;
    public int size;
}

public class ObjectPool : MonoBehaviour {
    public static ObjectPool instance;

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start() {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot) {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void InitPools() {
        Queue<GameObject> objectPool;

        foreach (Pool curPool in pools) {
            objectPool = new Queue<GameObject>();

            for (int i = 0; i < curPool.size; i++) {
                GameObject obj = Instantiate(curPool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(curPool.tag, objectPool);
        }
    }

    public void RemovePools() {
        poolDictionary.Clear();

        for (int i = transform.childCount - 1; i >= 0; --i) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
