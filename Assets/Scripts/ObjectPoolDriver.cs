using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolDriver : MonoBehaviour
{
    public bool useObjectPool = true;
    public float coolDown = 0.25f;
    public float lifeTime = 0.15f;
    public float minLifeTime = 0.15f;

    private float curCoolDown = 0f;
    private float curLifeTime = 0f;
    private bool lastUseObjectPool = false;

    // Start is called before the first frame update
    void Start() {
        lastUseObjectPool = !useObjectPool;
    }

    // Update is called once per frame
    void Update() {
        if (useObjectPool != lastUseObjectPool) {
            lastUseObjectPool = useObjectPool;

            if (useObjectPool)
                ObjectPool.instance.InitPools();
            else
                ObjectPool.instance.RemovePools();
        }

        curCoolDown -= Time.deltaTime;
        if (curCoolDown <= 0f) {
            curCoolDown = coolDown;

            if (useObjectPool)
                ObjectPool.instance.SpawnFromPool("Duck", Vector3.up * 2f, Quaternion.identity);
            else
                Instantiate(ObjectPool.instance.pools[0].prefab, ObjectPool.instance.transform);
        }

        curLifeTime -= Time.deltaTime;
        if (curLifeTime <= 0f) {
            curLifeTime = Random.Range(minLifeTime, lifeTime); ;

            if (useObjectPool)
                for (int i = ObjectPool.instance.transform.childCount - 1; i >= 0; --i) {
                    if (ObjectPool.instance.transform.GetChild(i).gameObject.activeSelf) {
                        ObjectPool.instance.transform.GetChild(i).gameObject.SetActive(false);
                        break;
                    }
                }
            else
                if (ObjectPool.instance.transform.childCount > 0)
                    Destroy(ObjectPool.instance.transform.GetChild(ObjectPool.instance.transform.childCount - 1).gameObject);
        }
    }
}
