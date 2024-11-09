using UnityEngine;

/// <summary>
/// 弥诺陶洛斯BOSS受伤
/// </summary>
public class MinotaurHurt : EnemyBaseHurt,IComponents
{
    public void InitAwake(Object o)
    {
        
    }

    protected override void Start()
    {
        base.Start();
        hurtData = DB.EnemyHurtData[EnemyType.弥诺陶洛斯.ToString()];
    }

    /// <summary>
    /// 播放受伤音效
    /// </summary>
    protected override void PlayHurtAudio()
    {
        R.Audio.PlayEffect(251, transform.position); //256
        if (PlaySpHurtAudio())
        {
            R.Audio.PlayEffect(Random.Range(57, 59), transform.position);
        }
    }

    /// <summary>
    /// 执行跟踪
    /// </summary>
    protected override void ExecuteFollow()
    {
        action.ChangeState(EnemyStaEnum.Run);
        base.ExecuteFollow();
    }
    
    public override void EnemyDie()
    {
        if (deadFlag)return;
        base.EnemyDie();
        NormalKill();
        SetHitSpeed(Vector2.zero);
        action.ChangeState(EnemyStaEnum.Death);
        Invoke(nameof(DieTimeControl), 0.12f);
    }
}