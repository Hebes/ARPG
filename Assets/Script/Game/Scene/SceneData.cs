/// <summary>
/// 世界数据
/// </summary>
public class SceneData
{
    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        assessmentData = new BattleAssessmentData(); //战斗评估数据
    }

    /// <summary>
    /// 是否进入屏障模式
    /// </summary>
    public bool BloodPalaceMode;

    public bool CanAIRun;
    public BloodPalaceTotalScore TotalScore;
    public BloodPalaceLevelScore LevelScore = new BloodPalaceLevelScore();
    public BattleAssessmentData assessmentData = new BattleAssessmentData();
}