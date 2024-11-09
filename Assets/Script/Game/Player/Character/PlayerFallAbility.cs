using UnityEngine;

/// <summary>
/// 玩家落下能力
/// </summary>
public class PlayerFallAbility : CharacterState
{
    public override void Update()
    {
        base.Update();

        if (TimeController.isPause) return;

        //开始落下 下降并且当前速度小于3 
        if (AnimEvent.checkFallDown && TimeController.GetCurrentSpeed().y <= 3f)
        {
            AnimEvent.checkFallDown = false;
            Action.ChangeState(PlayerStaEnum.Fall1);
            AnimEvent.isFalling = true;
        }

        //正常落下
        if (AnimEvent.isFalling && Attribute.isOnGround)
        {
            AnimEvent.isFalling = false;
            AnimEvent.checkFallDown = false;
            AnimEvent.PhysicReset();
            AnimEvent.PlayerSound(21);
            Action.ChangeState(PlayerStaEnum.GetUp);
        }

        //受伤掉落
        if (AnimEvent.flyHitFlag && TimeController.GetCurrentSpeed().y <= 0f && StateMachine.currentState.IsInArray(PlayerAction.HurtSta))
        {
            AnimEvent.flyHitFlag = false;
            Action.ChangeState(PlayerStaEnum.UnderAtkFlyToFall);
            AnimEvent.flyHitGround = true;
        }

        //受伤掉落到地面
        if (AnimEvent.flyHitGround && Attribute.isOnGround)
        {
            AnimEvent.hitJump = false;
            AnimEvent.flyHitGround = false;
            AnimEvent.PhysicReset();
            Vector2 currentSpeed = TimeController.GetCurrentSpeed();
            currentSpeed.x /= 2f;
            TimeController.SetSpeed(currentSpeed);
            Action.ChangeState(PlayerStaEnum.UnderAtkHitGround); //受伤掉落地面的动画
        }

        AnimEvent.UpdateFlashEnd();
    }
}