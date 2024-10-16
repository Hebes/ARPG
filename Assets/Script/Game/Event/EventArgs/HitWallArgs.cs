using System;
using UnityEngine;

/// <summary>
/// 击穿地面事件参数
/// </summary>
public class HitWallArgs : EventArgs
{
    public GameObject who;

    public HitWallArgs(GameObject who)
    {
        this.who = who;
    }
}