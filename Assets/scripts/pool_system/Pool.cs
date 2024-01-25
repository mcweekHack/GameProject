using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Pool
{
    public GameObject Prefab => prefab;
    [SerializeField]GameObject prefab;
    Queue<GameObject> queue;
    [SerializeField] int size;//初始预制体个数
    public int Size => size;
    public int RuntimeSize => queue.Count;
    Transform parent;
    public void Initialize(Transform parent)//初始化队列内的初始预制体
    {
        this.parent = parent;
        queue = new Queue<GameObject>();
        for(var i = 1; i < size; i++)
        {
            queue.Enqueue(Copy());
        }
    }
    GameObject Copy()//生成一个预制体的对象
    {
        var copy = GameObject.Instantiate(prefab,parent);
        copy.SetActive(false);
        return copy;
    }
    GameObject AvailObject()//返回一个可用对象,并将该对象提前入列
    {
        GameObject avail = null;
        if (queue.Count > 0&& !queue.Peek().activeSelf)
            avail = queue.Dequeue();
        else
            avail = Copy();
        queue.Enqueue(avail);
        return avail;
    }
    public GameObject EnableObject()//启用游戏对象
    {
        GameObject tmp = AvailObject();
        tmp.SetActive(true);
        return tmp;
    }
    public GameObject EnableObject(Vector3 pos)//重载
    {
        GameObject tmp = AvailObject();
        tmp.SetActive(true);
        tmp.transform.position = pos;
        return tmp;
    }
    public GameObject EnableObject(Vector3 pos,Quaternion rot)//重载
    {
        GameObject tmp = AvailObject();
        tmp.SetActive(true);
        tmp.transform.position = pos;
        tmp.transform.rotation = rot;
        return tmp;
    }
    public GameObject EnableObject(Vector3 pos, Quaternion rot,Vector3 scal)//重载
    {
        GameObject tmp = AvailObject();
        tmp.SetActive(true);
        tmp.transform.position = pos;
        tmp.transform.rotation = rot;
        tmp.transform.localScale = scal;
        return tmp;
    }
}
