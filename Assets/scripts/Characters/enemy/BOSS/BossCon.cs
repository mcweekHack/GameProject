using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCon : Character
{
    [SerializeField] AudioData hurtAudio;
    [SerializeField] AudioData DaethAudio;

    [SerializeField] int Score;
    [SerializeField] public float MoveDur;
    Vector2 tmp;


    [SerializeField] GameObject self;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform[] muzzle;
    [SerializeField] GameObject[] Bullet;
    public StateMachine stateMachine { get; set; }
    public EnemyState idleState { get; set; }
    public EnemyState moveAttack { get; set; }
    public EnemyState multiAttack { get; set; }
    public EnemyState explosion { get; set; }
     void Start()
    {
        stateMachine = new StateMachine();
        idleState = new IdleState(this, stateMachine);
        moveAttack = new MoveAttack(this, stateMachine);
        multiAttack = new MultiAttack(this, stateMachine);
        explosion = new Explosion(this, stateMachine);
        stateMachine.Initilize(multiAttack);
    }

    public void Update()
    {
        stateMachine.CurrentEnemyState.FrameUpdate();
    }
    public void FixedUpdate()
    {
        stateMachine.CurrentEnemyState.FixedUpdate();
    }

    
    
    
    
    
    
    
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
    public Transform[] GetMuzzle() => muzzle;
    public GameObject[] GetBullet_() => Bullet;
    public enum AnimationTriggerType
    {
        EnemyHurt,
        EnmeyDeath,
    }
    private void AnimationTriggerEvent(AnimationTriggerType type_)
    {
        stateMachine.CurrentEnemyState.AnimationTriggeredEvent(type_);
    }
    
}
