using UnityEngine;

/// <summary>
/// 悬崖，重生点
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Cliff : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(CTag.Player))
        {
            RebornPoint = transform;
        }
    }

    public static void Reset()
    {
        RebornPoint = null;
    }

    public static Transform RebornPoint;
}