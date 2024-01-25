using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] Vector2 move_time;
    [SerializeField] float awaitTime;
    [SerializeField] Vector2 bias;
    [SerializeField] Vector3 nowPos;
    [SerializeField] Vector3 nextpos;
    [SerializeField] float flyangle;
    [SerializeField] GameObject[] enemybullet;
    [SerializeField] Vector2 DelayTime;
    WaitForSeconds FireDelayTime;
    Quaternion moveQua;
    Coroutine MoveCoro;
    Coroutine FireCoro;
    [SerializeField] Transform muzzle;
    [SerializeField] AudioData shotaudio;
    void Awake()
    {
        DelayTime = new Vector2(1.5f, 2f);
        FireDelayTime = new WaitForSeconds(0.5f);
        flyangle = 30;
        move_time = new Vector2(6f, 7f);
        MoveCoro = null;
        awaitTime = 0.5f;
        bias = new Vector2(0.8f, 0.3f);
    }
    void OnEnable()
    {
        transform.position = viewport.instance.BackEnemySpwan(bias);
        nowPos = transform.position;
        MoveCoro = StartCoroutine(MoveCoroutine());
        FireCoro = StartCoroutine(FireCoroutine());
    }
    void OnDisable()
    {
        if (MoveCoro != null)
            StopCoroutine(MoveCoro);
        if (FireCoro != null)
            StopCoroutine(FireCoro);
    }
    IEnumerator MoveCoroutine()
    {
        nextpos = viewport.instance.BackEnemyTarget(bias);
        awaitTime = Random.Range(move_time.x,move_time.y);
        float p = nextpos.y > nowPos.y ? 1 : -1;
        moveQua = Quaternion.AngleAxis(flyangle * p, Vector3.right);
        float t = 0f;
        while (true)
        {
            if(t >= awaitTime)
            {
                t = 0f;
                nextpos = viewport.instance.BackEnemyTarget(bias);
                awaitTime = Random.Range(move_time.x, move_time.y);
                nowPos = transform.position;
                p = nextpos.y > nowPos.y ? 1 : -1;
                moveQua = Quaternion.AngleAxis(flyangle * p, Vector3.right);
            }
            else
            {
                transform.position = Vector3.Lerp(nowPos, nextpos,t / awaitTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, moveQua, t / awaitTime);
                t += Time.fixedDeltaTime;
            }
            yield return null;
        }
    }
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            for (var i = 0; i < enemybullet.Length;i++)
                Pool_manager.Release(enemybullet[i], muzzle.position, Quaternion.identity);
            FireDelayTime = new WaitForSeconds(Random.Range(DelayTime.x, DelayTime.y));
            AudioMana.instance.playSFXRandomly(shotaudio);
            yield return FireDelayTime;
        }
    }
}
