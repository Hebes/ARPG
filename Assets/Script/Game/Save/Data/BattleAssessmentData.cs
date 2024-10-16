/// <summary>
/// 战斗评估数据
/// </summary>
public class BattleAssessmentData
{
    /// <summary>
    /// 连击清除
    /// </summary>
    public void ComboClear()
    {
        ComboData.SameComboNum = 0;
        ComboData.AirComboNum = 0;
        ComboData.FlashAttackSuccessNum = 0;
        ComboData.CoreBreakNum = 0;
        ComboData.AllDamagePercent = 0f;
    }

    /// <summary>
    /// 战斗清除
    /// </summary>
    public void BattleClear()
    {
        FlashAttackSuccessNum = 0;
        FlashAttackNum = 0;
    }

    /// <summary>
    /// 连击数据
    /// </summary>
    public BattleComboData ComboData = new BattleComboData();

    /// <summary>
    /// 反击成功次数
    /// </summary>
    public int FlashAttackSuccessNum;

    /// <summary>
    /// 反击攻击数
    /// </summary>
    public int FlashAttackNum;

    /// <summary>
    /// 是否无伤
    /// </summary>
    public bool NotHurt = true;

    /// <summary>
    /// 当前连击数
    /// </summary>
    public int CurrentComboNum;

    /// <summary>
    /// 战斗连击数据
    /// </summary>
    public class BattleComboData
    {
        /// <summary>
        /// 相同连击数
        /// </summary>
        public int SameComboNum;

        /// <summary>
        /// 空中连击数
        /// </summary>
        public int AirComboNum;

        /// <summary>
        /// 反击攻击成功次数
        /// </summary>
        public int FlashAttackSuccessNum;

        /// <summary>
        /// 核心破坏数
        /// </summary>
        public int CoreBreakNum;

        /// <summary>
        /// 所有伤害百分比
        /// </summary>
        public float AllDamagePercent;
    }
}