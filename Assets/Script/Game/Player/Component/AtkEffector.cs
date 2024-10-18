using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class AtkEffector : BaseBehaviour
{
    public void SetData(JsonData atkData, int atkId)
    {
        playerAtk?.SetData(atkData, atkId);
    }

    public PlayerAtk playerAtk;

    [SerializeField] public Vector3 pos;

    [SerializeField] public bool UseAtkData = true;

    [SerializeField] public bool CanHitGround;
}