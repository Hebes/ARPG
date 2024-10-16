using UnityEngine;

/// <summary>
/// 伤害检查
/// </summary>
public class HurtCheck : MonoBehaviour
{
    [SerializeField] public HurtCheck.BodyType bodyType;

    public enum BodyType
    {
        Horn,
        Body,
        Tail
    }
}