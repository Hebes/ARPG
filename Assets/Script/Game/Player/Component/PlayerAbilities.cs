using UnityEngine;

/// <summary>
/// 玩家能力
/// </summary>
public class PlayerAbilities : MonoBehaviour
{
    protected StateMachine stateMachine; //状态机
    private CharacterState[] states; //角色行为

    public PlayerMoveAbility move = new PlayerMoveAbility(); //移动
    public PlayerAttackAbility attack = new PlayerAttackAbility(); //攻击
    public PlayerJumpAbility jump = new PlayerJumpAbility(); //跳跃
    public PlayerUpRisingAbility upRising = new PlayerUpRisingAbility(); //上升
    public PlayerFlashAbility flash = new PlayerFlashAbility(); //闪
    public PlayerHurtAbility hurt = new PlayerHurtAbility(); //受伤
    public PlayerHitGroundAbility hitGround = new PlayerHitGroundAbility(); //往地面攻击

    // //下面测试暂未开放
    // public PlayerChargingAbility charge = new PlayerChargingAbility();//充能
    // public PlayerChaseAbility chase = new PlayerChaseAbility(); //追逐

    private void Awake()
    {
        stateMachine = R.Player.StateMachine;
        stateMachine.OnEnter += OnStateMachineStateEnter;
        stateMachine.OnExit += OnStateMachineStateExit;
        stateMachine.OnTransfer += OnStateMachineStateTransfer;
        states = new CharacterState[7];
        states[0] = move;
        states[1] = attack;
        states[2] = jump;
        states[3] = hurt;
        states[4] = flash;
        states[5] = upRising;
        states[6] = hitGround;
        // states[3] = charge;
        // states[4] = execute;
        // states[7] = jumpDown;
        // states[9] = skill;
        // states[10] = flashAttack;
        //states[12] = chase;
        foreach (var s in states)
            s.Init();
    }

    private void Start()
    {
        for (var i = 0; i < states.Length; i++)
            states[i].Start();
    }

    private void Update()
    {
        for (var i = 0; i < states.Length; i++)
            states[i].Update();
    }

    private void OnEnable()
    {
        for (var i = 0; i < states.Length; i++)
            states[i].OnEnable();
    }

    private void OnDisable()
    {
        for (var i = 0; i < states.Length; i++)
            states[i].OnDisable();
    }

    private void OnDestroy()
    {
        for (var i = 0; i < states.Length; i++)
            states[i].OnDestroy();
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < states?.Length; i++)
            states[i].FixedUpdate();
    }

    protected virtual void OnStateMachineStateTransfer(object sender, TransferEventArgs args)
    {
        for (var i = 0; i < states.Length; i++)
            states[i].OnStateTransfer(sender, args);
    }

    protected virtual void OnStateMachineStateEnter(object sender, StateEventArgs args)
    {
        for (var i = 0; i < states.Length; i++)
            states[i].OnStateEnter(sender, args);
    }

    protected virtual void OnStateMachineStateExit(object sender, StateEventArgs args)
    {
        for (var i = 0; i < states.Length; i++)
            states[i].OnStateExit(sender, args);
    }
}