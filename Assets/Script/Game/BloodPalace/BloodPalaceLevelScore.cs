/// <summary>
/// 血宫等级评分
/// </summary>
public class BloodPalaceLevelScore
{
    public int Score => OriginalScore * (NotHurt ? 5 : 1);

    public void Clear()
    {
        WaveScore = 0;
        BeautyScore = 0;
        Time = 0f;
        FlashPercent = 0f;
        OriginalScore = 0;
        NotHurt = true;
    }

    public int WaveScore;

    public int BeautyScore;

    public float Time;

    public float FlashPercent;

    public int OriginalScore;

    public bool NotHurt = true;
}