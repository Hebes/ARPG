using UnityEngine;

/// <summary>
/// 玩家上挑
/// </summary>
public class PlayerUpRisingAbility : CharacterState
{
    public void UpJumpAttack()
    {
        if (TimeController.isPause) return;
        if (Attribute.isOnGround)
        {
            R.Player.TimeController.SetSpeed(Vector2.zero);
            if (R.Player.Enhancement.Combo1 != 0 && StateMachine.currentState.IsInArray(_canUpRisingSta))//上挑
            {
                R.Player.AnimEvent.PhysicReset();
                Weapon. AirAttackReset();
                Action.ChangeState(PlayerStaEnum.UpRising);
                Weapon.HandleUpRising();
            }
        }
    }

    /// <summary>
    /// 能够上升的状态
    /// </summary>
    private readonly string[] _canUpRisingSta =
    {
        PlayerStaEnum.Atk1.ToString(),
        PlayerStaEnum.Atk2.ToString(),
        PlayerStaEnum.Atk3.ToString(),
        PlayerStaEnum.Atk4.ToString(),
        PlayerStaEnum.GetUp.ToString(),
        PlayerStaEnum.Idle.ToString(),
        PlayerStaEnum.Run.ToString(),
        PlayerStaEnum.RunSlow.ToString(),
        "Run",
        "RunSlow",
        "IdleToDefense",
        "Defense",
        "FallToDefenseAir",
        "DefenseAir",
        "ExecuteToIdle",
        "FlashGround"
    };
}