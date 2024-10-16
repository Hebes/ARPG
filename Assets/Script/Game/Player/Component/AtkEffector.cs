using System.Collections.Generic;
using UnityEngine;

public class AtkEffector : BaseBehaviour
{
    public void SetData(Dictionary<PlayerAtkDataType, string> atkData, int atkId)
    {
        playerAtk?.SetData(atkData, atkId);
    }

    public PlayerAtk playerAtk;

    [SerializeField] public Vector3 pos;

    [SerializeField] public bool UseAtkData = true;

    [SerializeField] public bool CanHitGround;
}