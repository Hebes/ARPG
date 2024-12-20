﻿using LitJson;
using UnityEngine;

/// <summary>
/// 敌人攻击
/// </summary>
public class EnemyAtk : BaseBehaviour
{
    private JsonData _atkData;

    public JsonData atkData
    {
        private get => _atkData;
        set
        {
            _atkData = value;
            hitTimes = value.Get("hitTimes", 0);
            hitInterval = value.Get("hitInterval", 0f);
            hitType = value.Get("hitType", 1);
        }
    }

    private void Awake()
    {
        eAttr = transform.parent.parent.GetComponent<EnemyAttribute>();
    }

    private void Update()
    {
        if (atkStart)
        {
            hitInterval = Mathf.Clamp(hitInterval - Time.deltaTime, 0f, float.PositiveInfinity); //更新打击间隔时间
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals(ConfigGameObject.PlayerHurtBox))
        {
            GameObject hurt = other.transform.parent.parent.gameObject;
            GameObject target = transform.parent.parent.gameObject;
            var args = new PlayerHurtAtkEventArgs(hurt, target, target, eAttr.atk, atkId, atkData);
            GameEvent.PlayerHurtAtk.Trigger(args);
            atkStart = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (WorldTime.I.IsFrozen) return;
        if (atkData == null) return;
        "敌人攻击".Log();
        if (other.name.Equals(ConfigGameObject.PlayerHurtBox))
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
        if (other.name.Equals(ConfigGameObject.PlayerHurtBox))
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
            atkId = Incrementor.GetNextId();
            hitInterval = atkData.Get<float>("hitInterval", 0f);
            PlayerHurtAtkEventArgs args = new PlayerHurtAtkEventArgs(other.transform.parent.gameObject, transform.parent.gameObject,
                transform.parent.gameObject, eAttr.atk, atkId, atkData);
            GameEvent.PlayerHurtAtk.Trigger((transform, args));
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
            hitInterval -= Time.deltaTime;
            if (hitInterval <= 0f)
            {
                atkId = Incrementor.GetNextId();
                hitInterval = atkData.Get<float>("hitInterval", 0f);
                var go1 = transform.parent.gameObject;
                var args = new PlayerHurtAtkEventArgs(other.transform.parent.gameObject, go1, go1, eAttr.atk, atkId, atkData);
                GameEvent.PlayerHurtAtk.Trigger((transform, args));
                hitTimes--;
            }
        }
    }


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