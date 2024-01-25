using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] protected float MoveSpeed;
    [SerializeField] protected Vector2 MoveDirect;
    protected Coroutine Coro;
    [SerializeField] protected float bulletDamage;
    [SerializeField] GameObject HitVFX;
    protected virtual void OnEnable()
    {
        Coro = StartCoroutine(BulletMove());
    }
    protected IEnumerator BulletMove()
    {
        while (gameObject.activeSelf)
        {
            transform.Translate(MoveDirect * MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Character>(out Character character))
        {
            character.TakeDamage(bulletDamage);
            var ContactPoint = collision.GetContact(0);
            Pool_manager.Release(HitVFX, ContactPoint.point, Quaternion.LookRotation(ContactPoint.normal));
            gameObject.SetActive(false);
        }
    }
}
