using UnityEngine;

/// <summary>
/// 伤害数据工具
/// </summary>
public class HurtDataTools
{
    /// <summary>
    /// 反击
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="groundOnly"></param>
    /// <param name="actionInterrupt"></param>
    /// <param name="eAttr"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static bool Counterattack(int damage, bool groundOnly, ref bool actionInterrupt, ref EnemyAttribute eAttr, ref EnemyBaseAction action)
    {
        if (!eAttr.canCounterAttack) return false;
        if (damage == 0) return false;
        eAttr.currentCounterAttackValue += damage;
        bool flag = Random.Range(0, 100) < eAttr.currentCounterAttackProbPercentage;
        bool flag2 = eAttr.currentCounterAttackValue >= eAttr.counterAttack && flag && groundOnly && !action.IsInWeakSta();
        if (flag2)
        {
            eAttr.currentCounterAttackValue = 0;
            eAttr.currentCounterAttackProbPercentage = eAttr.counterAttackProbPercentage;
            eAttr.currentActionInterruptPoint = 0;
            actionInterrupt = false;
        }
        else
        {
            eAttr.currentCounterAttackProbPercentage += eAttr.counterAttackProbPercentage;
        }

        return flag2;
    }

    public static void AddActionInterruptPoint(int damage, string atkName, ref EnemyAttribute eAttr, ref bool actionInterrupt)
    {
        if (!eAttr.hasActionInterrupt)
        {
            return;
        }

        if (atkName.IsInArray(PlayerAtkType.LightAttack))
        {
            return;
        }

        int currentActionInterruptPoint = eAttr.currentActionInterruptPoint;
        eAttr.currentActionInterruptPoint += damage;
        actionInterrupt =
            (currentActionInterruptPoint < eAttr.actionInterruptPoint && eAttr.currentActionInterruptPoint >= eAttr.actionInterruptPoint);
    }

    /// <summary>
    /// 计算怪物是否可以防御
    /// </summary>
    /// <param name="damage">伤害</param>
    /// <param name="defenceTrigger">防御触发</param>
    /// <param name="action">行动</param>
    /// <param name="eAttr">敌人属性</param>
    /// <returns></returns>
    public static bool CalculateMonsterDefence(int damage, ref float defenceTrigger, ref EnemyBaseAction action, ref EnemyAttribute eAttr)
    {
        if (action.IsInWeakSta()) return false; //处于弱状态
        if (action.IsInDefenceState()) return false; //处于防卫状态
        if (eAttr.monsterDefence <= 0) return false; //怪物防御
        if (damage == 0) return false; //伤害0
        eAttr.currentDefence += damage; //累计伤害
        eAttr.startDefence = eAttr.currentDefence >= eAttr.monsterDefence; //是否开始防御
        bool flag = eAttr.startDefence && eAttr.isOnGround; //开始防御并在地上
        bool flag2 = false;
        if (flag)
        {
            flag2 = (Random.Range(0, 100) < defenceTrigger);
            if (!flag2)
            {
                defenceTrigger += 20f;
            }
        }

        return flag && flag2;
    }

    /// <summary>
    /// 计算怪物回避
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="sideStepTrigger"></param>
    /// <param name="action"></param>
    /// <param name="eAttr"></param>
    /// <returns></returns>
    public static bool CalculateMonsterSideStep(int damage, ref float sideStepTrigger, ref EnemyBaseAction action, ref EnemyAttribute eAttr)
    {
        if (action.IsInWeakSta()) return false;
        if (eAttr.monsterSideStep <= 0) return false;
        if (damage == 0) return false;

        eAttr.currentSideStep += damage;
        bool flag = true;
        if (!eAttr.sideStepInAir)
        {
            flag = eAttr.isOnGround;
        }

        bool flag2 = eAttr.currentSideStep >= eAttr.monsterSideStep && flag;
        bool flag3 = false;
        if (flag2)
        {
            flag3 = (Random.Range(0, 100) < sideStepTrigger);
            if (!flag3)
            {
                sideStepTrigger += 20f;
            }
        }

        return flag2 && flag3;
    }

    /// <summary>
    /// 获得攻击等级
    /// </summary>
    /// <param name="atkName"></param>
    /// <returns></returns>
    public static int GetAtkLevel(string atkName)
    {
        if (atkName.IsInArray(PlayerAtkType.AirAttack))
            return R.Player.Enhancement.AirAttack;

        if (atkName.IsInArray(PlayerAtkType.AirAvatarAttack))
            return R.Player.Enhancement.AirAvatarAttack;

        if (atkName == PlayerAtkType.AirCombo1)
            return R.Player.Enhancement.AirCombo1;

        if (atkName == PlayerAtkType.AirCombo2)
            return R.Player.Enhancement.AirCombo2;

        if (atkName.IsInArray(PlayerAtkType.Attack))
            return R.Player.Enhancement.Attack;

        if (atkName.IsInArray(PlayerAtkType.AvatarAttack))
            return R.Player.Enhancement.AvatarAttack;

        if (atkName == PlayerAtkType.BladeStorm)
            return R.Player.Enhancement.BladeStorm;

        if (atkName == PlayerAtkType.Charging)
            return R.Player.Enhancement.Charging;

        if (PlayerAtkType.Chase == atkName)
            return R.Player.Enhancement.Chase;

        if (atkName == PlayerAtkType.Combo1)
            return R.Player.Enhancement.Combo1;

        if (atkName.IsInArray(PlayerAtkType.Combo2))
            return R.Player.Enhancement.Combo2;

        if (PlayerAtkType.HitGround == atkName)
            return R.Player.Enhancement.HitGround;

        if (PlayerAtkType.Knockout == atkName)
        {
            return R.Player.Enhancement.Knockout;
        }

        if (PlayerAtkType.ShadeAttack == atkName)
            return R.Player.Enhancement.ShadeAttack;

        if (atkName.IsInArray(PlayerAtkType.TripleAttack))
            return R.Player.Enhancement.TripleAttack;

        if (PlayerAtkType.UpperChop == atkName)
            return R.Player.Enhancement.UpperChop;

        return 0;
    }

    /// <summary>
    /// 是否可以追逐攻击
    /// </summary>
    /// <returns></returns>
    public static bool ChaseAttack()
    {
        int num = UnityEngine.Random.Range(0, 100);
        return num <= 4 && R.Player.Enhancement.Chase > 0;
    }

    public static void FlashHPRecover()
    {
        float num = 0f;
        int flashAttack = R.Player.Enhancement.FlashAttack;

        switch (flashAttack)
        {
            case 1:
                num = 0.03f;
                break;
            case 2:
                num = 0.05f;
                break;
            case 3:
                num = 0.07f;
                break;
        }

        R.Player.Attribute.currentHP += (int)((float)R.Player.Attribute.maxHP * num);
    }
}