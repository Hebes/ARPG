using System.Collections.Generic;

/// <summary>
/// 敌人数据
/// </summary>
public class EnemyDataPreload : SingletonMono<EnemyDataPreload>
{
    public Dictionary<EnemyType, Dictionary<string, Dictionary<EnemyAttackDataType, string>>> attack = new();
    public Dictionary<EnemyType, Dictionary<string, Dictionary<EnemyHurtDataType, string>>>  hurt = new();
    public float[][] VibrationData;
    
    private void Awake()
    {
        attack = GameReadDB.EnemyAttackData;
        "加载攻击数据成功".Log();
        hurt =GameReadDB.EnemyHurtData;
        "加载受伤数据成功".Log();
        VibrationData = GameReadDB.VibrationData;
    }
}