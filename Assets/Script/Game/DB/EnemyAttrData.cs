using System;

[Serializable]
public class EnemyAttrData
{
    public string sceneName { get; set; }

    public int level { get; set; }

    public int maxHp { get; set; }

    public int atk { get; set; }

    public float atkSpeed { get; set; }

    public int maxSP { get; set; }

    public float scanSpeed { get; set; }

    public float moveSpeed { get; set; }

    public int flyHeight { get; set; }

    public int enemyId { get; set; }

    public int counterAttack { get; set; }

    public int counterAttackProbPercentage { get; set; }

    public int actionInterruptPoint { get; set; }

    public int baseDefence { get; set; }

    public int dropCoins { get; set; }

    public int dropExp { get; set; }

    public EnemyType enemyType
    {
        get { return (EnemyType)this.enemyId; }
    }

    public static EnemyAttrData FindBySceneNameAndType(string sceneName, EnemyType enemyId)
    {
        // bool flag = false;
        // EnemyAttrData enemyAttrData = null;
        // for (int i = 0; i < DB.EnemyAttrDataList.Count; i++)
        // {
        //     if (DB.EnemyAttrDataList[i].enemyId == (int)enemyId)
        //     {
        //         flag = true;
        //         if (DB.EnemyAttrDataList[i].sceneName == sceneName)
        //         {
        //             return DB.EnemyAttrDataList[i];
        //         }
        //
        //         if (DB.EnemyAttrDataList[i].sceneName == "-1")
        //         {
        //             enemyAttrData = DB.EnemyAttrDataList[i];
        //         }
        //     }
        // }

        // if (!flag)
        // {
        //     throw new IndexOutOfRangeException("enemyId" + enemyId + "不存在");
        // }
        //
        // if (enemyAttrData != null)
        // {
        //     return enemyAttrData;
        // }

        throw new IndexOutOfRangeException();
    }

    public static EnemyAttrData SetValue(string[] strings)
    {
        return new EnemyAttrData
        {
            sceneName = strings[0],
            enemyId = int.Parse(strings[1]),
            level = int.Parse(strings[2]),
            maxHp = int.Parse(strings[3]),
            atk = int.Parse(strings[4]),
            atkSpeed = float.Parse(strings[5]),
            maxSP = int.Parse(strings[6]),
            scanSpeed = float.Parse(strings[7]),
            moveSpeed = float.Parse(strings[8]),
            flyHeight = int.Parse(strings[9]),
            counterAttack = int.Parse(strings[10]),
            counterAttackProbPercentage = int.Parse(strings[11]),
            actionInterruptPoint = int.Parse(strings[12]),
            baseDefence = int.Parse(strings[13]),
            dropCoins = int.Parse(strings[14]),
            dropExp = int.Parse(strings[15])
        };
    }
}