using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 敌人子弹
/// </summary>
public class EnemyBullet : BaseBehaviour
{
    /// <summary>
    /// 是否击中地面
    /// </summary>
    private bool _hitGround => Physics2D.OverlapBox(transform.position, Vector2.one, 0f,
        LayerManager.WallMask | LayerManager.GroundMask | LayerManager.OneWayGroundMask | LayerManager.CeilingMask | LayerManager.ObstacleMask);

    private void OnEnable()
    {
        player = null;
        beAtked = false;
    }

    private void Update()
    {
        if (_hitGround && !enableOnGround)
        {
            if (explosionEffect > 0)
            {
                R.Effect.Generate(explosionEffect, null, transform.position);
            }

            R.Audio.PlayEffect(Random.Range(136, 139), transform.position);
            EffectController.TerminateEffect(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "PlayerHurtBox")
        {
            var args = new PlayerHurtAtkEventArgs(other.transform.parent.gameObject, gameObject, origin, damage, Incrementor.GetNextId(), atkData);
            GameEvent.PlayerHurtAtk.Trigger(args);
            if (hitAudio > 0)
            {
                R.Audio.PlayEffect(hitAudio, transform.position);
            }

            switch (type)
            {
                case BUlletType.Once:
                    if (!beAtked)
                    {
                        player = other.transform.parent;
                        HitBullet();
                    }

                    break;
                case BUlletType.Stay:
                    Destroy(GetComponent<Collider2D>());
                    break;
                case BUlletType.Continue:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// 被子弹攻击
    /// </summary>
    public void HitBullet()
    {
        if (beAtked)
        {
            if (explosionEffect > 0)
            {
                R.Effect.Generate(explosionEffect, null, transform.position);
            }

            R.Effect.Generate(49, null, transform.position);
            R.Audio.PlayEffect(Random.Range(136, 139), transform.position);
        }

        if (player)
        {
            if (explosionEffect > 0)
            {
                R.Effect.Generate(explosionEffect, null,
                    new Vector3((transform.position.x + player.position.x) / 2f,
                        transform.position.y, transform.position.z));
            }

            R.Audio.PlayEffect(Random.Range(136, 139), transform.position);
        }

        EffectController.TerminateEffect(gameObject);
    }

    private void OnDisable()
    {
        if (missAudio > 0)
        {
            R.Audio.PlayEffect(missAudio, transform.position);
        }
    }

    public void SetAtkData(JsonData jsonData)
    {
        $"子弹伤害设置".Log();
        atkData = jsonData;
    }

    private Transform player;

    /// <summary>
    /// 是否被攻击
    /// </summary>
    public bool beAtked;

    public int damage;
    public JsonData atkData;
    [HideInInspector] public GameObject origin;
    [SerializeField] public int explosionEffect = 90;
    [SerializeField] private bool enableOnGround;
    public BUlletType type;
    public EnemyType EnemyTypeOfShooter;
    public int hitAudio;
    public int missAudio;

    /// <summary>
    /// 子弹头型
    /// </summary>
    public enum BUlletType
    {
        /// <summary>
        /// 一次
        /// </summary>
        Once,

        /// <summary>
        /// 停留
        /// </summary>
        Stay,

        /// <summary>
        /// 持续
        /// </summary>
        Continue
    }
}