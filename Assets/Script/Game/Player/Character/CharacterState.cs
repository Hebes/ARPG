using System.Collections;
using UnityEngine;

/// <summary>
/// 角色状态
/// </summary>
public abstract class CharacterState
{
    protected PlayerAction Action;
    protected PlayerAttribute Attribute;
    protected StateMachine StateMachine;
    protected Claymore weapon;
    protected PlayerAnimEvent Listener;
    protected PlayerAbilities Abilities;
    protected PlayerTimeController TimeController;

    public void Init()
    {
        TimeController = R.Player.TimeController;
        Abilities = R.Player.Abilities;
        Action = R.Player.Action;
        Attribute = R.Player.Attribute;
        StateMachine = R.Player.StateMachine;
        Listener = R.Player.AnimEvent;
        weapon = R.Player.Claymore;
    }

    public virtual void Start()
    {
    }

    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
    }

    public virtual void OnDestroy()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void OnStateTransfer(object sender, TransferEventArgs args)
    {
    }

    public virtual void OnStateEnter(object sender, StateEventArgs args)
    {
    }

    public virtual void OnStateExit(object sender, StateEventArgs args)
    {
    }
    
    /// <summary>
    /// 启用携程
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    protected Coroutine StartCoroutine(IEnumerator routine)
    {
        return Action.StartCoroutine(routine);
    }

    /// <summary>
    /// 停止携程
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    protected void StopIEnumerator()
    {
        
    }
}