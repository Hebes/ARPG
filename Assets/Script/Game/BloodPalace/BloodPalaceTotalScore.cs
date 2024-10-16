using System;

/// <summary>
/// 血宫总得分
/// </summary>
[Serializable]
public class BloodPalaceTotalScore : IComparable<BloodPalaceTotalScore>
{
    /// <summary>
    /// 添加关卡分数
    /// </summary>
    /// <param name="levelScore"></param>
    public void AddLevelScore(BloodPalaceLevelScore levelScore)
    {
        this.Score += levelScore.Score;
        this.NotHurt &= levelScore.NotHurt;
        this.Time += levelScore.Time;
    }

    public int CompareTo(BloodPalaceTotalScore other)
    {
        return -1 * this.Score.CompareTo(other.Score);
    }

    public int Score;

    public bool NotHurt = true;

    public float Time;
}