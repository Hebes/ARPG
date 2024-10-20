using UnityEngine;

/// <summary>
/// 玩家攻击能力
/// </summary>
public class PlayerAttackAbility : CharacterState
{
    private bool _pressRelease = true;

    public override void Update()
    {
        if (Listener.airAtkDown && Attribute.isOnGround)
        {
            Listener.airAtkDown = false;
            Listener.PhysicReset();
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
            if (weapon.canChangeAtkAnim)
            {
                weapon.cirtAttack = cirtAtk;
                weapon.CanPlayNextAttack();
            }
            else
            {
                weapon.HandleAttack(cirtAtk);
            }

            return;
        }

        //空中攻击
        if (StateMachine.currentState.IsInArray(PlayerAction.AirAttackSta))
        {
            if (weapon.canChangeAirAtkAnim)
            {
                weapon.cirtAttack = cirtAtk;
                weapon.CanPlayNextAirAttack();
            }
            else
            {
                weapon.HandleAirAttack(cirtAtk);
            }

            return;
        }

        if (TimeController.isPause) return;
        if (StateMachine.currentState.IsInArray(CanAttackSta) || Action.canChangeAnim)
        {
            if (attackDir != 3)
                Action.TurnRound(attackDir);

            Listener.PhysicReset();
            if (Attribute.isOnGround)
            {
                TimeController.SetSpeed(Vector2.zero);
                weapon.HandleAttack(cirtAtk);
            }
            else
            {
                Listener.checkFallDown = false;
                weapon.HandleAirAttack(cirtAtk);
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
        if ( StateMachine.IsSta(CanAttackSta) && _pressRelease)
        {
        	if (attackDir != 3)
        	{
        		Action.TurnRound(attackDir);
        	}
        	Listener.PhysicReset();
        	R.Player.TimeController.SetSpeed(Vector2.zero);
        	if (StateMachine.IsSta(PlayerAction.NormalSta))
        	{
        		R.Player.TimeController.SetSpeed(Vector2.zero);
        		weapon.CirtAttackHold();
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
            weapon.AirAttackRecover();
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