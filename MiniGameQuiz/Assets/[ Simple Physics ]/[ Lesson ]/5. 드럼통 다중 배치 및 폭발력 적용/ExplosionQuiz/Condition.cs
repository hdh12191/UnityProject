using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
    static bool falseFlag, winFlag, startFlag;
    static float timeDelta, _addSpeed, createEnemyTime, _startTime;
    static int createEnemyCount, startCount;
    public Condition()
    {
        falseFlag = false;
        winFlag = false;
        startFlag = false;
    }

    public bool ConditionChange(bool f)
    {
        falseFlag = f;
        return falseFlag;
    }

    public bool ReturnFlag()
    {
        return falseFlag;
    }
    public void SetTime(float addTime)
    {
        timeDelta += addTime;
    }
    public float ReturnTime()
    {
        return timeDelta;
    }
    public void SetSpeed(float addSpeed)
    {
        _addSpeed += addSpeed;
    }
    public float ReturnSpeed()
    {
        return _addSpeed;
    }
    public void SetEnemyCount(int count)
    {
        createEnemyCount += count;
    }
    public int ReturnEnemyCount()
    {
        return createEnemyCount;
    }
    public void SetEnemyTime(float time)
    {
        createEnemyTime += time;
    }
    public float ReturnEnemyTime()
    {
        return createEnemyTime;
    }
    public void SetWinFlag(bool f)
    {
        winFlag = f;
    }
    public bool ReturnWinFlag()
    {
        return winFlag;
    }
    public void SetCount(int cnt)
    {
        startCount = cnt;
    }
    public int ReturnCount()
    {
        return startCount;
    }
    public void SetStartFlag(bool f)
    {
        startFlag = f;
    }
    public bool ReturnStartFlag()
    {
        return startFlag;
    }

    public void SetStartTime(float time)
    {
        _startTime = time;
    }
    public float ReturnStartTime()
    {
        return _startTime;
    }
}
