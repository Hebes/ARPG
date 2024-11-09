using UnityEngine;

/// <summary>
/// 玩家跳跃能力
/// </summary>
public class PlayerJumpAbility : CharacterState
{
    /// <summary>
    /// 是否是第一次跳
    /// </summary>
    private bool _firstJumped = false;

    /// <summary>
    /// 是否第二次跳跃
    /// </summary>
    private bool _secondJumpped;

    /// <summary>
    /// 无敌时间
    /// </summary>
    private int _invincibleRecover;

    public override void Update()
    {
        if (_invincibleRecover > 0)
        {
            _invincibleRecover--;
            if (_invincibleRecover <= 0)
                Abilities.Hurt.Invincible = false;
        }
    }

    public void Jump()
    {
        if (TimeController.isPause) return; //是否暂停
        //第一次跳跃
        if ((StateMachine.currentState.IsInArray(CanJumpSta) || Action.canChangeAnim) && !_firstJumped)
        {
            _firstJumped = true;
            AnimEvent.PhysicReset();
            StateCheck();
            Action.ChangeState(PlayerStaEnum.Jump);
            AnimEvent.FallDown();
            Vector2 currentSpeed = TimeController.GetCurrentSpeed();
            currentSpeed.y = 15f; //跳跃的高度15
            TimeController.SetSpeed(currentSpeed);
            return;
        }

        //第二次跳跃
        if ((StateMachine.currentState.IsInArray(SecondJumpSta) && _firstJumped && !_secondJumpped) ||
            (StateMachine.currentState.IsInArray(HurtJumpSta) && AnimEvent.hitJump))
        {
            if (AnimEvent.hitJump && StateMachine.currentState.IsInArray(HurtJumpSta))
            {
                JumpEffect();
                AnimEvent.hitJump = false;
                AnimEvent.flyHitFlag = false;
                AnimEvent.flyHitGround = false;
                Abilities.Hurt.Invincible = true;
                _invincibleRecover = WorldTime.SecondToFrame(0.3f);
            }

            _firstJumped = true;
            _secondJumpped = true;
            AnimEvent.PhysicReset();
            StateCheck();
            Action.ChangeState(PlayerStaEnum.Jump);
            AnimEvent.FallDown();
            Vector2 currentSpeed2 = TimeController.GetCurrentSpeed();
            currentSpeed2.y = 10f;
            TimeController.SetSpeed(currentSpeed2);
        }
    }

    /// <summary>
    /// 状态检查
    /// </summary>
    private void StateCheck()
    {
        AnimEvent.checkFallDown = false;
        AnimEvent.isFalling = false;
        AnimEvent.airAtkDown = false;
    }

    private void JumpEffect()
    {
        Transform transform = R.Effect.Generate(212, null, R.Player.Transform.position);
        transform.localScale = R.Player.Transform.localScale;
    }

    public override void OnStateTransfer(object sender, TransferEventArgs args)
    {
        if (args.lastState.IsInArray(PlayerAction.NormalSta)) return;
        if (!args.nextState.IsInArray(PlayerAction.NormalSta)) return;
        _firstJumped = false;
        _secondJumpped = false;
    }

    /// <summary>
    /// 能否跳跃的状态
    /// </summary>
    private static readonly string[] CanJumpSta =
    {
        PlayerStaEnum.Atk1.ToString(),
        PlayerStaEnum.Atk2.ToString(),
        PlayerStaEnum.Atk3.ToString(),
        PlayerStaEnum.Atk4.ToString(),
        PlayerStaEnum.AtkRemote.ToString(),
        PlayerStaEnum.Idle.ToString(),
        PlayerStaEnum.Run.ToString(),
        PlayerStaEnum.Fall1.ToString(),
    };

    /// <summary>
    /// 第二次可以跳跃的状态
    /// </summary>
    private static readonly string[] SecondJumpSta =
    {
        PlayerStaEnum.Jump.ToString(),
        PlayerStaEnum.Jumping.ToString(),
        PlayerStaEnum.Fall1.ToString(),
        PlayerStaEnum.Falling.ToString(),
    };

    /// <summary>
    /// 受伤是否可以跳跃的状态
    /// </summary>
    private static readonly string[] HurtJumpSta =
    {
        "UnderAtkFlyToFall",
        "UnderAtkHitGround",
        "UnderAtkHitToFly"
    };
}