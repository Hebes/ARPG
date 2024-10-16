using UnityEngine;

/// <summary>
/// 战斗区域触发
/// </summary>
public class BattleZoneTrigger : MonoBehaviour
{
    private void Awake()
    {
        GameEvent.EnemyHurtAtk.Register(EnterBattleModeByEnemyHurt);
        GameEvent.PlayerHurtAtk.Register(EnterBattleModeByPlayerHurt);
    }

    /// <summary>
    /// 通过玩家伤害进入战斗模式
    /// </summary>
    /// <param name="udata"></param>
    private void EnterBattleModeByPlayerHurt(object udata)
    {
        PlayerHurtAtkEventArgs args = (PlayerHurtAtkEventArgs)udata;
        "通过玩家伤害进入战斗模式".Log();
        if (R.Player.Transform.position.x < GameArea.MapRange.xMin + 3f ||
            R.Player.Transform.position.x > GameArea.MapRange.xMax - 3f)
        {
            return;
        }

        bool flag = false;
        EnemyBullet component = args.sender?.GetComponent<EnemyBullet>();
        if (component?.EnemyTypeOfShooter == EnemyType.空雷) flag = true; //敌人类型是空雷
        if (!R.Mode.CheckMode(Mode.AllMode.Battle) && !flag)
        {
            R.Mode.EnterMode(Mode.AllMode.Battle);
        }
    }

    /// <summary>
    /// 通过敌人受伤进入战斗模式
    /// </summary>
    /// <param name="udata"></param>
    /// <returns></returns>
    private void EnterBattleModeByEnemyHurt(object udata)
    {
        EnemyHurtAtkEventArgs args = (EnemyHurtAtkEventArgs)udata;
        "通过敌人受伤进入战斗模式".Log();
        if (!R.Mode.CheckMode(Mode.AllMode.Battle) &&
            args.hurted.GetComponent<SupplyBoxAction>() == null &&
            args.hurted.GetComponent<MusicPlayerAction>() == null)
        {
            R.Mode.EnterMode(Mode.AllMode.Battle);
        }
    }
}