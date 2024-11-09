using System;
using LitJson;
using UnityEngine;

/// <summary>
/// 玩家攻击
/// </summary>
public class PlayerAtk : BaseBehaviour
{
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void SetData(JsonData atkData, int atkId)
    {
        data = atkData;
        attackId = atkId;
        _hitTimes = data.Get(PlayerAtkDataType.hitTimes.ToString(), 0);
        _interval = data.Get(PlayerAtkDataType.interval.ToString(), 100f);
        _hitType = (HitType)data.Get(PlayerAtkDataType.hitType.ToString(), 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals(ConfigGameObject.EnemyHurtBox))
        {
            GameEvent.EnemyHurtAtk.Trigger(EventArgs(other, true));
        }
        else if (other.CompareTag(ConfigTag.EnemyBullet))
        {
            BreakBullet(other);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (WorldTime.I.IsFrozen || _hitType == HitType.Once || data == null) return;
        _interval -= Time.deltaTime;
        if (_interval > 0f) return;
        if (other.name.Equals(ConfigGameObject.EnemyHurtBox))
        {
            HitType hitType = _hitType;
            switch (hitType)
            {
                case HitType.Once:
                    break;
                case HitType.Limited:
                    LimitedAttack(other);
                    break;
                case HitType.UnLimited:
                    UnlimitedAttack(other);
                    break;
                default:
                    throw new Exception($"没有这个类型{_hitType}");
            }
        }
    }

    /// <summary>
    /// 无限制攻击
    /// </summary>
    /// <param name="other"></param>
    private void UnlimitedAttack(Collider2D other)
    {
        "无限制攻击".Log();
        _interval = data.Get(PlayerAtkDataType.interval.ToString(), 0f);
        attackId = Incrementor.GetNextId();
        GameEvent.EnemyHurtAtk.Trigger(EventArgs(other, false));
    }

    /// <summary>
    /// 有限的攻击
    /// </summary>
    /// <param name="other"></param>
    private void LimitedAttack(Collider2D other)
    {
        "有限的攻击".Log();
        if (_hitTimes > 0)
        {
            _interval = data.Get(PlayerAtkDataType.interval.ToString(), 0f);
            attackId = Incrementor.GetNextId();
            GameEvent.EnemyHurtAtk.Trigger(EventArgs(other, false));
            _hitTimes--;
        }
    }

    /// <summary>
    /// 打破子弹
    /// </summary>
    /// <param name="bullet"></param>
    private void BreakBullet(Collider2D bullet)
    {
        "打破子弹".Log();
        if (R.Player.StateMachine.currentState.IsInArray(PlayerAtkType.CantBreakBullet)) return;
        EnemyBullet component = bullet.GetComponent<EnemyBullet>();
        EnemyBulletLaucher component2 = bullet.GetComponent<EnemyBulletLaucher>();
        if (component != null)
        {
            component.beAtked = true;
            component.HitBullet();
        }
        else if (component2 != null)
        {
            component2.beAtked = true;
            component2.HitBullet();
        }

        WorldTime.I.TimeFrozenByFixedFrame(7);
        R.Camera.CameraController.CameraShake(0.166666672f, ShakeTypeEnum.Rect);
    }

    private Vector2 HurtPos(Bounds enemyBound)
    {
        return MathfX.Intersect2DCenter(enemyBound, _collider.bounds);
    }

    /// <summary>
    /// 敌人受伤时间参数
    /// </summary>
    /// <param name="enemyBody"></param>
    /// <param name="firstHurt"></param>
    /// <returns></returns>
    private EnemyHurtAtkEventArgs EventArgs(Collider2D enemyBody, bool firstHurt)
    {
        EnemyHurtAtkEventArgs.PlayerNormalAtkData playerNormalAtkData =
            new EnemyHurtAtkEventArgs.PlayerNormalAtkData(data, firstHurt);

        return new EnemyHurtAtkEventArgs(enemyBody.transform.parent.parent.gameObject,
            gameObject, attackId, HurtPos(enemyBody.bounds),
            HurtCheck.BodyType.Body, playerNormalAtkData);
    }

    /// <summary>
    /// 玩家动作的伤害数据
    /// </summary>
    private JsonData data;

    /// <summary>
    /// 攻击ID
    /// </summary>
    public int attackId;

    /// <summary>
    /// 间隔
    /// </summary>
    private float _interval;

    /// <summary>
    /// 命中次数
    /// </summary>
    private int _hitTimes;

    /// <summary>
    /// 撞击类型
    /// </summary>
    private HitType _hitType;


    /// <summary>
    /// 撞击类型
    /// </summary>
    public enum HitType
    {
        /// <summary>
        /// 一次
        /// </summary>
        Once,

        /// <summary>
        /// 有限的
        /// </summary>
        Limited,

        /// <summary>
        /// 无限的
        /// </summary>
        UnLimited
    }
}