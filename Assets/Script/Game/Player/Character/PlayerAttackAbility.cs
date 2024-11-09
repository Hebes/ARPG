using UnityEngine;

/// <summary>
/// 玩家攻击能力
/// </summary>
public class PlayerAttackAbility : CharacterState
{
    private bool _pressRelease = true;

    public override void Update()
    {
        if (AnimEvent.airAtkDown && Attribute.isOnGround)
        {
            AnimEvent.airAtkDown = false;
            AnimEvent.PhysicReset();
            TimeController.SetSpeed(Vector2.zero);
            "空中重攻击5".Log();
            //PlayerAction.ChangeState(PlayerAction.StateEnum.AirAtkHv5); //空中重攻击5
        }
    }

    /// <summary>
    /// 玩家攻击
    /// </summary>
    /// <param name="attackDir">玩家攻击的方向</param>
    /// <param name="cirtAtk">重攻击</param>
    public void PlayerAttack(int attackDir, bool cirtAtk)
    {
        //正常攻击
        if (StateMachine.currentState.IsInArray(PlayerAction.AttackSta))
        {
            "正常攻击".Log();
            if (Weapon.canChangeAtkAnim)
            {
                Weapon.cirtAttack = cirtAtk;
                Weapon.CanPlayNextAttack();
            }
            else
            {
                Weapon.HandleAttack(cirtAtk);
            }

            return;
        }

        //空中攻击
        if (StateMachine.currentState.IsInArray(PlayerAction.AirAttackSta))
        {
            if (Weapon.canChangeAirAtkAnim)
            {
                Weapon.cirtAttack = cirtAtk;
                Weapon.CanPlayNextAirAttack();
            }
            else
            {
                Weapon.HandleAirAttack(cirtAtk);
            }

            return;
        }

        if (TimeController.isPause) return;
        if (StateMachine.currentState.IsInArray(CanAttackSta) || Action.canChangeAnim)
        {
            if (attackDir != 3)
                Action.TurnRound(attackDir);

            AnimEvent.PhysicReset();
            if (Attribute.isOnGround)
            {
                TimeController.SetSpeed(Vector2.zero);
                Weapon.HandleAttack(cirtAtk);
            }
            else
            {
                AnimEvent.checkFallDown = false;
                Weapon.HandleAirAttack(cirtAtk);
            }
        }
    }

    /// <summary>
    /// 重攻击长安
    /// </summary>
    /// <param name="attackDir"></param>
    public void PlayerCirtPressAttack(int attackDir)
    {
        //"玩家按下重攻击".Error();
        if (TimeController.isPause)return;
        if ( StateMachine.currentState.IsInArray(CanAttackSta) && _pressRelease)
        {
        	if (attackDir != 3)
        	{
        		Action.TurnRound(attackDir);
        	}
        	AnimEvent.PhysicReset();
        	R.Player.TimeController.SetSpeed(Vector2.zero);
        	if (StateMachine.currentState.IsInArray(PlayerAction.NormalSta))
        	{
        		R.Player.TimeController.SetSpeed(Vector2.zero);
        		Weapon.CirtAttackHold();
        	}
            "空中重攻击".Log();
        	// else
        	// {
        	// 	weapon.AirCirtAttackHold();
        	// }
        	_pressRelease = false;
        }
    }

    public void PlayerCirtPressAttackReleasd()
    {
        _pressRelease = true;
    }

    public override void OnStateTransfer(object sender, TransferEventArgs args)
    {
        //playl listener.atkBox.localScale = new Vector3(0f, 0f, 1f);
        //Listener.atkBox.localPosition = Vector3.zero;
        if (args.lastState.IsInArray(PlayerAction.AirAttackSta) && !args.nextState.IsInArray(PlayerAction.AirAttackSta))
        {
            Weapon.AirAttackRecover();
        }
    }


    /// <summary>
    /// 能攻击的状态
    /// </summary>
    private static readonly string[] CanAttackSta =
    {
        PlayerStaEnum.Idle.ToString(),
        PlayerStaEnum.Run.ToString(),
        PlayerStaEnum.Jump.ToString(),
        PlayerStaEnum.Jumping.ToString(),
        PlayerStaEnum.Fall1.ToString(),
        PlayerStaEnum.Falling.ToString(),
    };
}