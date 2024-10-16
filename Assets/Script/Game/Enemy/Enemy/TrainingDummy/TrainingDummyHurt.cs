using UnityEngine;

public class TrainingDummyHurt : EnemyBaseHurt
{
    #region 组件

    private TrainingDummyAnimEvent _anim;

    private void GetComponent()
    {
        _anim = GetComponentInChildren<TrainingDummyAnimEvent>();
    }

    #endregion
    
    protected override void Init()
    {
        base.Init();
        GetComponent();
        hurtData = GameReadDB.EnemyHurtData[EnemyType.稻草人];
    }
    
    /// <summary>
    /// 设置命中速度
    /// </summary>
    /// <param name="speed"></param>
    public override void SetHitSpeed(Vector2 speed)
    {
        if (playerAtkName is "UpRising" or "AtkUpRising" or "AtkRollEnd" or "NewExecute2_1")
            _anim.maxFlyHeight = 4.5f;
        else
            _anim.maxFlyHeight = -1f;
        base.SetHitSpeed(speed);
    }
}