using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class EnemyBulletLaucher : BaseBehaviour
{
    private bool isOnGround => Physics2D.Raycast(transform.position, -Vector2.up, 0.36f, LayerManager.GroundMask).collider != null;

    private void OnEnable()
    {
        player = null;
        beAtked = false;
    }

    private void Start()
    {
    }

    private void OnDisable()
    {
        if (disablePlayEffect != -1)
        {
            R.Effect.Generate(disablePlayEffect, null, transform.position);
        }
    }

    private void Update()
    {
        if (isOnGround && groundEffect != -1)
        {
            R.Effect.Generate(groundEffect, null, transform.position + new Vector3(0f, -0.3f, 0f));
            EffectController.TerminateEffect(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "PlayerHurtBox")
        {
            var args = new PlayerHurtAtkEventArgs(R.Player.GameObject, gameObject, attacker.gameObject, damage, Incrementor.GetNextId(), atkData);
            GameEvent.PlayerHurtAtk.Trigger(args);
            if (!beAtked)
            {
                player = R.Player.Transform;
            }

            HitBullet();
        }
    }

    /// <summary>
    /// 子弹伤害
    /// </summary>
    public void HitBullet()
    {
        if (beAtked && hitEffect != -1)
        {
            R.Effect.Generate(hitEffect, null, transform.position);
            R.Effect.Generate(49, null, transform.position);
        }

        if (player && hitEffect != -1)
        {
            R.Effect.Generate(hitEffect, null,
                new Vector3((transform.position.x + player.position.x) / 2f, transform.position.y, transform.position.z));
        }

        EffectController.TerminateEffect(gameObject);
    }

    public void SetVelocity(float speed, float angle)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Sin(angle * 0.0174532924f), speed * Mathf.Cos(angle * 0.0174532924f));
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -angle));
    }

    public void SetAtkData(JsonData data)
    {
        $"子弹类型伤害设置".Log();
        atkData = data;
    }

    [SerializeField] public float speed;
    [SerializeField] public float angle;
    [SerializeField] private bool canThrough;
    [SerializeField] private int hitEffect = -1;
    [SerializeField] private int groundEffect = -1;
    [SerializeField] private int disablePlayEffect = -1;
    [SerializeField] public bool beAtked;

    private JsonData atkData;

    public int damage;

    private Transform player;

    public Transform attacker;
}