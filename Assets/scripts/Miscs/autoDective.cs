using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDective : MonoBehaviour
{
    [SerializeField] bool ifDie;
    [SerializeField] WaitForSeconds duration;
    [SerializeField] float dura;
    void Awake()
    {
        dura = 2f;//�ȴ�����
        ifDie = false;//��ʼ��ʱӦ����False
        duration = new WaitForSeconds(dura);
    }
    void OnEnable()
    {
        StartCoroutine(DurationCoro());
    }
    IEnumerator DurationCoro()
    {
        yield return duration;
        if (ifDie)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}
