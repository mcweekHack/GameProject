using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class player : Character
{
    [SerializeField] Stutas_bar_HUD Hud_health;

    [SerializeField] AudioData HurtSFX;
    [SerializeField] AudioData ShotAudio;
    [SerializeField] AudioData DogeAudio;
    [SerializeField] AudioData DeathAudio;



    [SerializeField] bool IfHealthGrow = true;
    [SerializeField] float HealthGrowDelayTime;
    [SerializeField] float HealthGrowPercent;
    [SerializeField] float MaxRoll;
    [SerializeField] float RollSpeed;
    [SerializeField] float DogeCost;
    [SerializeField] float DogeSpeed;
    bool IsOverdrive;
    [SerializeField] float OverMoveSpeedFactor;
    [SerializeField] float OverShotSpeedFactor;
    WaitForSeconds HealthGrowDelay;


    [SerializeField]playerinput input_;
    [SerializeField] float moveSpeed;
    [SerializeField]Vector2 bias;
    [SerializeField] float accetime;
    [SerializeField] float deccetime;
    [SerializeField] float flyangle;
    [SerializeField] float moveangle;
    WaitForSeconds FireDelayTime;
    float FireDelay;
    [SerializeField] GameObject playerbullet;
    [SerializeField] GameObject playerbulletOverDrive;
    [SerializeField] GameObject playerbulletSelect;
    [SerializeField] Transform muzzle;
    [SerializeField] Collider2D Playercollider;
    Rigidbody2D rb;
    Quaternion moveQua;




    Coroutine moveCoro;
    Coroutine FireCoro;
    Coroutine HealthGrowCoro;
    Coroutine DogeCoro;
    Coroutine ContinCoro;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Playercollider = GetComponent<Collider2D>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        input_.onMove += OnMove;
        input_.stopMove += StopMove;
        input_.onFire += OnFire;
        input_.stopFire += StopFire;
        input_.SkillDoge += Doge;
        input_.onOverdirve += OnOverdrive;
        PlayerOverdrive.on += OnOverdriveOn;
        PlayerOverdrive.off += OnOverdriveOff;

        Hud_health.Initialize(MaxHealth, health);
        energy_system.instance.ClearEnergy();
    }
    void OnDisable()
    {
        input_.onMove -= OnMove;
        input_.stopMove -= StopMove;
        input_.onFire -= OnFire;
        input_.stopFire -= StopFire;
        input_.SkillDoge -= Doge;
        input_.onOverdirve -= OnOverdrive;
        PlayerOverdrive.on -= OnOverdriveOn;
        PlayerOverdrive.off -= OnOverdriveOff;
        try
        {
            Score_system.instance.Score_Reset();
        }
        catch
        {

        }
    }
    void Start()
    {
        IsOverdrive = false;
        HealthGrowDelay = new WaitForSeconds(HealthGrowDelayTime);
        FireDelay = 0.2f;
        FireDelayTime = new WaitForSeconds(FireDelay);
        flyangle = 30f;
        moveangle = -5f;
        moveCoro = null;
        accetime = 0.5f;
        deccetime = 0.5f;
        bias = new Vector2(0.8f, 0.3f);
        rb.gravityScale = 0f;
        moveSpeed = 10f;
        DogeSpeed = 12f;
        DogeCost = 25f;
        RollSpeed = 400f;
        MaxRoll = 360f;
        OverMoveSpeedFactor = 1.2f;
        OverShotSpeedFactor = 2f;
        input_.EnableCon();
    }
    void OnMove(Vector2 MoveSignal)
    {
        if(moveCoro!=null)
            StopCoroutine(moveCoro);
        moveQua = Quaternion.AngleAxis(flyangle * MoveSignal.y, Vector3.right) * Quaternion.AngleAxis(moveangle * MoveSignal.x, Vector3.forward);
        moveCoro = StartCoroutine(MovelerpCoroutine(rb, accetime, MoveSignal.normalized * moveSpeed,moveQua));
        if (ContinCoro != null)
            StopCoroutine(ContinCoro);
            ContinCoro = StartCoroutine(PositionContaionCoroutine());
    }
    void StopMove()
    {
        if(moveCoro!=null)
            StopCoroutine(moveCoro);
        moveCoro = StartCoroutine(MovelerpCoroutine(rb, deccetime, Vector2.zero, Quaternion.identity));
        if(ContinCoro != null)
        StopCoroutine(ContinCoro);
    }
    void OnFire()
    {
        FireCoro = StartCoroutine(FireCoroutine());
    }
    void StopFire()
    {
        if (FireCoro != null)
            StopCoroutine(FireCoro);
    }

    public override void TakeDamage(float damage)
    {
        AudioMana.instance.playSFXRandomly(HurtSFX);
        base.TakeDamage(damage);
        Hud_health.UpdateStatus(MaxHealth, health);
        if (gameObject.activeSelf)
        {
            if (IfHealthGrow)
            {
                if (HealthGrowCoro != null)
                    StopCoroutine(HealthGrowCoro);
                HealthGrowCoro = StartCoroutine(HealthGrowingCorotuine(HealthGrowDelay, HealthGrowPercent));
            }
        }
    }

    IEnumerator PositionContaionCoroutine()
    {
        while (true)
        {
            transform.position = viewport.instance.BackMovePos(transform.position,bias);
            yield return null;
        }
    }
    IEnumerator MovelerpCoroutine(Rigidbody2D rb,float Ltime,Vector2 vel,Quaternion moveQua)
    {
        float t = 0f;
        while (t < Ltime)
        {
            t += Time.deltaTime;
            rb.velocity = Vector2.Lerp(rb.velocity,vel,t / Ltime);
            transform.rotation = Quaternion.Lerp(transform.rotation, moveQua, t / Ltime);
            yield return null;
        }
        StopCoroutine(moveCoro);
    }
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            Pool_manager.Release(playerbulletSelect, muzzle.position, Quaternion.identity);
            AudioMana.instance.playSFXRandomly(ShotAudio);
            yield return FireDelayTime;
        }
    }
    public override void RestoreHealth(float Add)
    {
        base.RestoreHealth(Add);
        Hud_health.UpdateStatus(MaxHealth, health);
    }
    public override void Die()
    {
        Hud_health.UpdateStatus(MaxHealth, 0f);
        AudioMana.instance.playSFXRandomly(DeathAudio);
        base.Die();
    }
    void Doge()
    {
        Vector2 tmp = rb.velocity.normalized;
        if (tmp == Vector2.zero) return;
        if (!energy_system.instance.UseEnergy(DogeCost)) return;
        if (DogeCoro != null) StopCoroutine(DogeCoro);
        if (moveCoro != null) StopCoroutine(moveCoro);
        if (FireCoro != null) StopCoroutine(FireCoro);
        DogeCoro = StartCoroutine(DogeCoroutine(tmp));
    }
    IEnumerator DogeCoroutine(Vector2 dir)
    {
        float cur = 0;
        Playercollider.isTrigger = true;

        input_.onMove -= OnMove;
        input_.onFire -= OnFire;
        input_.SkillDoge -= Doge;
        AudioMana.instance.playSFXRandomly(DogeAudio);
        while (cur <= MaxRoll)
        {
            rb.velocity = dir * DogeSpeed;
            transform.rotation = Quaternion.AngleAxis(cur, Vector3.right);
            cur += Time.fixedDeltaTime*RollSpeed;
            yield return null;
        }
        rb.velocity = Vector2.zero;
        input_.onMove += OnMove;
        input_.onFire += OnFire;
        input_.SkillDoge += Doge;

        transform.rotation = Quaternion.AngleAxis(0f, Vector3.right);
        Playercollider.isTrigger = false;
    }
    void OnOverdrive()
    {
        if (!energy_system.instance.IsFullEnergy()) return;
        PlayerOverdrive.on.Invoke();
    }
    void OnOverdriveOn()
    {
        IsOverdrive = true;
        moveSpeed *= OverMoveSpeedFactor;
        FireDelayTime = new WaitForSeconds(FireDelay/ OverShotSpeedFactor);
        playerbulletSelect = playerbulletOverDrive;
    }
    void OnOverdriveOff()
    {
        IsOverdrive = false;
        moveSpeed /= OverMoveSpeedFactor;
        FireDelayTime = new WaitForSeconds(FireDelay);
        playerbulletSelect = playerbullet;
    }

}
