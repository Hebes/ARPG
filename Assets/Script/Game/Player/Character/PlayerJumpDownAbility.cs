/// <summary>
/// 玩家跳下来技能
/// </summary>
public class PlayerJumpDownAbility : CharacterState
{
    public override void Start()
    {
        _platform = Action.GetComponent<PlatformMovement>();
    }

    public void JumpDown()
    {
        "玩家跳下来".Log();
        if (!StateMachine.currentState.IsInArray(PlayerAction.NormalSta)) return;
        if (_platform.IgnoreOnOneWayGround())
            Action.ChangeState(PlayerStaEnum.Fall1);
    }

    private PlatformMovement _platform;
}