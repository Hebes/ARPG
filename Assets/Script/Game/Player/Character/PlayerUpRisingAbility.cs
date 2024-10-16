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
            if (R.Player.Enhancement.Combo1 != 0 && StateMachine.currentState.IsInArray(_canUpRisingSta))
            {
                //上挑特殊转向处理,因为动画不是朝右边的
                Vector3 temp = Listener.transform.localScale;
                temp.x = -1;
                Listener.transform.localScale = temp;
                //下面的不是
                weapon.HandleUpRising();
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