using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] GameObject DeathVfx;
    [SerializeField] protected float MaxHealth;
    [SerializeField] protected float health;
    [SerializeField] Stutas_bar HealthBar;
    bool IfShowHealth;
    Coroutine HealthGrowCoro;
    protected virtual void OnEnable()
    {
        health = MaxHealth;
        IfShowHealth = true;
        ShowHealth();
    }
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
        if (IfShowHealth)
            HealthBar.UpdateStatus(MaxHealth, health);
    }
    public virtual void Die()
    {
        health = 0f;
        Pool_manager.Release(DeathVfx, transform.position);
        IfShowHealth = false;
        CloseHealth();
        gameObject.SetActive(false);
    }
    public virtual void RestoreHealth(float Add)
    {
        health += Add;
        health = health > MaxHealth ? MaxHealth : health;
        HealthBar.UpdateStatus(MaxHealth, health);
    }
    protected IEnumerator HealthGrowingCorotuine(WaitForSeconds waitTime,float percent)
    {
        while (health < MaxHealth && health > 0)
        {
            yield return waitTime;
            RestoreHealth(MaxHealth*percent);
        }
    }
    protected IEnumerator HealthDowningCorotuine(WaitForSeconds waitTime, float percent)
    {
        while (health > 0)
        {
            yield return waitTime;
            TakeDamage(MaxHealth * percent);
        }
    }
    void ShowHealth()
    {
        HealthBar.gameObject.SetActive(true);
        HealthBar.Initialize(MaxHealth, health);
    }
    void CloseHealth()
    {
        HealthBar.gameObject.SetActive(false);
    }
}
