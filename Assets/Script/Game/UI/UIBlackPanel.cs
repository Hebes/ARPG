using DG.Tweening;
using UnityEngine;

/// <summary>
/// 通过门时候需要黑一下
/// </summary>
public class UIBlackPanel : SMono<UIBlackPanel>
{
    public CanvasGroup cg;

    /// <summary>
    /// 透明
    /// </summary>
    /// <param name="during">在时间内</param>
    /// <param name="ignoreTimeScale">忽略时间尺度</param>
    /// <returns></returns>
    public YieldInstruction FadeTransparent(float during = 0.3f, bool ignoreTimeScale = false)
    {
        return FadeTo(0f, during, ignoreTimeScale);
    }

    /// <summary>
    /// 变黑
    /// </summary>
    /// <param name="during">在时间内</param>
    /// <param name="ignoreTimeScale">忽略时间尺度</param>
    /// <returns></returns>
    public YieldInstruction FadeBlack(float during = 0.3f, bool ignoreTimeScale = false)
    {
        return FadeTo(1f, during, ignoreTimeScale);
    }

    /// <summary>
    /// 逐渐失去
    /// </summary>
    /// <param name="endValue"></param>
    /// <param name="during">在时间内</param>
    /// <param name="ignoreTimeScale">忽略时间尺度</param>
    /// <returns></returns>
    private YieldInstruction FadeTo(float endValue, float during, bool ignoreTimeScale = false)
    {
        return DOTween.To(() => cg.alpha, delegate(float alpha) { cg.alpha = alpha; }, endValue, during).SetUpdate(ignoreTimeScale)
            .WaitForCompletion();
    }

    public void Kill()
    {
        cg.DOKill(false);
    }
}