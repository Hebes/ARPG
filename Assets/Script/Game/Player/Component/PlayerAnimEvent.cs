using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 玩家动画事件
/// </summary>
public class PlayerAnimEvent : MonoBehaviour
{
    private StateMachine _stateMachine;
    private PlayerAction _playerAction;
    private PlayerAttribute _attribute;
    private PlayerAtk _playerAtk;
    private PlayerTimeController _playerTimeController;
    private Claymore _claymore;
    private OnionCreator onion;

    private void Awake()
    {
        _claymore = R.Player.GetComponent<Claymore>();
        Animator animator = GetComponent<Animator>();

        animator.UnAnimatorAddEventAll();

        animator.AddAnimatorEvent(PlayerStaEnum.Atk1.ToString(), 0, nameof(PlayEffect), 98);
        animator.AddAnimatorEvent(PlayerStaEnum.Atk1.ToString(), 0, nameof(SetAtkData));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk1.ToString(), 0, nameof(SetAttackId));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk1.ToString(), 0, nameof(PlayAtkSound));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk1.ToString(), 7, nameof(CanPlayNextAttack));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk1.ToString(), 9, nameof(AttackFinish));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk1.ToString(), 12, nameof(PlayAnim), PlayerStaEnum.Idle.ToString());

        animator.AddAnimatorEvent(PlayerStaEnum.Atk2.ToString(), 0, nameof(PlayEffect), 99);
        animator.AddAnimatorEvent(PlayerStaEnum.Atk2.ToString(), 0, nameof(SetAtkData));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk2.ToString(), 0, nameof(SetAttackId));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk2.ToString(), 0, nameof(PlayAtkSound));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk2.ToString(), 7, nameof(CanPlayNextAttack));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk2.ToString(), 9, nameof(AttackFinish));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk2.ToString(), 11, nameof(PlayAnim), PlayerStaEnum.Idle.ToString());

        animator.AddAnimatorEvent(PlayerStaEnum.Atk3.ToString(), 0, nameof(PlayEffect), 98);
        animator.AddAnimatorEvent(PlayerStaEnum.Atk3.ToString(), 0, nameof(SetAtkData));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk3.ToString(), 0, nameof(SetAttackId));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk3.ToString(), 0, nameof(PlayAtkSound));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk3.ToString(), 8, nameof(CanPlayNextAttack));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk3.ToString(), 10, nameof(AttackFinish));
        animator.AddAnimatorEvent(PlayerStaEnum.Atk3.ToString(), 14, nameof(PlayAnim), PlayerStaEnum.Idle.ToString());

        animator.AddAnimatorEvent(PlayerStaEnum.Flash.ToString(), 0, nameof(SetAtkData));
        animator.AddAnimatorEvent(PlayerStaEnum.Flash.ToString(), 0, nameof(SetAttackId));
        animator.AddAnimatorEvent(PlayerStaEnum.Flash.ToString(), 0, nameof(OpenOnion)); //打开幻影
        animator.AddAnimatorEvent(PlayerStaEnum.Flash.ToString(), 1, nameof(PlayFlashSound));
        animator.AddAnimatorEvent(PlayerStaEnum.Flash.ToString(), 3, nameof(FlashPositionSet));

        animator.AddAnimatorEvent(PlayerStaEnum.Flash2.ToString(), 4, nameof(PlayAnim), PlayerStaEnum.Fall1.ToString());

        animator.AddAnimatorEvent(PlayerStaEnum.FlashEnd.ToString(), 1, nameof(CanChangeState));
        animator.AddAnimatorEvent(PlayerStaEnum.FlashEnd.ToString(), 2, nameof(PlayAnim), PlayerStaEnum.Idle.ToString());

        animator.AddAnimatorEvent(PlayerStaEnum.Hurt.ToString(), 0, nameof(_claymore.AirAttackReset));
        animator.AddAnimatorEvent(PlayerStaEnum.Hurt.ToString(), 2, nameof(PlayHurtSoundLight));
        animator.AddAnimatorEvent(PlayerStaEnum.Hurt.ToString(), 5, nameof(PlayAnim), PlayerStaEnum.Idle.ToString());

        animator.AddAnimatorEvent(PlayerStaEnum.Run.ToString(), 0, nameof(PlayRunSound));
        animator.AddAnimatorEvent(PlayerStaEnum.Run.ToString(), 3, nameof(PlayRunSound));
        animator.AddAnimatorEvent(PlayerStaEnum.Run.ToString(), 6, nameof(PlayRunSound));

        animator.AddAnimatorEvent(PlayerStaEnum.RunSlow.ToString(), 2, nameof(PlayAnim), PlayerStaEnum.Idle.ToString());

        animator.AddAnimatorEvent(PlayerStaEnum.Jump.ToString(), 0, nameof(PlayerSound), 22);

        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk1.ToString(), 0, nameof(PlayEffect), 103);
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk1.ToString(), 0, nameof(AirPhysic), 0);
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk1.ToString(), 5, nameof(SetAtkData));
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk1.ToString(), 5, nameof(SetAttackId));
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk1.ToString(), 5, nameof(PlayerSound), 28);
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk1.ToString(), 7, nameof(PlayShoutSoundLight));
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk1.ToString(), 7, nameof(CanPlayNextAirAttack));
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk1.ToString(), 10, nameof(AirAttackFinish));

        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk2.ToString(), 0, nameof(SetAtkData));
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk2.ToString(), 0, nameof(PlayEffect), 104);
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk2.ToString(), 0, nameof(AirPhysic), 0);
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk2.ToString(), 0, nameof(SetAttackId));
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk2.ToString(), 1, nameof(PlayerSound), 29);
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk2.ToString(), 4, nameof(PlayShoutSoundLight));
        //animator.AddAnimatorEvent(PlayerStaEnum.AirAtk2, 4, nameof(CanPlayNextAirAttack));
        animator.AddAnimatorEvent(PlayerStaEnum.AirAtk2.ToString(), 7, nameof(AirAttackFinish));

        animator.AddAnimatorEvent(PlayerStaEnum.Fall1.ToString(), 0, nameof(Falling));
        animator.AddAnimatorEvent(PlayerStaEnum.Fall1.ToString(), 0, nameof(PhysicReset));

        //起立
        animator.AddAnimatorEvent(PlayerStaEnum.GetUp.ToString(), 0, nameof(FlashReset));
        animator.AddAnimatorEvent(PlayerStaEnum.GetUp.ToString(), 0, nameof(Speed), "{\"x\":0,\"y\":0}");
        animator.AddAnimatorEvent(PlayerStaEnum.GetUp.ToString(), 0, nameof(AirAttackReset));
        animator.AddAnimatorEvent(PlayerStaEnum.GetUp.ToString(), 5, nameof(PlayAnim), PlayerStaEnum.Idle.ToString());

        animator.AddAnimatorEvent(PlayerStaEnum.UpRising.ToString(), 0, nameof(AirAttackReset));
        animator.AddAnimatorEvent(PlayerStaEnum.UpRising.ToString(), 0, nameof(PhysicReset));
        animator.AddAnimatorEvent(PlayerStaEnum.UpRising.ToString(), 0, nameof(PlayEffect), 106);
        animator.AddAnimatorEvent(PlayerStaEnum.UpRising.ToString(), 1, nameof(SetAtkData));
        animator.AddAnimatorEvent(PlayerStaEnum.UpRising.ToString(), 1, nameof(SetAttackId));
        animator.AddAnimatorEvent(PlayerStaEnum.UpRising.ToString(), 6, nameof(Speed), "{\"x\":0,\"y\":15}");
        animator.AddAnimatorEvent(PlayerStaEnum.UpRising.ToString(), 6, nameof(PlayerSound), 32);
        animator.AddAnimatorEvent(PlayerStaEnum.UpRising.ToString(), 13, nameof(FallDown));
        animator.AddAnimatorEvent(PlayerStaEnum.UpRising.ToString(), 13, nameof(TurnRoundChild));

        animator.AddAnimatorEvent(PlayerStaEnum.HitGroundStart.ToString(), 0, nameof(Speed), "{\"x\":0,\"y\":-60}");
        animator.AddAnimatorEvent(PlayerStaEnum.HitGroundStart.ToString(), 0, nameof(AirPhysic), 0);
        animator.AddAnimatorEvent(PlayerStaEnum.HitGroundStart.ToString(), 0, nameof(Speed), "{\"x\":0,\"y\":5}");//10
        animator.AddAnimatorEvent(PlayerStaEnum.HitGroundStart.ToString(), 3, nameof(AirPhysic), 0);
        animator.AddAnimatorEvent(PlayerStaEnum.HitGroundStart.ToString(), 8, nameof(SetAtkData));
        animator.AddAnimatorEvent(PlayerStaEnum.HitGroundStart.ToString(), 8, nameof(SetAttackId));
        animator.AddAnimatorEvent(PlayerStaEnum.HitGroundStart.ToString(), 9, nameof(AirPhysic), 1);
        animator.AddAnimatorEvent(PlayerStaEnum.HitGroundStart.ToString(), 9, nameof(Speed), "{\"x\":0,\"y\":-20}"); //-80
        animator.AddAnimatorEvent(PlayerStaEnum.HitGroundStart.ToString(), 10, nameof(PlayAnim), PlayerStaEnum.HitGrounding.ToString());

        animator.AddAnimatorEvent(PlayerStaEnum.HitGrounding.ToString(), 0, nameof(HitGroundCheck));
        animator.AddAnimatorEvent(PlayerStaEnum.HitGrounding.ToString(), 0, nameof(AirAttackReset));
        animator.AddAnimatorEvent(PlayerStaEnum.HitGrounding.ToString(), 0, nameof(PlayerSound), 33);
        animator.AddAnimatorEvent(PlayerStaEnum.HitGrounding.ToString(), 0, nameof(FlashReset));

        animator.AddAnimatorEvent(PlayerStaEnum.HitGroundEnd.ToString(), 0, nameof(FlashReset));
        animator.AddAnimatorEvent(PlayerStaEnum.HitGroundEnd.ToString(), 9, nameof(PlayEffect), 166);
        animator.AddAnimatorEvent(PlayerStaEnum.HitGroundEnd.ToString(), 9, nameof(PlayAnim), PlayerStaEnum.Idle.ToString());
        
        animator.AddAnimatorEvent(PlayerStaEnum.DoubleFlash.ToString(), 0, nameof(SetAtkData));
        animator.AddAnimatorEvent(PlayerStaEnum.DoubleFlash.ToString(), 0, nameof(PlayEffect),183);
        animator.AddAnimatorEvent(PlayerStaEnum.DoubleFlash.ToString(), 0, nameof(PlayerSound),201);
        animator.AddAnimatorEvent(PlayerStaEnum.DoubleFlash.ToString(), 1, nameof(PlayerSound),202);
        animator.AddAnimatorEvent(PlayerStaEnum.DoubleFlash.ToString(), 1, nameof(SetAttackId));
        animator.AddAnimatorEvent(PlayerStaEnum.DoubleFlash.ToString(), 3, nameof(CanPlayNextAttack));
        animator.AddAnimatorEvent(PlayerStaEnum.DoubleFlash.ToString(), 7, nameof(AttackFinish));
        animator.AddAnimatorEvent(PlayerStaEnum.DoubleFlash.ToString(), 7, nameof(PlayAnim),PlayerStaEnum.Idle.ToString());
        
        animator.AddAnimatorEvent(PlayerStaEnum.AtkFlashRollEnd.ToString(), 0, nameof(PlayerSound),151);
        animator.AddAnimatorEvent(PlayerStaEnum.AtkFlashRollEnd.ToString(), 0, nameof(PlayEffect),184);
        animator.AddAnimatorEvent(PlayerStaEnum.AtkFlashRollEnd.ToString(), 0, nameof(SetAtkData));
        animator.AddAnimatorEvent(PlayerStaEnum.AtkFlashRollEnd.ToString(), 1, nameof(Speed), "{\"x\":0,\"y\":20}");
        animator.AddAnimatorEvent(PlayerStaEnum.AtkFlashRollEnd.ToString(), 3, nameof(Speed), "{\"x\":0,\"y\":-80}");
        animator.AddAnimatorEvent(PlayerStaEnum.AtkFlashRollEnd.ToString(), 15, nameof(PlayAnim), PlayerStaEnum.Idle.ToString());
    }

    private void Start()
    {
        getUp = true;
        _stateMachine = R.Player.StateMachine;
        _playerAction = R.Player.Action;
        _playerTimeController = R.Player.TimeController;

        _playerAtk = atkBox.GetComponent<PlayerAtk>();
        _attribute = R.Player.Attribute;
        onion = R.Player.GetComponent<OnionCreator>();
    }

    private void Update()
    {
        if (_playerTimeController.isPause) return;

        //开始落下 下降并且当前速度小于3 
        if (checkFallDown && _playerTimeController.GetCurrentSpeed().y <= 3f)
        {
            checkFallDown = false;
            _playerAction.ChangeState(PlayerStaEnum.Fall1);
            isFalling = true;
        }

        //正常落下
        if (isFalling && _attribute.isOnGround)
        {
            "着陆".Log();
            isFalling = false;
            checkFallDown = false;
            PhysicReset();
            PlayerSound(21);
            _playerAction.ChangeState(PlayerStaEnum.GetUp);
        }

        //受伤掉落
        if (flyHitFlag && _playerTimeController.GetCurrentSpeed().y <= 0f && _stateMachine.currentState.IsInArray(PlayerAction.HurtSta))
        {
            flyHitFlag = false;
            _playerAction.ChangeState(PlayerStaEnum.Hurt);
            flyHitGround = true;
        }

        //受伤掉落到地面
        if (flyHitGround && _attribute.isOnGround)
        {
            hitJump = false;
            flyHitGround = false;
            PhysicReset();
            Vector2 currentSpeed = _playerTimeController.GetCurrentSpeed();
            currentSpeed.x /= 2f;
            _playerTimeController.SetSpeed(currentSpeed);
            //pm.PAction.ChangeState(PlayerAction.StateEnum.UnderAtkHitGround);//受伤掉落地面的动画
        }

        UpdateFlashEnd();
    }


    /// <summary>
    /// 能否继续地面攻击
    /// </summary>
    public void CanPlayNextAttack()
    {
        _claymore.CanPlayNextAttack();
    }

    /// <summary>
    /// 攻击结束
    /// </summary>
    public void AttackFinish()
    {
        _claymore.AttackFinish();
    }

    /// <summary>
    /// 空中攻击停止
    /// </summary>
    public void AirAttackFinish()
    {
        _claymore.AirAttackFinish();
    }

    /// <summary>
    /// 能否继续空中攻击
    /// </summary>
    public void CanPlayNextAirAttack()
    {
        _claymore.CanPlayNextAirAttack();
    }

    /// <summary>
    /// 重置物理
    /// </summary>
    public void PhysicReset()
    {
        R.Player.Rigidbody2D.WakeUp();
        R.Player.Rigidbody2D.gravityScale = 1f;
    }

    public void FlashReset()
    {
    }


    public void SetAtkData()
    {
        if (GameReadDB.PlayerAtkData.TryGetValue(_stateMachine.currentState, out Dictionary<PlayerAtkDataType, string> data))
        {
            _playerAtk.SetData(data, Incrementor.GetNextId());
        }
    }

    public void TurnRoundChild()
    {
        //因为动画往左边的原因,的特殊处理
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    /// <summary>
    /// 设置攻击id
    /// </summary>
    public void SetAttackId()
    {
        _playerAtk.attackId = Incrementor.GetNextId();
    }

    /// <summary>
    /// 落下检测
    /// </summary>
    public void FallDown()
    {
        checkFallDown = true;
    }

    /// <summary>
    /// 检查攻击地面
    /// </summary>
    public void HitGroundCheck()
    {
        checkFallDown = false;
        isFalling = false;
        airAtkDown = false;
        checkHitGround = true;
    }

    public void Falling()
    {
        isFalling = true;
    }

    public void AirAtkDown()
    {
        isFalling = false;
        checkFallDown = false;
        checkHitGround = false;
        airAtkDown = true;
    }

    /// <summary>
    /// 空中的物理
    /// </summary>
    /// <param name="gravityScale"></param>
    public void AirPhysic(float gravityScale)
    {
        R.Player.TimeController.SetSpeed(Vector2.zero);
        R.Player.Rigidbody2D.gravityScale = gravityScale;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    public void PlayRunSound()
    {
        R.Audio.PlayEffect(runSound[Random.Range(0, runSound.Length)], transform.position);
    }

    /// <summary>
    /// 播放闪音效
    /// </summary>
    public void PlayFlashSound()
    {
        R.Audio.PlayEffect(flashSound[Random.Range(0, flashSound.Length)], transform.position);
    }

    public void PlayAtkSound()
    {
        R.Audio.PlayEffect(atkSound[Random.Range(0, atkSound.Length)], transform.position);
    }

    /// <summary>
    /// https://blog.csdn.net/linxinfa/article/details/87855614
    /// </summary>
    /// <param name="speed"></param>
    public void Speed(string speed)
    {
        string sta = _stateMachine.currentState;//测试专用
        JsonData jsonData = JsonMapper.ToObject(speed);
        float num = float.Parse(jsonData["x"].ToJson());
        float y = float.Parse(jsonData["y"].ToJson());
        Vector2 speed2 = new Vector2(transform.localScale.x * -num, y);
        R.Player.TimeController.SetSpeed(speed2);
    }

    public void CanChangeState()
    {
        _playerAction.canChangeAnim = true;
    }

    public void PlayAnim(string sta)
    {
        _playerAction.ChangeState(sta);
    }


    /// <summary>
    /// 空中攻击重置
    /// </summary>
    public void AirAttackReset()
    {
        if (_claymore.airAttackReset) return;
        _claymore.airAttackReset = true;
    }

    public void OpenOnion()
    {
        onion.Open(true, 0.3f, onionObj);
    }

    public void ChargeEnemyFrozen()
    {
        WorldTime.I.TimeFrozenByFixedFrame(10, FrozenArgs.FrozenType.Enemy);
    }

    public void StopRoll()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject gameObject = GameObject.Find("NewRoll(Clone)");
            if (gameObject != null)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void StopIEnumerator(string name)
    {
        StopCoroutine(name);
    }

    public void StopIEnumerator(IEnumerator enumerator)
    {
        StopCoroutine(enumerator);
    }

    public void MoveBoxSize(float xSize)
    {
        $"MoveBoxSize".Log();
        // Vector2 offset = _box.offset;
        // offset.x = -xSize / 2f;
        // _box.offset = offset;
        // Vector2 size = _box.size;
        // size.x = xSize;
        // _box.size = size;
    }

    public void BoxSizeRecover()
    {
        $"BoxSizeRecover".Log();
        // _box.offset = new Vector2(0f, 1f);
        // _box.size = new Vector2(1f, 2f);
    }

    public IEnumerator FlashPositionSet()
    {
        "FlashPositionSet打开".Log();
        float x = 0f;
        float y = 0f;
        PlayerStaEnum nextSta = PlayerStaEnum.Flash2;
        int num = flashDir; //方向
        switch (num + 5)
        {
            case 0:
            case 1:
                x = 3.535f;
                y = -3.535f;
                break;
            case 3:
                y = -5f;
                //nextSta = PlayerAction.StateEnum.FlashDown2;
                break;
            case 4:
            case 6:
                x = 5f;
                nextSta = PlayerStaEnum.Flash2;
                break;
            case 7:
                y = 4f;
                //nextSta = PlayerAction.StateEnum.FlashUp2;
                break;
            case 9:
            case 10:
                x = 3.535f;
                y = 3.535f;
                break;
        }

        Vector2 deltaPos = new Vector3(transform.parent.localScale.x * x, y);
        int clip = 8;
        for (int i = 0; i < clip; i++)
        {
            if (i == clip / 2)
            {
                float num2;
                if (deltaPos.x == 0f)
                {
                    num2 = 1.57079637f * Mathf.Sign(deltaPos.y);
                }
                else
                {
                    num2 = Mathf.Atan(deltaPos.y / deltaPos.x);
                }

                Vector3 position = Vector3.zero;
                if (y == 0f)
                {
                    position = Vector3.up * 0.5f;
                }

                R.Effect.Generate(48, transform, position, new Vector3(0f, 0f, num2 * 180f / 3.14159274f));
            }

            Vector3 nextPos = transform.parent.position + new Vector3(deltaPos.x / 8f, deltaPos.y / 8f, 0f);
            R.Player.TimeController.NextPosition(nextPos);
            yield return new WaitForFixedUpdate();
        }

        atkBox.localScale = Vector3.zero;
        atkBox.localPosition = Vector3.zero;
        R.Player.Rigidbody2D.gravityScale = 1f;
        if (_attribute.isOnGround)
        {
            //pAction.ChangeState(PlayerAction.StateEnum.FlashGround);
            _playerAction.ChangeState(PlayerStaEnum.FlashEnd);
        }
        else
        {
            Vector2 speed = new Vector2(5 * _attribute.faceDir, 10f);
            if (flashDir == 2 || flashDir == -2)
            {
                speed.x = 0f;
            }

            R.Player.TimeController.SetSpeed(speed);
            _playerAction.ChangeState(nextSta, 1.5f);
        }

        yield break;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioIndex"></param>
    public void PlayerSound(int audioIndex)
    {
        "音频播放audioIndex".Log();
        R.Audio.PlayEffect(audioIndex, transform.position);
    }

    public void RandomPlaySound(int audioIndex)
    {
        if (Random.Range(0, 100) > 60)
        {
            return;
        }

        PlayerSound(audioIndex);
    }

    /// <summary>
    /// 播放喊声灯
    /// </summary>
    public void PlayShoutSoundLight()
    {
    }

    public void PlayShoutSoundHeavy()
    {
    }

    public void PlayHurtSoundLight()
    {
    }

    public void PlayHurtSoundHeavy()
    {
    }


    public void PlayExecuteSound()
    {
    }

    /// <summary>
    /// 飞了
    /// </summary>
    public void FlyHit()
    {
        checkFallDown = false;
        isFalling = false;
        checkHitGround = false;
        flyHitFlag = true;
    }

    public void HitGetUp()
    {
        if (!_attribute.isDead && getUp)
        {
            getUp = true;
            $"HitGetUp".Log();
            _playerAction.ChangeState(PlayerStaEnum.Idle); //UnderAtkGetUp
        }
    }

    public IEnumerator HitJumpBack()
    {
        yield return new WaitForSeconds(0.2f);
        hitJump = true;
    }

    public IEnumerator PlayerDieEnumerator()
    {
        UIPause.I.Enabled = false;
        yield return new WaitForSeconds(2f);
        GameEvent.OnPlayerDead.Trigger(null);
        yield return new WaitForSeconds(2f);
    }

    public void PlayEffect(int effectId)
    {
        $"{R.Effect.fxData[effectId].functionName}显示特效{effectId}".Log();
        if (_stateMachine.currentState != "QTECharge1End" && _stateMachine.currentState != "Charge1End" &&
            _stateMachine.currentState != "AirChargeEnd")
        {
            R.Effect.Generate(effectId, this.transform);
            return;
        }

        AtkEffector component = R.Effect.fxData[effectId].effect.GetComponent<AtkEffector>();
        Vector3 pos = component.pos;
        if (component.CanHitGround)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(this.transform.position, -Vector2.up, 100f, LayerManager.GroundMask);
            pos = new Vector3(pos.x, pos.y - Mathf.Clamp(raycastHit2D.distance, 0f, float.PositiveInfinity), pos.z);
        }

        Transform transform = R.Effect.Generate(effectId, this.transform, pos);
        transform.localScale = new Vector3(transform.localScale.x * -Mathf.Sign(this.transform.localScale.x), transform.localScale.y,
            transform.localScale.z);
        if (component.UseAtkData)
        {
            if (charge)
            {
                charge = false;
                Vector3 position = Camera.main.transform.position.SetZ(-0.2f);
                transform.position = position;
                transform.GetComponent<AtkEffector>().SetData(GameReadDB.PlayerAtkData["Charge1EndLevel1"], Incrementor.GetNextId());
            }
            else
            {
                transform.GetComponent<AtkEffector>().SetData(GameReadDB.PlayerAtkData[_stateMachine.currentState], Incrementor.GetNextId());
            }
        }
    }

    public void CloseEnemyEffect()
    {
        R.Effect.Generate(152, transform, Vector3.zero);
    }

    public void CloseEnemyBlast()
    {
        R.Effect.Generate(153, transform, new Vector3(_attribute.faceDir * 2.5f, 1f, 0f));
    }

    public void FlashStart()
    {
        _flashEndCount = WorldTime.SecondToFrame(0.2f);
        _attribute.flashFlag = true;
    }

    /// <summary>
    /// 更新闪的结束
    /// </summary>
    private void UpdateFlashEnd()
    {
        if (_flashEndCount > 0)
        {
            _flashEndCount--;
            if (_flashEndCount > 0) return;
            _attribute.flashFlag = false;
        }
    }


    /// <summary>
    /// 获取执行的敌人
    /// </summary>
    public void GetExecuteEnemy()
    {
        Vector2 zero = Vector2.zero;
        for (int i = 0; i < executeEnemyList.Count; i++)
        {
            EnemyHurtAtkEventArgs args = new EnemyHurtAtkEventArgs(executeEnemyList[i].gameObject, EnemyHurtAtkEventArgs.HurtTypeEnum.ExecuteFollow);
            GameEvent.EnemyHurtAtk.Trigger(args);
            zero.x = (transform.position.x + executeEnemyList[i].transform.position.x) / 2f;
        }

        zero.y = transform.position.y + 2f;
        R.Camera.Controller.CameraShake(0.25f, ShakeTypeEnum.Rect, 0.6f);
        CameraController.I.OpenMotionBlur(0.13333334f, 1f, transform.position);
        CameraController.I.CameraZoom(new Vector3(zero.x, zero.y, Camera.main.transform.parent.position.z + 3f), 0.166666672f);
    }

    public void StartExecute()
    {
        // for (int i = 0; i < executeEnemyList.Count; i++)
        // {
        // 	EnemyHurtAtkEventArgs args = new EnemyHurtAtkEventArgs(executeEnemyList[i], EnemyHurtAtkEventArgs.HurtTypeEnum.Execute, stateMachine.currentState);
        // 	EventManager.PostEvent("EnemyHurtAtk", gameObject, args);
        // }
        // executeEnemyList.Clear();
        // pAction.QTEHPRecover();
    }

    public void ExecuteTimeSlow()
    {
        SingletonMono<WorldTime>.I.TimeSlowByFrameOn60Fps(30, 0.5f);
    }

    public void Execute2_1Hit()
    {
        // for (int i = 0; i < executeEnemyList.Count; i++)
        // {
        // 	EnemyBaseHurt component = executeEnemyList[i].GetComponent<EnemyBaseHurt>();
        // 	component.StopFollowLeftHand();
        // 	Vector3 position = executeEnemyList[i].transform.position;
        // 	position.y = Mathf.Clamp(position.y + 1f, position.y, LayerManager.YNum.GetGroundHeight(executeEnemyList[i].gameObject) + 4f);
        // 	executeEnemyList[i].transform.position = position;
        // 	EnemyHurtAtkEventArgs.PlayerNormalAtkData attackData = new EnemyHurtAtkEventArgs.PlayerNormalAtkData(atkData[stateMachine.currentState], true)
        // 	{
        // 		camShakeFrame = 0,
        // 		shakeStrength = 0f
        // 	};
        // 	Vector3 position2 = component.center.position;
        // 	EnemyHurtAtkEventArgs args = new EnemyHurtAtkEventArgs(executeEnemyList[i].gameObject, gameObject, Incrementor.GetNextId(), position2, HurtCheck.BodyType.Body, attackData);
        // 	EventManager.PostEvent("EnemyHurtAtk", gameObject, args);
        // 	R.Camera.Controller.OpenMotionBlur(0.13333334f, 1f, transform.position);
        // 	R.Camera.Controller.ZoomFinished();
        // 	R.Camera.Controller.CameraMoveToBySpeed(Camera.main.transform.parent.position + Vector3.up * 4f, 20f, false, Ease.InOutExpo);
        // 	R.Camera.Controller.CameraShake(0.266666681f, 0.4f, CameraController.ShakeTypeEnum.Horizon);
        // }
    }

    public IEnumerator Execute2_1ChangeState()
    {
        // GameObject enemy = executeEnemyList[0].gameObject;
        // EnemyAttribute attr = enemy.GetComponent<EnemyAttribute>();
        // while (attr.timeController.GetCurrentSpeed().y > 0f)
        // {
        // 	yield return null;
        // }
        // attr.timeController.SetSpeed(Vector2.zero);
        // AirPhysic(0f);
        // Vector3 pos = transform.position;
        // pos.y = enemy.transform.position.y;
        // transform.position = pos;
        // pAction.ChangeState(PlayerAction.StateEnum.NewExecute2_2);
        yield return null;
    }

    public void AirExecute1_2Hit()
    {
        // for (int i = 0; i < executeEnemyList.Count; i++)
        // {
        // 	executeEnemyList[i].GetComponent<EnemyAttribute>().timeController.SetSpeed(Vector2.down * 60f);
        // 	Vector3 position = Camera.main.transform.parent.position;
        // 	position.y = LayerManager.YNum.GetGroundHeight(executeEnemyList[i]) + 2f;
        // 	R.Camera.Controller.ZoomFinished();
        // 	R.Camera.Controller.CameraMoveToBySpeed(position, 40f, false, Ease.InOutExpo);
        // 	R.Camera.Controller.CameraShake(0.167f, 0.4f, CameraController.ShakeTypeEnum.Rect, true);
        // }
    }

    public void QTECameraFinish()
    {
    }

    public void ChargeEffectAppear()
    {
        // if (chargeAnim.gameObject.activeSelf)
        // {
        // 	return;
        // }
        // chargeAnim.gameObject.SetActive(true);
        // chargeAnim.ChargeZeroToOne();
    }

    public void ChargeEffectDisappear()
    {
        //chargeAnim.gameObject.SetActive(false);
    }


    [Header("检查下降")] public bool checkFallDown;
    [Header("是否下降")] public bool isFalling;
    [Header("空袭击落")] public bool airAtkDown;
    [Header("是否开启检查击中地面")] public bool checkHitGround;
    public bool getUp;
    public bool charge;
    public bool flyHitFlag;
    public bool flyHitGround;
    [Header("跳打")] public bool hitJump;
    [Header("闪的距离")] public int flashDir;
    [Header("跳打时间")] private float hitJumpTime;
    public Transform atkBox;
    [Header("闪的结束数量")] private int _flashEndCount;
    public List<GameObject> executeEnemyList;
    [SerializeField] private GameObject[] onionObj;
    private int[] runSound = { 11, 12, 13, 14, 15, 16, 17 };
    private int[] flashSound = { 18, 19, 20 };
    private int[] hurtSoundLight = { 44, 45, 46 };
    private int[] hurtSoundHeavy = { 47, 48 };
    private int[] atkSound = { 24, 25, 26, 27 };
    private int[] executeSound = { 134, 135 };
}