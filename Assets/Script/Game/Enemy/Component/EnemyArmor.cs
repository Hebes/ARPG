using UnityEngine;

/// <summary>
/// 敌人盔甲
/// </summary>
public abstract class EnemyArmor : MonoBehaviour
{
    private void Awake()
    {
        eAttr = GetComponent<EnemyAttribute>();
    }

    public abstract void HitArmor(int damage, string data);

    public abstract void Break();

    protected EnemyAttribute eAttr;
}