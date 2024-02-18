using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCon : Character
{
    [SerializeField] public AudioData hurtAudio;
    [SerializeField] public AudioData DaethAudio;
    [SerializeField] public AudioData ShootSound1;
    [SerializeField] public AudioData ShootSound2;
    [SerializeField] public AudioData LazerSound;

    [SerializeField] int Score;
    [SerializeField] public float MoveDur;
    Vector2 tmp;


    [SerializeField] GameObject self;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform[] muzzle;
    [SerializeField] GameObject[] Bullet;
    public StateMachine stateMachine { get; set; }
    public List<EnemyState> States;
     void Start()
    {
        stateMachine = new StateMachine();
        States = new List<EnemyState>();
        States.Add(new WalkAround(this, stateMachine));
        States.Add(new MoveAttack(this, stateMachine));
        States.Add(new MultiAttack(this, stateMachine));
        States.Add(new Explosion(this, stateMachine));
        States.Add(new LaserAttack(this, stateMachine));
        stateMachine.Initilize(States[0]);
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
