using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Pool
{
    public GameObject Prefab => prefab;
    [SerializeField]GameObject prefab;
    Queue<GameObject> queue;
    [SerializeField] int size;//��ʼԤ�������
    public int Size => size;
    public int RuntimeSize => queue.Count;
    Transform parent;
    public void Initialize(Transform parent)//��ʼ�������ڵĳ�ʼԤ����
    {
        this.parent = parent;
        queue = new Queue<GameObject>();
        for(var i = 1; i < size; i++)
        {
            queue.Enqueue(Copy());
        }
    }
    GameObject Copy()//����һ��Ԥ����Ķ���
    {
        var copy = GameObject.Instantiate(prefab,parent);
        copy.SetActive(false);
        return copy;
    }
    GameObject AvailObject()//����һ�����ö���,�����ö�����ǰ����
    {
        GameObject avail = null;
        if (queue.Count > 0&& !queue.Peek().activeSelf)
            avail = queue.Dequeue();
        else
            avail = Copy();
        queue.Enqueue(avail);
        return avail;
    }
    public GameObject EnableObject()//������Ϸ����
    {
        GameObject tmp = AvailObject();
        tmp.SetActive(true);
        return tmp;
    }
    public GameObject EnableObject(Vector3 pos)//����
    {
        GameObject tmp = AvailObject();
        tmp.SetActive(true);
        tmp.transform.position = pos;
        return tmp;
    }
    public GameObject EnableObject(Vector3 pos,Quaternion rot)//����
    {
        GameObject tmp = AvailObject();
        tmp.SetActive(true);
        tmp.transform.position = pos;
        tmp.transform.rotation = rot;
        return tmp;
    }
    public GameObject EnableObject(Vector3 pos, Quaternion rot,Vector3 scal)//����
    {
        GameObject tmp = AvailObject();
        tmp.SetActive(true);
        tmp.transform.position = pos;
        tmp.transform.rotation = rot;
        tmp.transform.localScale = scal;
        return tmp;
    }
}
