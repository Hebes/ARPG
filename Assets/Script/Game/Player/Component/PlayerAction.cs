using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 玩家行动
/// 重置属性和给定默认旋转方向
/// 玩家朝向,播放动画
/// </summary>
public class PlayerAction : MonoBehaviour
{
    private StateMachine _stateMachine;
    private PlayerAbilities _playerAbilities;
    private PlayerAnimationController _playerAnimationController;
    private PlayerAnimEvent listener;
    private PlayerAttribute playerAttribute;
    private Claymore weapon;
    [SerializeField] public ParticleSystem blockPartical;

    /// <summary>
    /// 是否允许通过门
    /// </summary>
    public bool NotAllowPassSceneGate => _stateMachine.currentState.IsInArray(HurtSta) ||
                                         _stateMachine.currentState.IsInArray(ExecuteSta);

    public bool IsInNormalState()
    {
        return _stateMachine.currentState.IsInArray(NormalSta);
    }


    private void Awake()
    {
        _playerAnimationController = GetComponent<PlayerAnimationController>();
        blockPartical = transform.FindComponent<ParticleSystem>();
        weapon = GetComponent<Claymore>();
        listener = transform.FindComponent<PlayerAnimEvent>();
        playerAttribute = R.Player.Attribute;
        _stateMachine = GetComponent<StateMachine>();
        _stateMachine = GetComponent<StateMachine>();
        _playerAbilities = GetComponent<PlayerAbilities>();
        _stateMachine.AddStates(typeof(PlayerStaEnum).ToArray());
        _stateMachine.OnEnter += OnMyStateEnter;
        _stateMachine.OnTransfer += OnStateTransfer;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        R.Player.Attribute.ResetData();
        TurnRound(1);
        ChangeState(PlayerStaEnum.Idle); //初始化玩家状态
    }

    private void Update()
    {
        if (openPos)
        {
            transform.position = position;
        }

        if (_move)
        {
            _playerAbilities.move.Move(R.Player.Attribute.faceDir);
        }
    }

    /// <summary>
    /// 朝向
    /// </summary>
    /// <param name="dir">方向</param>
    public void TurnRound(int dir)
    {
        Vector3 localScale = transform.localScale;
        GameEvent.OnPlayerTurnRound.Trigger(null);
        switch (dir)
        {
            case -1:
                R.Player.Attribute.faceDir = dir;
                localScale.x = -1f * Mathf.Abs(localScale.x);
                transform.localScale = localScale;
                break;
            case 1:
                R.Player.Attribute.faceDir = dir;
                localScale.x = 1f * Mathf.Abs(localScale.x);
                transform.localScale = localScale;
                break;
        }
    }

    public void StartMove()
    {
        _move = true;
    }

    public void StopMove()
    {
        _move = false;
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void OnMyStateEnter(object sender, StateEventArgs args)
    {
        string state = args.state;
        //if (R.Player.Abilities.hurt.DeadFlag && !args.state.IsInArray(DieSta)) return;
        if (R.Player.Attribute.isDead && !state.IsInArray(DieSta)) return;
        PlayerStaEnum playerEnemyStaEnum = state.ToEnum<PlayerStaEnum>();
        switch (playerEnemyStaEnum)
        {
            case PlayerStaEnum.AirAtk1: //空中攻击1
            case PlayerStaEnum.AirAtk2: //空中攻击2
            case PlayerStaEnum.AirAtk3: //空中攻击3
            case PlayerStaEnum.Flash: //闪
            case PlayerStaEnum.Flash2: //闪2
            case PlayerStaEnum.FlashEnd: //闪结束
            case PlayerStaEnum.Jump:
            case PlayerStaEnum.Fall1:
            case PlayerStaEnum.Atk1:
            case PlayerStaEnum.Atk2:
            case PlayerStaEnum.Atk3:
            case PlayerStaEnum.Death:
            case PlayerStaEnum.AtkRemote: //远程攻击
            case PlayerStaEnum.RunSlow:
            case PlayerStaEnum.UpRising:
            case PlayerStaEnum.HitGroundStart:
            case PlayerStaEnum.HitGroundEnd:
            case PlayerStaEnum.DoubleFlash:
            case PlayerStaEnum.AtkFlashRollEnd:
                _playerAnimationController.Play(state, false, true, _playerAnimationController.animSpeed);
                break;
            case PlayerStaEnum.Idle:
            case PlayerStaEnum.Run:
            case PlayerStaEnum.Jumping:
            case PlayerStaEnum.Falling:
            case PlayerStaEnum.Hurt:
            case PlayerStaEnum.GetUp:
            case PlayerStaEnum.HitGrounding:
                _playerAnimationController.Play(state, true, false, _playerAnimationController.animSpeed);
                break;
            default: throw new Exception($"未找到播放的状态的动画{state}");
        }
    }

    private void OnStateTransfer(object sender, TransferEventArgs args)
    {
        canChangeAnim = false;
        listener.BoxSizeRecover();
        if (args.nextState.IsInArray(HurtSta))
        {
            weapon.AirAttackReset();
            listener.StopIEnumerator("FlashPositionSet");
            listener.isFalling = false;
            listener.airAtkDown = false;
            listener.checkFallDown = false;
        }

        if (!args.lastState.IsInArray(NormalSta) && args.nextState.IsInArray(NormalSta))
        {
            listener.PhysicReset();
            weapon.AirAttackReset();
            if (!GetComponent<Collider2D>().enabled)
            {
                GetComponent<Collider2D>().enabled = true;
            }
        }

        if (args.nextState == "UnderAtkHitToFly")
        {
            listener.hitJump = false;
            StartCoroutine(listener.HitJumpBack());
        }

        // if (pab.hurt.DeadFlag)
        // {
        //     GetComponent<ChangeSpineColor>().TurnOffAll();
        // }
        // else if (args.nextState.IsInArray(NormalSta) || args.nextState.IsInArray(JumpSta) || args.nextState.IsInArray(FlySta))
        // {
        //     GetComponent<ChangeSpineColor>().TurnOnBreatheLight();
        // }
        // else
        // {
        //     GetComponent<ChangeSpineColor>().TurnOnEmission();
        // }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
    {
        if (_stateMachine.currentState.IsInArray(HurtSta))
        {
            ChangeState(!playerAttribute.isOnGround ? PlayerStaEnum.Fall1 : PlayerStaEnum.Idle);
        }
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="sta"></param>
    /// <param name="speed"></param>
    public void ChangeState(PlayerStaEnum sta, float speed = 1f)
    {
        ChangeState(sta.ToString(), speed);
    }

    public void ChangeState(string sta, float speed = 1f)
    {
        _playerAnimationController.animSpeed = speed;
        _stateMachine.SetState(sta);
    }

    /// <summary>
    /// 更新
    /// </summary>
    public static void Reborn()
    {
        GameEvent.Assessment.Trigger((R.Player.Action, new AssessmentEventArgs(AssessmentEventArgs.EventType.ContinueGame)));
        R.Player.Attribute.AllAttributeRecovery();
        R.Player.Abilities.hurt.DeadFlag = false; //死去的标志位
        R.Player.Action.ChangeState(PlayerStaEnum.Idle);
    }

    /// <summary>
    /// 重置
    /// </summary>
    public static void Reset()
    {
        "玩家重置".Log();
        PlayerAttribute attribute = R.Player.Attribute;
        attribute.ResetData();
        R.Player.Abilities.hurt.DeadFlag = false;
        R.Player.Action.ChangeState(PlayerStaEnum.Idle);
    }

    [Header("能否切换动画")] public bool canChangeAnim;
    [SerializeField] private Transform leftHand;
    [SerializeField] private MeshRenderer spATK;
    [Header("临时方向")] public int tempDir;
    public Transform hurtBox;
    [Header("能量球数量")] public int absorbNum;
    private bool _move;
    public Vector3 position;
    public bool openPos;

    /// <summary>
    /// 处决状态
    /// </summary>
    public static readonly string[] ExecuteSta =
    {
        "Execute",
        "Execute2",
    };

    /// <summary>
    /// 默认状态
    /// </summary>
    public static readonly string[] NormalSta =
    {
        PlayerStaEnum.Idle.ToString(),
        PlayerStaEnum.GetUp.ToString(),
        PlayerStaEnum.Run.ToString(),
        PlayerStaEnum.RunSlow.ToString(),
    };

    /// <summary>
    /// 空中轻型攻击
    /// </summary>
    public static readonly string[] AirLightAttackSta =
    {
        "AirAtk1",
    };

    /// <summary>
    /// 攻击状态
    /// </summary>
    public static readonly string[] AttackSta =
    {
        PlayerStaEnum.Atk1.ToString(),
        PlayerStaEnum.Atk2.ToString(),
        PlayerStaEnum.Atk3.ToString(),
        PlayerStaEnum.Atk4.ToString(),
        PlayerStaEnum.DoubleFlash.ToString(),
    };

    /// <summary>
    /// 移动状态
    /// </summary>
    public static readonly string[] RunSta =
    {
        PlayerStaEnum.Run.ToString(),
    };

    /// <summary>
    /// 跳跃状态
    /// </summary>
    public static readonly string[] JumpSta =
    {
        PlayerStaEnum.Jump.ToString(),
        PlayerStaEnum.Jumping.ToString(),
    };

    /// <summary>
    /// 飞行状态
    /// </summary>
    public static readonly string[] FlySta =
    {
        PlayerStaEnum.Fall1.ToString(),
        PlayerStaEnum.Falling.ToString(),
        PlayerStaEnum.Jumping.ToString(),
    };

    /// <summary>
    /// 向上上升
    /// </summary>
    public static readonly string[] UpRisingSta =
    {
        "UpRising",
        "AtkUpRising"
    };

    /// <summary>
    /// 闪避攻击
    /// </summary>
    public static readonly string[] FlashAttackSta =
    {
        PlayerStaEnum.Flash.ToString(),
        "FlashDown1",
    };

    /// <summary>
    /// 死亡状态
    /// </summary>
    public static readonly string[] DieSta =
    {
        PlayerStaEnum.Death.ToString(),
    };

    /// <summary>
    /// 受伤状态
    /// </summary>
    public static readonly string[] HurtSta =
    {
        PlayerStaEnum.Hurt.ToString(),
    };

    /// <summary>
    /// 空中攻击状态
    /// </summary>
    public static readonly string[] AirAttackSta =
    {
        PlayerStaEnum.AirAtk1.ToString(),
        PlayerStaEnum.AirAtk2.ToString(),
        PlayerStaEnum.AirAtk3.ToString(),
    };

    /// <summary>
    /// 击中地面状态
    /// </summary>
    public static readonly string[] HitGroundSta =
    {
        "HitGround",
        "HitGround2",
        "RollReady",
        "Roll",
        "RollEnd",
        "RollEndFrame",
        "RollFrameEnd"
    };

    /// <summary>
    /// 充能状态
    /// </summary>
    public static readonly string[] ChargeSta =
    {
        "Charge1Ready",
        "Charging1",
        "Charge1End",
        "AirCharging",
        "AirChargeEnd"
    };
}