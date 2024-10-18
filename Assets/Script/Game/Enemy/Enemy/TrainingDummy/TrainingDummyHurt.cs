using UnityEngine;

public class TrainingDummyHurt : EnemyBaseHurt
{
    private TrainingDummyAnimEvent _anim;

    protected override void Init()
    {
        base.Init();
        _anim = transform.FindComponent<TrainingDummyAnimEvent>();
        hurtData = DB.EnemyHurtData[EnemyType.稻草人.ToString()];
    }

    /// <summary>
    /// 设置命中敌人的velocity
    /// </summary>
    /// <param name="speed"></param>
    public override void SetHitSpeed(Vector2 speed)
    {
        if (playerAtkName == PlayerStaEnum.UpRising.ToString() ||
            playerAtkName == "AtkUpRising" ||
            playerAtkName == "AtkRollEnd" ||
            playerAtkName == "NewExecute2_1")
            _anim.maxFlyHeight = 4.5f;
        else
            _anim.maxFlyHeight = -1f;
        base.SetHitSpeed(speed);
    }
}