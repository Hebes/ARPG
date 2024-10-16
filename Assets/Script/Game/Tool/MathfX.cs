using System.Collections.Generic;
using UnityEngine;

public static class MathfX
{
    /// <summary>
    /// 旋转矢量
    /// </summary>
    /// <param name="v"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static Vector3 RotateVector(Vector3 v, float angle)
    {
        return Quaternion.AngleAxis(angle, Vector3.back) * v;
    }

    /// <summary>
    /// 相交二维中心
    /// </summary>
    /// <param name="bounds1"></param>
    /// <param name="bounds2"></param>
    /// <returns></returns>
    public static Vector2 Intersect2DCenter(Bounds bounds1, Bounds bounds2)
    {
        float magnitude = bounds1.size.magnitude;
        float magnitude2 = bounds2.size.magnitude;
        Vector2 a = bounds2.center - bounds1.center;
        Vector2 a2 = a * magnitude / (magnitude + magnitude2);
        return a2 + (Vector2)bounds1.center;
    }

    /// <summary>
    /// 相交二维中心
    /// </summary>
    /// <param name="player"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static Vector2[] Intersect2DCenterMCM(Bounds player, Collider2D other)
    {
        float num = 0.1f;
        Bounds bounds = player;
        List<Vector2> list = new List<Vector2>();
        float x = bounds.min.x;
        float x2 = bounds.max.x;
        float y = bounds.min.y;
        float y2 = bounds.max.y;
        for (float num2 = x; num2 < x2; num2 += num)
        {
            for (float num3 = y; num3 < y2; num3 += num)
            {
                Vector2 vector = new Vector2(num2, num3);
                if (other.OverlapPoint(vector))
                {
                    list.Add(vector);
                }
            }
        }

        if (list.Count != 0)
        {
            return list.ToArray();
        }

        return new Vector2[]
        {
            MathfX.Intersect2DCenter(player, other.bounds)
        };
    }

    /// <summary>
    /// 在射程内
    /// </summary>
    /// <param name="x"></param>
    /// <param name="rangeMin"></param>
    /// <param name="rangeMax"></param>
    /// <returns></returns>
    public static bool isInRange(float x, float rangeMin, float rangeMax)
    {
        return x > rangeMin && x < rangeMax;
    }

    /// <summary>
    /// 在距离中
    /// </summary>
    /// <param name="x"></param>
    /// <param name="middle"></param>
    /// <param name="diameter"></param>
    /// <returns></returns>
    public static bool isInMiddleRange(float x, float middle, float diameter)
    {
        diameter = Mathf.Abs(diameter);
        return MathfX.isInRange(x, middle - diameter / 2f, middle + diameter / 2f);
    }

    /// <summary>
    /// 最近的对象在x轴上
    /// </summary>
    /// <param name="aim"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    public static GameObject NearestObjOnXAxis(GameObject aim, ICollection<GameObject> objects)
    {
        float num = float.MaxValue;
        GameObject result = null;
        foreach (GameObject gameObject in objects)
        {
            float num2 = Mathf.Abs(aim.transform.position.x - gameObject.transform.position.x);
            if (num2 < num)
            {
                num = num2;
                result = gameObject;
            }
        }

        return result;
    }

    /// <summary>
    /// 高度的位置
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static float PosToHeight(Vector3 pos)
    {
        return pos.y * 1.957f;
    }

    /// <summary>
    /// 高度的位置
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static float HeightToPos(float height)
    {
        return 0.511f * height;
    }
}