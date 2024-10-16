using UnityEngine;

/// <summary>
/// https://blog.csdn.net/qq_56755504/article/details/131258547 重力参考为啥是-40
/// </summary>
public class WorldSetting : SMono<WorldSetting>
{
    private void Awake()
    {
        //设置世界重力
        Physics2D.gravity = new Vector2(0f, -40f);
    }
}