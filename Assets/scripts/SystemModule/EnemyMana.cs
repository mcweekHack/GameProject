using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMana : Singleton<EnemyMana>
{
    public int WaveNum => RoundNum;
    public float WaveGap => TimeBetweenRound;
    [SerializeField] GameObject WaveUI;
    bool IsGenerate = true;
    [SerializeField] GameObject[] enemy;
    [SerializeField] static List<GameObject> enemyAlive;
    [SerializeField] float AwaitTime;
    [SerializeField] float TimeBetweenRound;
    WaitForSeconds AwaitT;
    WaitForSeconds TimeBtRo;
    int MaxEnemyNum = 20;
    int MinEnemyNum = 4;
    int RoundNum = 1;
    int EnemyAmount;
    public int EnemyLeft = 0;
    WaitUntil Noenemy;

    protected override void Awake()
    {
        base.Awake();
        AwaitT = new WaitForSeconds(AwaitTime);
        Noenemy = new WaitUntil(() => EnemyLeft<=0);
        TimeBtRo = new WaitForSeconds(TimeBetweenRound);
        enemyAlive = new List<GameObject>();
    }
    IEnumerator Start()
    {
        while (IsGenerate)
        {
            yield return Noenemy;
            WaveUI.SetActive(true);
            yield return TimeBtRo;
            WaveUI.SetActive(false);
            StartCoroutine(RandomlySpwanCoroutine(RoundNum));
        }
    }
    IEnumerator RandomlySpwanCoroutine(int round)
    {
        EnemyAmount = Mathf.Clamp(EnemyAmount, MinEnemyNum + RoundNum/3,MaxEnemyNum);
        EnemyLeft = EnemyAmount;
        for (var i = 0; i < EnemyAmount; i++)
        {
            enemyAlive.Add(Pool_manager.Release(enemy[Random.Range(0, enemy.Length)]));
            yield return AwaitT;
        }
        RoundNum++;
    }
    public static void RemoveList(GameObject target)
    {
        enemyAlive.Remove(target);
        return;
    }
    public static GameObject ReturnAliveEnemy()
    {
        if (enemyAlive.Count <= 0) return null;
        return enemyAlive[enemyAlive.Count - 1];
    }
}
