using System.Collections;
using UnityEngine;

/// <summary>
/// 悬崖伤害
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class CliffHurt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ConfigTag.Player))
        {
            StartCoroutine(PlayerReset());
        }
    }

    private IEnumerator PlayerReset()
    {
        yield return new WaitForSeconds(delayTime);
        PlayerAttribute pAttr = R.Player.Attribute;
        DamgeType damgeType = damageType;
        if (damgeType != DamgeType.Constant)
        {
            if (damgeType == DamgeType.Percent)
            {
                pAttr.currentHP -= (int)(pAttr.maxHP * percent / 100f);
            }
        }
        else
        {
            pAttr.currentHP -= damage;
        }

        if (!pAttr.isDead)
        {
            R.Player.GetComponent<Rigidbody2D>().gravityScale = 0f;
            R.Player.Action.ChangeState(PlayerStaEnum.UnderAtk1, 1f);
            yield return new WaitForSeconds(0.2f);
            R.Player.Transform.position = Cliff.RebornPoint.position + Vector3.up * 3f;
        }
    }

    [Header("伤害类型")] [SerializeField] private DamgeType damageType;
    [Header("延迟时间")] [SerializeField] private float delayTime;
    [Header("伤害")] [SerializeField] private int damage;
    [Header("百分比伤害")] [SerializeField] private int percent;


    /// <summary>
    /// 伤害类型
    /// </summary>
    public enum DamgeType
    {
        /// <summary>
        /// 持续伤害
        /// </summary>
        Constant,

        /// <summary>
        /// 百分比伤害
        /// </summary>
        Percent
    }
}