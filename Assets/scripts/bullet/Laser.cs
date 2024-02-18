using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    protected Coroutine Coro;
    protected float BulletDamage;
    [SerializeField] GameObject HitFVX;
    [SerializeField] float HurtBet;
    WaitForSeconds HurtBetTime;
    protected virtual void Start()
    {
        HurtBetTime = new WaitForSeconds(HurtBet);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Character>(out Character character))
        {
            if (Coro != null)
                StopCoroutine(Coro);
            Coro = StartCoroutine(HurtCoroutine(character));
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(Coro != null)
            StopCoroutine(Coro);
    }
    protected virtual IEnumerator HurtCoroutine(Character character)
    {
        while (character.gameObject.activeSelf)
        {
            character.TakeDamage(BulletDamage);
            Pool_manager.Release(HitFVX, character.transform.position);
            yield return HurtBetTime;
        }
    }
    
}
