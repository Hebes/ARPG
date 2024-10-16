using System.Collections.Generic;
using LitJson;
using UnityEngine;

/// <summary>
/// 敌人攻击
/// </summary>
public class EnemyAtk : BaseBehaviour
{
    public Dictionary<PlayerHurtDataType, string> atkData
    {
        private get => _atkData;
        set
        {
            _atkData = value;
            hitTimes = 0;
            hitInterval = 0;
            hitType = 1;
            // hitTimes =  value.Get<int>("hitTimes", 0);
            // hitInterval = value.Get<float>("hitInterval", 0f);
            // hitType = value.Get<int>("hitType", 1);
        }
    }

    private void Start()
    {
        eAttr = transform.parent.GetComponent<EnemyAttribute>();
    }

    private void Update()
    {
        if (atkStart)
        {
            hitInterval = Mathf.Clamp(hitInterval - Time.deltaTime, 0f, float.PositiveInfinity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(CTag.EnemyHurtBox))
        {
            "玩家受到伤害".Log();
            PlayerHurtAtkEventArgs args = new PlayerHurtAtkEventArgs(other.transform.parent.gameObject,
                transform.parent.gameObject,
                transform.parent.gameObject, eAttr.atk, atkId, atkData);
            GameEvent.PlayerHurtAtk.Trigger(args);
            atkStart = true;
        }
       
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (WorldTime.I.IsFrozen) return;
        if (atkData == null) return;
        "敌人攻击".Log();
        if (other.CompareTag(CTag.EnemyHurtBox))
        {
            switch (hitType)
            {
                case 0:
                    UnlimitedAttack(other);
                    break;
                case 1:
                    LimitedAttack(other);
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(CTag.EnemyHurtBox))
        {
            atkStart = false;
        }
    }

    /// <summary>
    /// 无限制攻击
    /// </summary>
    /// <param name="other"></param>
    private void UnlimitedAttack(Collider2D other)
    {
        if (hitInterval <= 0f)
        {
            // atkId = Incrementor.GetNextId();
            // hitInterval = atkData.Get<float>("hitInterval", 0f);
            // PlayerHurtAtkEventArgs args = new PlayerHurtAtkEventArgs(other.transform.parent.gameObject, transform.parent.gameObject,
            //     transform.parent.gameObject, eAttr.atk, atkId, atkData);
            // EGameEvent.PlayerHurtAtk.Trigger((transform, args));
        }
    }

    /// <summary>
    /// 有限的攻击
    /// </summary>
    /// <param name="other"></param>
    private void LimitedAttack(Collider2D other)
    {
        if (hitTimes > 0)
        {
            // hitInterval -= Time.deltaTime;
            // if (hitInterval <= 0f)
            // {
            //     atkId = Incrementor.GetNextId();
            //     hitInterval = atkData.Get<float>("hitInterval", 0f);
            //     PlayerHurtAtkEventArgs args = new PlayerHurtAtkEventArgs(other.transform.parent.gameObject, transform.parent.gameObject,
            //         transform.parent.gameObject, eAttr.atk, atkId, atkData);
            //     EGameEvent.PlayerHurtAtk.Trigger((transform, args));
            //     hitTimes--;
            // }
        }
    }

    private  Dictionary<PlayerHurtDataType, string> _atkData;

    /// <summary>
    /// 敌人属性
    /// </summary>
    private EnemyAttribute eAttr;

    public int atkId;

    /// <summary>
    /// 命中次数
    /// </summary>
    private int hitTimes;

    /// <summary>
    /// 打击时间间隔
    /// </summary>
    private float hitInterval;

    /// <summary>
    /// 命中类型
    /// </summary>
    private int hitType;

    /// <summary>
    /// 是否攻击开始
    /// </summary>
    public bool atkStart;
}