using UnityEngine;

/// <summary>
/// 玩家能力
/// </summary>
public class PlayerAbilities : MonoBehaviour
{
    private StateMachine _stateMachine; //状态机
    private CharacterState[] _states; //角色行为

    public readonly PlayerMoveAbility Move = new PlayerMoveAbility(); //移动
    public readonly PlayerAttackAbility Attack = new PlayerAttackAbility(); //攻击
    public readonly PlayerJumpAbility Jump = new PlayerJumpAbility(); //跳跃
    public readonly PlayerUpRisingAbility UpRising = new PlayerUpRisingAbility(); //上升
    public readonly PlayerFlashAbility Flash = new PlayerFlashAbility(); //闪
    public readonly PlayerHurtAbility Hurt = new PlayerHurtAbility(); //受伤
    public readonly PlayerHitGroundAbility HitGround = new PlayerHitGroundAbility(); //往地面攻击
    public readonly PlayerFallAbility Fall = new PlayerFallAbility();//落下能力

    // //下面测试暂未开放
    public readonly PlayerChargingAbility Charge = new PlayerChargingAbility(); //充能
    // public PlayerChaseAbility chase = new PlayerChaseAbility(); //追逐

    private void Awake()
    {
        _stateMachine = R.Player.StateMachine;
        _stateMachine.OnEnter += OnStateMachineStateEnter;
        _stateMachine.OnExit += OnStateMachineStateExit;
        _stateMachine.OnTransfer += OnStateMachineStateTransfer;
        _states = new CharacterState[9];
        _states[0] = Move;
        _states[1] = Attack;
        _states[2] = Jump;
        _states[3] = Hurt;
        _states[4] = Flash;
        _states[5] = UpRising;
        _states[6] = HitGround;
        _states[7] = Fall;
        _states[8] = Charge;
        // states[4] = execute;
        // states[7] = jumpDown;
        // states[9] = skill;
        // states[10] = flashAttack;
        //states[12] = chase;
        foreach (var s in _states)
            s.Init();
    }

    private void Start()
    {
        for (var i = 0; i < _states.Length; i++)
            _states[i].Start();
    }

    private void Update()
    {
        for (var i = 0; i < _states.Length; i++)
            _states[i].Update();
    }

    private void OnEnable()
    {
        for (var i = 0; i < _states.Length; i++)
            _states[i].OnEnable();
    }

    private void OnDisable()
    {
        for (var i = 0; i < _states.Length; i++)
            _states[i].OnDisable();
    }

    private void OnDestroy()
    {
        for (var i = 0; i < _states.Length; i++)
            _states[i].OnDestroy();
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < _states?.Length; i++)
            _states[i].FixedUpdate();
    }

    protected virtual void OnStateMachineStateTransfer(object sender, TransferEventArgs args)
    {
        for (var i = 0; i < _states.Length; i++)
            _states[i].OnStateTransfer(sender, args);
    }

    protected virtual void OnStateMachineStateEnter(object sender, StateEventArgs args)
    {
        for (var i = 0; i < _states.Length; i++)
            _states[i].OnStateEnter(sender, args);
    }

    protected virtual void OnStateMachineStateExit(object sender, StateEventArgs args)
    {
        for (var i = 0; i < _states.Length; i++)
            _states[i].OnStateExit(sender, args);
    }
}