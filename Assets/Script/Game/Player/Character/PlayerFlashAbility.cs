using System;
using UnityEngine;

/// <summary>
/// 玩家闪现能力
/// </summary>
public class PlayerFlashAbility : CharacterState
{
    /// <summary>
    /// 在障碍上
    /// </summary>
    private bool IsOnObstacle
    {
        get
        {
            RaycastHit2D hit = Physics2D.Raycast(Action.transform.position + new Vector3(-0.45f, 0.4f, 0f), Vector2.down, 0.6f, LayerManager.ObstacleMask);
            RaycastHit2D hit2 = Physics2D.Raycast(Action.transform.position + new Vector3(0.45f, 0.4f, 0f), Vector2.down, 0.6f, LayerManager.ObstacleMask);
            return hit || hit2;
        }
    }

    /// <summary>
    /// 冷却时间
    /// </summary>
    private int CoolDown
    {
        get
        {
            if (!R.Mode.CheckMode(Mode.AllMode.Battle))
                return WorldTime.SecondToFrame(0.5f);
            //游戏难度
            switch (R.GameData.Difficulty)
            {
                case 0: return WorldTime.SecondToFrame(1.5f);
                case 1: return WorldTime.SecondToFrame(2.5f);
                case 2: return WorldTime.SecondToFrame(3f);
                default: return WorldTime.SecondToFrame(1.5f);
            }
        }
    }

    public override void Update()
    {
        if (Attribute.isDead) return;
        //更新闪的次数
        if (Attribute.currentFlashTimes < Attribute.flashTimes)
        {
            Attribute.FlashCd++;
            if (Attribute.FlashCd >= CoolDown)
            {
                Attribute.FlashCd = 0;
                Attribute.currentFlashTimes = Mathf.Clamp(Attribute.currentFlashTimes + 1, 0, Attribute.flashTimes);
            }
        }
    }

    public void FlashFace() => Swipe(R.Player.Attribute.faceDir);
    public void FlashRight() => Swipe(1);
    public void FlashLeft() => Swipe(-1);
    public void FlashUp() => Swipe(2);
    public void FlashRightUp() => Swipe(4);
    public void FlashRightDown() => Swipe(Attribute.isOnGround ? 1 : -4);
    public void FlashLeftUp() => Swipe(5);
    public void FlashLeftDown() => Swipe(Attribute.isOnGround ? -1 : -5);

    public void FlashDown()
    {
        if (FlashOnObstacle())
        {
            Swipe(-2);
            return;
        }

        if (Attribute.isOnGround) return;
        Swipe(-2);
    }

    /// <summary>
    /// 是否可以闪
    /// </summary>
    /// <returns></returns>
    private bool FlashLevelCheck() => Attribute.currentFlashTimes > 0;

    /// <summary>
    /// 障碍闪
    /// </summary>
    /// <returns></returns>
    private bool FlashOnObstacle()
    {
        bool flag = R.Player.Attribute.flashLevel == 3;
        return flag && IsOnObstacle;
    }

    /// <summary>
    /// 闪 
    /// </summary>
    /// <param name="dir"></param>
    private void Swipe(int dir)
    {
        if (R.Player.TimeController.isPause) return;
        if ((StateMachine.currentState.IsInArray(CanFlashSta) || Action.canChangeAnim) && FlashLevelCheck())
        {
            R.StopIEnumerator(nameof(AnimEvent.FlashPositionSet));
            if (dir is 1 or -4 or 4)
            {
                Action.TurnRound(1);
            }

            if (dir is -1 or -5 or 5)
            {
                Action.TurnRound(-1);
            }

            AnimEvent.flashDir = dir;
            switch (dir + 5)
            {
                case 0:
                case 1:
                    //Action.ChangeState(EnemyStaEnum.FlashDown45_1);
                    break;
                case 3:
                    // pac.ChangeState(PlayerAction.StateEnum.FlashDown1);
                    break;
                case 4:
                case 6: //1 玩家右边  -1 玩家左边
                    Action.ChangeState(PlayerStaEnum.Flash);
                    break;
                case 7:
                    //pac.ChangeState(PlayerAction.StateEnum.FlashUp1);
                    break;
                case 9:
                case 10:
                    //pac.ChangeState(PlayerAction.StateEnum.FlashUp45_1);
                    break;
            }

            GameEvent.Assessment.Trigger(new AssessmentEventArgs(AssessmentEventArgs.EventType.CurrentComboFinish));
            StateInit();
            AnimEvent.FlashStart();
            Attribute.currentFlashTimes = Mathf.Clamp(Attribute.currentFlashTimes - 1, 0, Attribute.flashTimes);
            //R.Ui.Flash.OnFlash(PAttr.currentFlashTimes);
        }
    }

    private void StateInit()
    {
        AnimEvent.isFalling = false;
        AnimEvent.airAtkDown = false;
        AnimEvent.checkFallDown = false;
        AnimEvent.checkHitGround = false;
        TimeController.SetSpeed(Vector2.zero);
        AnimEvent.AirPhysic(0f);
    }

    /// <summary>
    /// 状态机状态转换
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public override void OnStateTransfer(object sender, TransferEventArgs args)
    {
        Attribute.flashFlag = false;
        ParticleSystem.EmissionModule emission = Action.blockPartical.emission;
        if (args.nextState.IsInArray(PlayerAction.FlashAttackSta) && Math.Abs(emission.rateOverDistance.constant - 10f) > 1.401298E-45f)
            emission.rateOverDistance = 10f;

        if (!args.nextState.IsInArray(PlayerAction.FlashAttackSta) && Math.Abs(emission.rateOverDistance.constant) > 1.401298E-45f)
            emission.rateOverDistance = 0f; //设置粒子系统在距离上的发射速率
    }

    /// <summary>
    /// 闪一次
    /// </summary>
    // public void FlashOnce()
    // {
    //     listener.StopIEnumerator("FlashPositionSet");
    //     listener.flashDir = PAttr.faceDir;
    //     pac.ChangeState(PlayerAction.StateEnum.Flash1);
    //     StateInit();
    // }
    private static readonly string[] CanFlashSta =
    {
        PlayerStaEnum.Idle.ToString(), //等待1
        PlayerStaEnum.Idle2.ToString(), //等待2 卖萌
        PlayerStaEnum.Idle3.ToString(), //等待3
        PlayerStaEnum.Run.ToString(), //跑
        
        PlayerStaEnum.Fall1.ToString(), //落下
        PlayerStaEnum.Falling.ToString(), //落下中
        PlayerStaEnum.FlashEnd.ToString(), //落下中
        
        PlayerStaEnum.AirAtk1.ToString(), //空中攻击1
        PlayerStaEnum.AirAtk2.ToString(), //空中攻击2
        PlayerStaEnum.AirAtk3.ToString(), //空中攻击3
        
        PlayerStaEnum.Atk1.ToString(), //攻击1轻攻击类型1
        PlayerStaEnum.Atk2.ToString(), //攻击2轻攻击类型2
        PlayerStaEnum.Atk3.ToString(), //攻击3轻攻击类型3
        PlayerStaEnum.Atk4.ToString(), //攻击4一般是重攻击
        PlayerStaEnum.AtkRemote.ToString(), //远程攻击
        
        PlayerStaEnum.Jump.ToString(), //跳跃
        PlayerStaEnum.Jumping.ToString(), //跳跃中
        PlayerStaEnum.JumpBack.ToString(), //往后跳
    };
}