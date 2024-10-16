using System;
using UnityEngine;

/// <summary>
/// Ui控制器
/// </summary>
public class UIController : SMono<UIController>
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 重置
    /// </summary>
    public void Reset()
    {
        "重置".Log();
        if (UIBossHpBar.I.Visible)
            UIBossHpBar.I.Disappear();
        //R.Ui.HitsGrade.HideHitNumAndBar();
    }
    
    
    public GameObject CreateEnemyPoint(EnemyAttribute enemy)
    {
        "创建敌人点".Log();
        return null;
        // GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.enemyPoint);
        // if (gameObject != null)
        // {
        //     UIEnemyPointController component = gameObject.GetComponent<UIEnemyPointController>();
        //     component.enemy = enemy;
        // }
        // return this.enemyPoint;
    }
    
    [SerializeField] private GameObject enemyPoint;
}