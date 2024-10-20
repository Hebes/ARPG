using UnityEngine;

/// <summary>
/// 玩家移动能力->这里的移动只负责转向
/// </summary>
public class PlayerMoveAbility : CharacterState
{
    [Header("移动停止计数器")] private int moveStopCounter;
    [Header("空气摩擦力")] private float airFric = 8f;
    [Header("附加摩擦力")] private float extraFric = 60f;
    public Vector2 addSpeed = Vector2.zero;

    public override void Update()
    {
        moveStopCounter--;
        if (moveStopCounter == 0)
            Move(0);
    }

    public override void FixedUpdate()
    {
        if ((StateMachine.currentState.IsInArray(PlayerAction.JumpSta) || StateMachine.currentState.IsInArray(PlayerAction.FlySta)) && !Attribute.isOnGround)
            DealAirFric(Attribute.moveSpeed, true);
        if (StateMachine.currentState.IsInArray(PlayerAction.UpRisingSta) && !Attribute.isOnGround)
            DealAirFric(Attribute.moveSpeed / 2f, false);
        if (StateMachine.currentState == "BladeStorm")
            DealAirFric(Attribute.moveSpeed / 4f, false);
    }

    /// <summary>
    /// 处理空中的摩擦力
    /// </summary>
    /// <param name="maxSpeed"></param>
    /// <param name="canTurn"></param>
    private void DealAirFric(float maxSpeed, bool canTurn)
    {
        Vector2 currentSpeed = TimeController.GetCurrentSpeed();
        //X轴
        float num = currentSpeed.x;
        num = Mathf.Clamp(Mathf.Abs(num) - airFric * Time.fixedDeltaTime, 0f, float.MaxValue) * Mathf.Sign(num);
        if (Input.Game.MoveLeft.Pressed || Input.Game.MoveRight.Pressed)
        {
            int num2 = Input.Game.MoveLeft.Pressed ? -1 : 1;
            int num3 = Input.Game.MoveLeft.Pressed ? -1 : 1;
            if (num2 != Attribute.faceDir && canTurn)
                Action.TurnRound(num2); //转向
            num += num3 * extraFric * Time.fixedDeltaTime;
        }

        currentSpeed.x = Mathf.Clamp(Mathf.Abs(num) - airFric * Time.fixedDeltaTime, 0f, maxSpeed) * Mathf.Sign(num);
        if (!Attribute.isOnGround)
        {
            Vector2 pointA = Action.transform.position + new Vector3(0.5f * Attribute.faceDir, 0f, 0f);
            Vector2 pointB = Action.transform.position + new Vector3(0.6f * Attribute.faceDir, 2.2f, 0f);
            Collider2D[] array = Physics2D.OverlapAreaAll(pointA, pointB, LayerManager.WallMask);
            int num4 = 0;
            for (var i = 0; i < array.Length; i++)
            {
                int layer = array[i].gameObject.layer;
                if (layer == LayerManager.WallLayerID || layer == LayerManager.GroundLayerID)
                    num4++;
            }

            if (num4 > 0)
            {
                currentSpeed.x = Mathf.Sign(currentSpeed.x) * -1f * 0.1f;
            }

            num4 = 0;
            for (int j = 0; j < array.Length; j++)
            {
                if (array[j].gameObject.layer == LayerManager.CeilingLayerID)
                    num4++;
            }

            if (num4 > 0 && currentSpeed.y > 0f)
                currentSpeed.y = 0f;
        }

        TimeController.SetSpeed(currentSpeed);
    }

    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="dir">方向</param>
    public void Move(int dir)
    {
        if (Attribute.isDead) return;
        if (dir == 0)
        {
            Action.tempDir = 3;
            if (StateMachine.currentState.IsInArray(PlayerAction.RunSta))
            {
                TimeController.SetSpeed(Vector2.zero);
                Action.ChangeState(PlayerStaEnum.RunSlow);
            }

            if (StateMachine.currentState.IsInArray(PlayerAction.AttackSta))
                TimeController.SetSpeed(Vector2.zero);
        }
        else
        {
            Action.tempDir = dir;
            if (StateMachine.currentState.IsInArray(PlayerAction.NormalSta) || Action.canChangeAnim)
                PlayerMove(dir == -1, Attribute.moveSpeed, Vector2.zero);
            if (StateMachine.currentState.IsInArray(PlayerAction.AttackSta) && dir == Attribute.faceDir)
                PlayerMove(dir == -1, Attribute.moveSpeed / 4f, addSpeed, false);
            if (StateMachine.currentState.IsInArray(PlayerAction.AirLightAttackSta) && dir == Attribute.faceDir)
                PlayerMove(dir == -1, Attribute.moveSpeed / 4f, addSpeed, false);
        }
    }

    /// <summary>
    /// 玩家移动
    /// </summary>
    /// <param name="isLeft">是否是左边</param>
    /// <param name="walkSpeed">行走速度</param>
    /// <param name="aSpeed">增加速度</param>
    /// <param name="playRun">播放跑</param>
    private void PlayerMove(bool isLeft, float walkSpeed, Vector2 aSpeed, bool playRun = true)
    {
        if (TimeController.isPause) return; //暂停跳过
        walkSpeed = AirWallCheck(walkSpeed);
        int num = isLeft ? -1 : 1;
        Vector2 vector = new Vector2(walkSpeed * num, TimeController.GetCurrentSpeed().y);
        vector += aSpeed;
        vector = EdgeCheck(vector);
        vector = SlopeCheck(vector);
        TimeController.SetSpeed(vector); //设置玩家移动
        if (playRun)
        {
            Action.ChangeState(PlayerStaEnum.Run); //切换状态
            Action.TurnRound(num); //更新朝向
        }

        //最大移动停止计数
        moveStopCounter = 4;
    }

    /// <summary>
    /// 空气壁检查
    /// </summary>
    /// <param name="walkSpeed"></param>
    /// <returns></returns>
    private float AirWallCheck(float walkSpeed)
    {
        Vector3 temp1 = Action.transform.position + new Vector3(0.5f * Attribute.faceDir, 0f, 0f);
        Vector3 temp2 = Action.transform.position + new Vector3(0.6f * Attribute.faceDir, 2.2f, 0f);
        Collider2D[] collider2Ds = Physics2D.OverlapAreaAll(temp1, temp2, LayerManager.GroundMask);
        if (!Attribute.isOnGround && collider2Ds.Length > 0)
            walkSpeed = 0f;
        return walkSpeed;
    }

    /// <summary>
    /// 边检查
    /// </summary>
    /// <param name="speed"></param>
    /// <returns></returns>
    private Vector2 EdgeCheck(Vector2 speed)
    {
        Vector3 position = Action.transform.position;//玩家的位置
        if (position.x >= GameArea.PlayerRange.max.x - Attribute.bounds.size.x / 2f)
            speed.x = speed.x <= 0f ? speed.x : 0f;
        if (position.x <= GameArea.PlayerRange.min.x + Attribute.bounds.size.x / 2f)
            speed.x = speed.x >= 0f ? speed.x : 0f;
        return speed;
    }

    /// <summary>
    /// 边坡检查
    /// </summary>
    /// <param name="speed"></param>
    /// <returns></returns>
    private Vector2 SlopeCheck(Vector2 speed)
    {
        if (StateMachine.IsSta(PlayerAction.NormalSta))
        {
            Vector2 groundNormal = R.Player.PlatformMovement.GetGroundNormal();
            Vector2 vector = VU.ProjectOnPlane(speed, groundNormal);;
            float d = Mathf.Clamp(Mathf.Abs(vector.x), Mathf.Abs(speed.x / 2f), Mathf.Abs(speed.x));
            speed = speed.y > 0f ? vector.normalized * d : vector;
        }

        return speed;
    }

    /// <summary>
    /// 状态切换
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public override void OnStateTransfer(object sender, TransferEventArgs args)
    {
        if (args.nextState.IsInArray(PlayerAction.NormalSta) && !args.lastState.IsInArray(PlayerAction.NormalSta))
        {
            GameEvent.Assessment.Trigger(new AssessmentEventArgs(AssessmentEventArgs.EventType.CurrentComboFinish));
        }
    }
}