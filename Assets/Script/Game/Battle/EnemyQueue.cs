using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 敌人队列
/// </summary>
public class EnemyQueue : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvent.EnemyKilled.Register(EnemyDie);
    }

    private void OnDisable()
    {
        GameEvent.EnemyKilled.UnRegister(EnemyDie);
    }

    /// <summary>
    /// 敌人死亡
    /// </summary>
    /// <param name="udata"></param>
    private void EnemyDie(object udata)
    {
        EnemyAttribute enemyAttribute = udata as EnemyAttribute;
        StartCoroutine(CheckEnemy(enemyAttribute.baseData.enemyType));
    }

    /// <summary>
    /// 获取敌人数量
    /// </summary>
    /// <returns></returns>
    public int GetEnemyCount()
    {
        return queueData.EnemyType.Count;
    }

    /// <summary>
    /// 检查敌人是否存在
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private bool CheckEnemyExist(EnemyType type)
    {
        foreach (EnemyType enemyType in queueData.EnemyType)
        {
            if (type == enemyType)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 检查敌人死亡
    /// </summary>
    /// <param name="sender"></param>
    private void CheckEnemyDie(object sender)
    {
        EnemyAttribute eAttr = sender as EnemyAttribute;
        StartCoroutine(GenerateEnemyDelay(eAttr));
    }

    private IEnumerator GenerateEnemyDelay(EnemyAttribute eAttr)
    {
        GameObject e = R.Enemy.GetEnemyByType(queueData.CheckEnemy);
        yield return new WaitForSeconds(queueData.DelayTime);
        if (queueData.CheckEnemy != EnemyType.未定)
        {
            if (e != null && !e.GetComponent<EnemyAttribute>().isDead)
            {
                GenerateEnemy(eAttr.baseData.enemyType);
            }
        }
        else
        {
            GenerateEnemy(eAttr.baseData.enemyType);
        }
    }

    private void GenerateEnemy()
    {
        // int index = Random.Range(0, queueData.EnemyType.Count);
        // if (!queueData.Random)
        // {
        // 	index = 0;
        // }
        // Rect rect = queueData.enemyQueueArea.ToRect();
        // Vector2 value = new Vector2(Random.Range(rect.x - rect.width / 2f, rect.x + rect.width / 2f), Random.Range(rect.y - rect.height / 2f, rect.y + rect.height / 2f));
        // Singleton<EnemyGenerator>.Instance.GenerateEnemy(queueData.EnemyType[index], new Vector2?(value), true, true);
        // queueData.EnemyType.RemoveAt(index);
    }

    private void GenerateEnemy(EnemyType type)
    {
        // int index = Random.Range(0, queueData.EnemyType.Count);
        // if (queueData.EnemyType.Count == 1)
        // {
        // 	index = 0;
        // }
        // Rect rect = queueData.enemyQueueArea.ToRect();
        // Vector2 value = new Vector2(Random.Range(rect.x - rect.width / 2f, rect.x + rect.width / 2f), Random.Range(rect.y - rect.height / 2f, rect.y + rect.height / 2f));
        // if (queueData.Random && queueData.EnemyType.Count > 0)
        // {
        // 	Singleton<EnemyGenerator>.Instance.GenerateEnemy(queueData.EnemyType[index], new Vector2?(value), true, true);
        // 	if (queueData.Limited)
        // 	{
        // 		foreach (EnemyType enemyType in queueData.EnemyType)
        // 		{
        // 			if (queueData.EnemyType[index] == enemyType)
        // 			{
        // 				queueData.EnemyType.Remove(enemyType);
        // 				break;
        // 			}
        // 		}
        // 	}
        // }
        // else
        // {
        // 	Singleton<EnemyGenerator>.Instance.GenerateEnemy(type, new Vector2?(value), true, true);
        // }
    }

    /// <summary>
    /// 检查敌人
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private IEnumerator CheckEnemy(EnemyType type)
    {
        yield return new WaitForSeconds(queueData.DelayTime);
        if (queueData.CheckEnemy == EnemyType.未定)
        {
            CheckSuppleType(type);
        }
        else
        {
            GameObject enemyByType = R.Enemy.GetEnemyByType(queueData.CheckEnemy);
            if (enemyByType != null && !enemyByType.GetComponent<EnemyAttribute>().isDead)
            {
                CheckSuppleType(type);
            }
        }
    }

    private void CheckSuppleType(EnemyType type)
    {
        if (queueData.Random)
        {
            RandomSuppleEnemy();
        }
        else
        {
            GivenTypeSuppleEnemy(type);
        }
    }

    /// <summary>
    /// 随机敌人
    /// </summary>
    private void RandomSuppleEnemy()
    {
        //随机敌人
        // int index = Random.Range(0, queueData.EnemyType.Count);
        // if (queueData.EnemyType.Count == 1)
        // {
        // 	index = 0;
        // }
        // Vector2 spwanPosition = GetSpwanPosition();
        // if (queueData.Limited)
        // {
        // 	if (queueData.EnemyType.Count > 0)
        // 	{
        // 		Singleton<EnemyGenerator>.Instance.GenerateEnemy(queueData.EnemyType[index], new Vector2?(spwanPosition), true, true);
        // 		queueData.EnemyType.RemoveAt(index);
        // 	}
        // }
        // else if (queueData.EnemyType.Count > 0)
        // {
        // 	Singleton<EnemyGenerator>.Instance.GenerateEnemy(queueData.EnemyType[index], new Vector2?(spwanPosition), true, true);
        // }
        // else
        // {
        // 	Log.Alert("怪物池为空！！！" + LevelManager.SceneName);
        // }
    }

    /// <summary>
    /// 给定类灵活敌人
    /// </summary>
    /// <param name="type"></param>
    private void GivenTypeSuppleEnemy(EnemyType type)
    {
        // if (!CheckEnemyExist(type))return;
        // Vector2 spwanPosition = GetSpwanPosition();
        // if (queueData.Limited)
        // {
        // 	if (queueData.EnemyType.Count > 0)
        // 	{
        // 		Singleton<EnemyGenerator>.Instance.GenerateEnemy(type, new Vector2?(spwanPosition), true, true);
        // 		RemoveEnemyFromPool(type);
        // 	}
        // }
        // else
        // {
        // 	Singleton<EnemyGenerator>.Instance.GenerateEnemy(type, new Vector2?(spwanPosition), true, true);
        // }
    }

    private Vector2 GetSpwanPosition()
    {
        Rect rect = queueData.enemyQueueArea.ToRect();
        return new Vector2(Random.Range(rect.x - rect.width / 2f, rect.x + rect.width / 2f),
            Random.Range(rect.y - rect.height / 2f, rect.y + rect.height / 2f));
    }

    private void RemoveEnemyFromPool(EnemyType type)
    {
        foreach (EnemyType enemyType in queueData.EnemyType)
        {
            if (type == enemyType)
            {
                queueData.EnemyType.Remove(enemyType);
                break;
            }
        }
    }

    [SerializeField] public EnemyQueueData queueData;
}

[Serializable]
public class EnemyQueueData
{
    public EnemyType CheckEnemy;

    public bool Random;

    public bool Limited = true;

    public float DelayTime = 0.5f;

    public AreaRect enemyQueueArea = default(AreaRect);

    public List<EnemyType> EnemyType = new List<EnemyType>();
}

public struct AreaRect
{
    public AreaRect(Rect r)
    {
        xMin = r.xMin;
        xMax = r.xMax;
        yMin = r.yMin;
        yMax = r.yMax;
    }

    public Rect ToRect()
    {
        return Rect.MinMaxRect(xMin, yMin, xMax, yMax);
    }

    public float xMin;

    public float xMax;

    public float yMin;

    public float yMax;
}