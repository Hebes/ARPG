using UnityEngine;

public class TrainingDummyHurt : EnemyBaseHurt
{
    #region 组件

    private TrainingDummyAnimEvent _anim;

    private void GetComponent()
    {
        _anim = GetComponent<TrainingDummyAnimEvent>();
    }

    #endregion
    
    protected override void Init()
    {
        base.Init();
        GetComponent();
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