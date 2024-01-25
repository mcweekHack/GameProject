using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewport : Singleton<viewport>
{
    Vector2 minpot;
    Vector2 maxpot;
    Camera mainCamera;
    float midx;
    void Start()
    {
        mainCamera = Camera.main;
        minpot = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f));
        maxpot = mainCamera.ViewportToWorldPoint(new Vector3(1f, 1f));
        midx = (minpot.x + maxpot.x) / 2;
    }
    public Vector3 BackMovePos(Vector3 playerPos,Vector2 bias)
    {
        Vector3 res = Vector3.zero;
        res.x = Mathf.Clamp(playerPos.x, minpot.x+bias.x, maxpot.x-bias.x);
        res.y = Mathf.Clamp(playerPos.y, minpot.y+bias.y, maxpot.y-bias.y);
        return res;
    }
    public Vector3 BackEnemySpwan(Vector2 bias)//敌人出生位置
    {
        Vector3 res = Vector3.zero;
        res.x = maxpot.x + bias.x;
        res.y = Random.Range(minpot.y + bias.y, maxpot.y - bias.y);
        return res;
    }
    public Vector3 BackEnemyTarget(Vector2 bias)//敌人的下一个移动位置
    {
        Vector3 res = Vector3.zero;
        res.x = Random.Range(midx + bias.x, maxpot.x - bias.x);
        res.y = Random.Range(minpot.y + bias.y, maxpot.y - bias.y);
        return res;
    }
}
