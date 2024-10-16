using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人管理器
/// </summary>
public class EnemyManager
{
    /// <summary>
    /// 获取敌人类型
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public EnemyType GetEnemyType(string name)
    {
        return EnemyGenerator.GetEnemyType(name);
    }

    /// <summary>
    /// 产生
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pos"></param>
    /// <param name="withEffect"></param>
    /// <param name="enemyPoint"></param>
    /// <returns></returns>
    public GameObject Generate(string name, Vector2? pos = null, bool withEffect = true, bool enemyPoint = true)
    {
        return Generator.GenerateEnemy(name, pos, withEffect, enemyPoint);
    }
    
    /// <summary>
    /// 产生
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    /// <param name="withEffect"></param>
    /// <param name="enemyPoint"></param>
    /// <returns></returns>
    public GameObject Generate(EnemyType type, Vector2? pos = null, bool withEffect = true, bool enemyPoint = true)
    {
        return Generator.GenerateEnemy(type, pos, withEffect, enemyPoint);
    }
    
    private EnemyGenerator Generator => EnemyGenerator.I;

    public List<EnemyAttribute> EnemyAttributes => _enemyAttributes;

    public int Count => _enemyAttributes.Count;

    public GameObject First => _enemyAttributes.Count <= 0 || !(_enemyAttributes[0] != null) ? null : _enemyAttributes[0].gameObject;

    private List<GameObject> GetXNearestRangeEnemy(float middle, float range)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < _enemyAttributes.Count; i++)
        {
            if (MathfX.isInMiddleRange(_enemyAttributes[i].transform.position.x, middle, range))
            {
                list.Add(_enemyAttributes[i].gameObject);
            }
        }

        return list;
    }

    public bool HasNearEnemy(float xPos, float range)
    {
        for (int i = 0; i < _enemyAttributes.Count; i++)
        {
            if (MathfX.isInMiddleRange(_enemyAttributes[i].transform.position.x, xPos, range))
                return true;
        }

        return false;
    }

    public bool HasEnemyInRect(Rect rect)
    {
        for (int i = 0; i < _enemyAttributes.Count; i++)
        {
            if (rect.Contains(_enemyAttributes[i].transform.position))
                return true;
        }

        return false;
    }

    public GameObject GetNearestRangeEnemys(Vector2 pos, float range)
    {
        GameObject result = null;
        List<GameObject> xnearestRangeEnemy = GetXNearestRangeEnemy(pos.x, range);
        float num = range;
        for (int i = 0; i < xnearestRangeEnemy.Count; i++)
        {
            float num2 = Vector2.Distance(pos, xnearestRangeEnemy[i].transform.position);
            if (num2 < num && num2 < range)
            {
                result = xnearestRangeEnemy[i];
                num = num2;
            }
        }

        return result;
    }

    public GameObject GetNearestRangeEnemyWithDir(Vector2 pos, float rectX, float rectY, int dir, bool isNeedAlive, bool isOnGround)
    {
        GameObject result = null;
        List<GameObject> xnearestRangeEnemy = GetXNearestRangeEnemy(pos.x, rectX + rectY);
        float num = rectX + rectY;
        for (int i = 0; i < xnearestRangeEnemy.Count; i++)
        {
            // GameObject gameObject = xnearestRangeEnemy[i];
            // bool flag = InputSetting.JudgeDir(pos, gameObject.transform.position) == dir;
            // bool flag2 = MathfX.isInMiddleRange(gameObject.transform.position.x, pos.x, rectX) &&
            //              MathfX.isInMiddleRange(gameObject.transform.position.y, pos.y, rectY);
            // EnemyAttribute component = gameObject.GetComponent<EnemyAttribute>();
            // bool flag3 = component != null && !component.isDead;
            // bool flag4 = component != null && component.isOnGround;
            // if (flag2 && flag && flag3 == isNeedAlive && flag4 == isOnGround)
            // {
            //     float num2 = Vector2.Distance(pos, gameObject.transform.position);
            //     if (num2 < num)
            //     {
            //         result = gameObject;
            //         num = num2;
            //     }
            // }
        }

        return result;
    }

    public bool HasXNearestRangeEnemy(float middle, float diameter)
    {
        for (int i = 0; i < _enemyAttributes.Count; i++)
        {
            if (MathfX.isInMiddleRange(_enemyAttributes[i].transform.position.x, middle, diameter))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 获取最远的敌人位置
    /// </summary>
    /// <param name="pivot"></param>
    /// <param name="rect"></param>
    /// <returns></returns>
    public Vector3? GetFarestEnemyPosition(Vector3 pivot, Rect? rect = null)
    {
        float num = 0f;
        int num2 = -1;
        for (int i = 0; i < EnemyAttributes.Count; i++)
        {
            EnemyAttribute enemyAttribute = EnemyAttributes[i];
            if (Vector3.Distance(enemyAttribute.transform.position, pivot) > num)
            {
                if (rect != null)
                {
                    if (Mathf.Abs(enemyAttribute.transform.position.x - rect.Value.center.x) < rect.Value.width / 2f &&
                        Mathf.Abs(enemyAttribute.transform.position.y - rect.Value.center.y) < rect.Value.height / 2f)
                    {
                        num = Vector3.Distance(enemyAttribute.transform.position, pivot);
                        num2 = i;
                    }
                }
                else
                {
                    num = Vector3.Distance(enemyAttribute.transform.position, pivot);
                    num2 = i;
                }
            }
        }

        return (num2 != -1) ? new Vector3?(EnemyAttributes[num2].transform.position) : null;
    }

    public Vector3 GetAverageEnemyPosition(Vector3 player, float distance = 10f)
    {
        int num = 0;
        Vector3 vector = Vector3.zero;
        for (int i = 0; i < _enemyAttributes.Count; i++)
        {
            if (Vector3.Distance(player, _enemyAttributes[i].transform.position) < distance)
            {
                vector += _enemyAttributes[i].transform.position;
                num++;
            }
        }

        if (num != 0)
        {
            vector /= num;
        }

        return vector;
    }

    public GameObject GetEnemyById(string id)
    {
        for (int i = 0; i < _enemyAttributes.Count; i++)
        {
            if (_enemyAttributes[i].id == id)
            {
                return _enemyAttributes[i].gameObject;
            }
        }

        return null;
    }

    public GameObject GetEnemyByType(EnemyType type)
    {
        for (int i = 0; i < _enemyAttributes.Count; i++)
        {
            if (_enemyAttributes[i].baseData.enemyType == type)
            {
                return _enemyAttributes[i].gameObject;
            }
        }

        return null;
    }

    public List<GameObject> GetEnemysByType(EnemyType type)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < _enemyAttributes.Count; i++)
        {
            if (_enemyAttributes[i].baseData.enemyType == type)
            {
                list.Add(_enemyAttributes[i].gameObject);
            }
        }

        return list;
    }

    public int GetEnemyCountByType(EnemyType type)
    {
        int num = 0;
        for (int i = 0; i < _enemyAttributes.Count; i++)
        {
            if (_enemyAttributes[i].baseData.enemyType == type)
            {
                num++;
            }
        }

        return num;
    }

    public bool CanBeExecutedEnemyExist()
    {
        bool flag = false;
        for (int i = 0; i < R.Enemy.Count; i++)
        {
            //flag |= R.Enemy.EnemyAttributes[i].CurrentCanBeExecute;
        }

        return flag;
    }

    public void AddEnemy(EnemyAttribute enemyAttribute)
    {
        if (enemyAttribute != null)
        {
            _enemyAttributes.Add(enemyAttribute);
        }
    }

    public void RemoveEnemy(EnemyAttribute enemyAttribute)
    {
        if (enemyAttribute != null)
        {
            _enemyAttributes.Remove(enemyAttribute);
        }
    }

    public void KillAllEnemy()
    {
        if (_enemyAttributes != null)
        {
            for (int i = 0; i < _enemyAttributes.Count; i++)
            {
                _enemyAttributes[i].currentHp = 0;
            }
        }
    }

    public void Clear()
    {
        Boss = null;
        _enemyAttributes.Clear();
    }

    private readonly List<EnemyAttribute> _enemyAttributes = new List<EnemyAttribute>();

    public GameObject Boss;
}