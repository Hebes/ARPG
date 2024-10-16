using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    [Header("背景")] public Image foreground;
    [Header("当前")] public Image mFG;
    [Header("充当指标的东西")] public Transform thumb;
    [HideInInspector] [SerializeField] protected float mValue = 1f;
    public int numberOfSteps = 0;
    public List<EventDelegate> onChange = new List<EventDelegate>();
    [SerializeField] private FillDirection mFill = FillDirection.LeftToRight;

    public float value
    {
        get
        {
            if (numberOfSteps > 1) return Mathf.Round(mValue * (numberOfSteps - 1)) / (numberOfSteps - 1);
            return mValue;
        }
        set => Set(value);
    }

    public bool GetActive(Behaviour mb)
    {
        return mb && mb.enabled && mb.gameObject.activeInHierarchy;
    }

    public void Set(float val)
    {
        val = Mathf.Clamp01(val);

        if (mValue != val)
        {
            float before = value;
            mValue = val;

            if (before != value)
            {
                ForceUpdate();
            }
        }
    }

    protected bool isInverted => (mFill == FillDirection.RightToLeft || mFill == FillDirection.TopToBottom);

    /// <summary>
    /// 强制更新
    /// </summary>
    public void ForceUpdate()
    {
        
       
    }
    protected bool isHorizontal => mFill is FillDirection.LeftToRight or FillDirection.RightToLeft;

    public enum FillDirection
    {
        LeftToRight,
        RightToLeft,
        BottomToTop,
        TopToBottom,
    }
}