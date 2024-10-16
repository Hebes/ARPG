using UnityEngine;

/// <summary>
/// 移动区域限制
/// </summary>
public class MoveAreaLimit : BaseBehaviour
{
    private EnemyAttribute eAttr;
    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponentInChildren<Rigidbody2D>();

        if (gameObject.CompareTag(CTag.Enemy))
        {
            type = LimitType.Enemy;
            bodySize = GetComponentInChildren<Collider2D>().bounds.size;
        }
        else if (gameObject.CompareTag(CTag.Player))
        {
            type = LimitType.Player;
        }
    }

    private void Update()
    {
        if (_frameCount < 1)
        {
            _frameCount++;
            return;
        }

        _frameCount = 0;
        float num = 0.05f;
        Vector3 position = transform.position;
        Vector3 v = body.velocity;
        if (type == LimitType.Player)
        {
            float num2 = GameArea.PlayerRange.xMin + bodySize.x / 2f;
            float num3 = GameArea.PlayerRange.xMax - bodySize.x / 2f;
            if (position.x > num3 + num || position.x < num2 - num)
            {
                GameEvent.HitWall.Trigger((this,new HitWallArgs(gameObject)));
                v.x = 0f;
                body.velocity = v;
                body.position = new Vector2(Mathf.Clamp(position.x, num2, num3),
                    Mathf.Clamp(position.y, GameArea.PlayerRange.yMin, GameArea.PlayerRange.yMax));
            }

            if (position.y > GameArea.PlayerRange.yMax + num)
            {
                v.y = 0f;
                body.velocity = v;
                body.position = new Vector2(Mathf.Clamp(position.x, num2, num3),
                    Mathf.Clamp(position.y, GameArea.PlayerRange.yMin, GameArea.PlayerRange.yMax));
            }
        }

        float yMax = GameArea.MapRange.yMax;
        float yMin = GameArea.MapRange.yMin;
        Vector3 position2 = transform.position;
        if (yMin > position2.y)
        {
            position2.y = yMax;
            transform.position = position2;
            $"{transform.name}击穿了地面".Log();
        }
    }

    public LimitType type = LimitType.Enemy;
    public Vector2 bodySize = new Vector2(0.5f, 0f);
    public int battleZoneId = 1;
    private const int Freq = 1;
    private int _frameCount;

    /// <summary>
    /// 区域限制类型
    /// </summary>
    public enum LimitType
    {
        Player,
        Enemy,
        Map
    }
}