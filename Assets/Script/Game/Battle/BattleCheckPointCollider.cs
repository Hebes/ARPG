using UnityEngine;

/// <summary>
/// 战斗检查点碰撞
/// </summary>
public class BattleCheckPointCollider : BaseBehaviour
{
    public BattleCheckPoint parent;
    
    public void OnEnable()
    {
        parent = transform.parent.GetComponent<BattleCheckPoint>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag(CTag.Player))return;
        //没有战斗结束 //开放战场
        if (!parent.isBattleOver && !parent.isOpenBattleZone)
        {
            parent.isOpenBattleZone = true;
            parent.InitGameArea();
            GameEvent.Battle.Trigger(BattleEventArgs.Begin);//战斗开始
        }
    }
}