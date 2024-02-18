using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_manager : MonoBehaviour
{
    [SerializeField] Pool[] playerBullet;
    [SerializeField] Pool[] EnemyBullet;
    [SerializeField] Pool[] enemy;
    [SerializeField] Pool[] HitVFX;
    [SerializeField] Pool[] Boss;
    [SerializeField] static Dictionary<GameObject, Pool> PoolDic;
    void Awake()
    {
        PoolDic = new Dictionary<GameObject, Pool>();
        Initialize(playerBullet);
        Initialize(EnemyBullet);
        Initialize(enemy);
        Initialize(HitVFX);
        Initialize(Boss);
    }
    void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
#if UNITY_EDITOR
            if (PoolDic.ContainsKey(pool.Prefab))
            {
                Debug.Log("Pool Manager Warning: The pool system is tring to add a ObjectPool that has already been added!:The wrong Object: " + pool.Prefab.name);
                continue;
            }
#endif
            Transform poolch = new GameObject("Pool:" + pool.Prefab.name).transform;
            poolch.parent = transform;
            pool.Initialize(poolch);
            PoolDic.Add(pool.Prefab, pool);
        }
    }
    public static GameObject Release(GameObject prefab)//启用对象
    {
#if UNITY_EDITOR
        if(!PoolDic.ContainsKey(prefab))
        {
            Debug.Log("Pool Manager Warning: The pool system is tring to release a object that dosen't exist!: The Wrong object: " + prefab.name);
            return null;
        }
#endif
        GameObject tmp = PoolDic[prefab].EnableObject();
        return tmp;
    }
    public static GameObject Release(GameObject prefab,Vector3 pos)//重载
    {
#if UNITY_EDITOR
        if(!PoolDic.ContainsKey(prefab))
        {
            Debug.Log("Pool Manager Warning: The pool system is tring to release a object that dosen't exist!: The Wrong object: " + prefab.name);
            return null;
        }
#endif
        GameObject tmp = PoolDic[prefab].EnableObject(pos);
        return tmp;
    }
    public static GameObject Release(GameObject prefab,Vector3 pos,Quaternion rot)//重载
    {
#if UNITY_EDITOR
        if(!PoolDic.ContainsKey(prefab))
        {
            Debug.Log("Pool Manager Warning: The pool system is tring to release a object that dosen't exist!: The Wrong object: " + prefab.name);
            return null;
        }
#endif
        GameObject tmp = PoolDic[prefab].EnableObject(pos,rot);
        return tmp;
    }
    public static GameObject Release(GameObject prefab,Vector3 pos,Quaternion rot,Vector3 scal)//重载
    {
#if UNITY_EDITOR
        if(!PoolDic.ContainsKey(prefab))
        {
            Debug.Log("Pool Manager Warning: The pool system is tring to release a object that dosen't exist!: The Wrong object: " + prefab.name);
            return null;
        }
#endif
        GameObject tmp = PoolDic[prefab].EnableObject(pos,rot,scal);
        return tmp;
    }
#if UNITY_EDITOR
void OnDestroy()//测试代码
{
    CheckPoolSize(playerBullet);
    CheckPoolSize(EnemyBullet);
    CheckPoolSize(enemy);
    CheckPoolSize(HitVFX);
    CheckPoolSize(Boss);
}
#endif
    void CheckPoolSize(Pool[] pools)//测试代码
    {
        foreach(var pool in pools)
        {
            if(pool.Size < pool.RuntimeSize)
            {
                Debug.LogWarning(string.Format("The pool:{0},its RuntimeSize:{1} is bigger than initial size:{2}", pool.Prefab.name, pool.RuntimeSize, pool.Size));
            }
        }
    }

}
