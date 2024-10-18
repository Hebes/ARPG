using System.Collections;
using UnityEngine;

/// <summary>
/// 玩家击地技能
/// </summary>
public class PlayerHitGroundAbility : CharacterState
{
    public override void Start()
    {
        _atkBox = Action.GetComponentInChildren<PlayerAtk>().transform;
        _platform = R.Player.GetComponent<PlatformMovement>();
    }

    public override void Update()
    {
        if (Attribute.isDead) return; //死亡
        if (TimeController.isPause) return; //暂停
        if (Listener.checkHitGround && Attribute.isOnGround) //站在地面或者击中地面
        {
            TimeController.SetSpeed(Vector2.zero);
            Listener.PhysicReset();
            Listener.checkHitGround = false;
            Vector3 position = new Vector3(0f, _platform.GetDistanceToGround(), 0f);
            R.Effect.Generate(22, Action.transform, position, Vector3.zero);
            PlayerStaEnum currentState = StateMachine.currentState.ToEnum<PlayerStaEnum>();
            switch (currentState)
            {
                case PlayerStaEnum.HitGrounding:
                    R.Player.AnimEvent.CameraEffect(0, 0.2f, 0.1f, ShakeTypeEnum.Rect, 0.2f);
                    Action.ChangeState(PlayerStaEnum.HitGroundEnd);
                    break;
                // case "Roll":
                // case "DahalRoll":
                //     _atkBox.localScale = Vector2.zero;
                //     //pac.ChangeState(PlayerAction.StateEnum.RollEnd);
                //     break;
                // case "HitGround":
                //     //pac.ChangeState(PlayerAction.StateEnum.HitGround2);
                //     break;
                // case "RollEndFrame":
                //     //pac.ChangeState(PlayerAction.StateEnum.RollFrameEnd);
                //     break;
                // case "QTEHitGround":
                //     //pac.ChangeState(PlayerAction.StateEnum.QTEHitGround2);
                //     break;
                // case "NewExecuteAir1_2":
                //     SingletonMono<CameraController>.Instance.KillTweening();
                //     SingletonMono<CameraController>.Instance.CloseMotionBlur();
                //     StartCoroutine(ExecuteHitGround());
                //     break;
                // case "QTERoll":
                //     //pac.ChangeState(PlayerAction.StateEnum.QTERollEnd);
                //     break;
            }
        }
    }

    /// <summary>
    /// 执行攻击地面
    /// </summary>
    /// <returns></returns>
    private IEnumerator ExecuteHitGround()
    {
        yield return new WaitForSeconds(0.05f);
        Listener.StartExecute();
    }

    /// <summary>
    /// 攻击地面
    /// </summary>
    public void HitGround()
    {
        if (TimeController.isPause) return;
        if (!Attribute.isOnGround
            && StateMachine.currentState.IsInArray(CanHitGroundSta)
            && R.Player.Enhancement.HitGround != 0)
        {
            nameof(Listener.FlashPositionSet).StopIEnumerator();
            if (Action.tempDir != 3)
                Action.TurnRound(Action.tempDir);
            Action.ChangeState(PlayerStaEnum.HitGroundStart);
        }
    }

    public override void OnStateTransfer(object sender, TransferEventArgs args)
    {
        if (args.nextState == PlayerStaEnum.AirAtk3.ToString()) //"Roll"
        {
            Vector2 speed = new Vector2(4 * Attribute.faceDir, 0f);
            Listener.AirPhysic(0.8f);
            R.Player.TimeController.SetSpeed(speed);
        }
    }

    /// <summary>
    /// 能够攻击地面的状态
    /// </summary>
    private static readonly string[] CanHitGroundSta =
    {
        PlayerStaEnum.Fall1.ToString(),
        PlayerStaEnum.Flash.ToString(),
        PlayerStaEnum.Flash2.ToString(),
        PlayerStaEnum.FlashEnd.ToString(),
        PlayerStaEnum.AirAtk1.ToString(),
        PlayerStaEnum.AirAtk2.ToString(),
        PlayerStaEnum.AirAtk3.ToString(),
        PlayerStaEnum.Jump.ToString(),
        PlayerStaEnum.Jumping.ToString(),
        PlayerStaEnum.JumpBack.ToString(),
    };

    private Transform _atkBox;
    private PlatformMovement _platform;
}