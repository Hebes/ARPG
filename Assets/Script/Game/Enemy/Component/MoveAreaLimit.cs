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
        if (gameObject.tag.Equals(ConfigTag.Player))
        {
            type = LimitType.Player;
            bodySize = GetComponent<Collider2D>().bounds.size;
        }

        body = GetComponent<Rigidbody2D>();
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
        Vector3 position = transform.position; //自己的位置
        Vector3 v = body.velocity; //获取或设置物体的速度

        if (type == LimitType.Player)
        {
            Rect playerRange = GameArea.PlayerRange;
            float num2 = playerRange.xMin + bodySize.x / 2f;
            float num3 = playerRange.xMax - bodySize.x / 2f;
            float x = Mathf.Clamp(position.x, num2, num3);
            float y = Mathf.Clamp(position.y, playerRange.yMin, playerRange.yMax);
            if (position.x > num3 + num || position.x < num2 - num) //如果玩家超过最大或最小的PlayerRange范围
            {
                GameEvent.HitWall.Trigger(new HitWallArgs(gameObject));
                
                v.x = 0f; //设置物体的速度
                body.velocity = v; //物体的速度
                body.position = new Vector2(x, y);
            }

            if (position.y > playerRange.yMax + num) //当前的位置超过玩家的范围
            {
                v.y = 0f;
                body.velocity = v;
                body.position = new Vector2(x, y);
            }
        }

        //区域内的Y
        float yMin = GameArea.MapRange.yMin;
        float yMax = GameArea.MapRange.yMax;
        Vector3 position2 = transform.position;
        if (yMin > position2.y)//玩家往下的Y超过了区域的左下角的Y
        {
            position2.y = yMax;//设置自己的y为右上角的点的Y
            transform.position = position2;
            $"{transform.name}击穿了地面".Log();
        }
    }

    public LimitType type = LimitType.Enemy;
    public Vector2 bodySize = new Vector2(0.5f, 0f);
    public int battleZoneId = 1;
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