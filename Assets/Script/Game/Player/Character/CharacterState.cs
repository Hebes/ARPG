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
    protected Claymore Weapon;
    protected PlayerAnimEvent AnimEvent;
    protected PlayerAbilities Abilities;
    protected PlayerTimeController TimeController;

    public void Init()
    {
        TimeController = R.Player.TimeController;
        Abilities = R.Player.Abilities;
        Action = R.Player.Action;
        Attribute = R.Player.Attribute;
        StateMachine = R.Player.StateMachine;
        AnimEvent = R.Player.AnimEvent;
        Weapon = R.Player.Claymore;
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
}