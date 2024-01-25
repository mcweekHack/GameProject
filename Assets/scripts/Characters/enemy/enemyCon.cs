using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCon : Character
{
    [SerializeField] AudioData hurtAudio;
    [SerializeField] AudioData DaethAudio;
    [SerializeField] int Score;
    [SerializeField] GameObject self;
    public override void Die()
    {
        energy_system.instance.GetEnergy(energy_system.instance.KillBouns);
        EnemyMana.instance.EnemyLeft--;
        AudioMana.instance.playSFXRandomly(DaethAudio);
        Score_system.instance.AddScore(Score);
        EnemyMana.RemoveList(self);
        base.Die();
    }
    public override void TakeDamage(float damage)
    {
        AudioMana.instance.playSFXRandomly(hurtAudio);
        base.TakeDamage(damage);
    }
}
