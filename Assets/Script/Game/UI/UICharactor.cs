using System.Collections;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 角色界面
/// </summary>
public class UICharactor : SMono<UICharactor>
{
    /// <summary>
    /// 是否是关闭状态
    /// </summary>
    public bool IsHide;

    [SerializeField] private CanvasGroup[] _uiNeedHide;

    private void Start()
    {
        _uiNeedHide = new CanvasGroup[2];
        _uiNeedHide[0] = transform.Find("HP").GetComponent<CanvasGroup>();
        _uiNeedHide[1] = UIOptions.I.canvasGroup;
        foreach (var c in _uiNeedHide)
            c.FadeTo(0, false);
    }

    /// <summary>
    /// 打开
    /// </summary>
    /// <param name="immediately">是否马上显示</param>
    /// <returns></returns>
    public YieldInstruction ShowUI(bool immediately = false)
    {
        if (immediately)
        {
            foreach (var c in _uiNeedHide)
                c.FadeTo(1, true);
            IsHide = false;
            return null;
        }

        return StartCoroutine(IFadeInAnim());
    }

    /// <summary>
    /// 关闭
    /// </summary>
    /// <param name="immediately"></param>
    /// <returns></returns>
    public YieldInstruction HideUI(bool immediately = false)
    {
        if (immediately)
        {
            for (int i = 0; i < _uiNeedHide.Length; i++)
                _uiNeedHide[i].FadeTo(0);
            IsHide = true;
            return null;
        }

        return StartCoroutine(IFadeOutAnim());
    }

    private IEnumerator IFadeInAnim()
    {
        for (int i = 0; i < _uiNeedHide.Length; i++)
            _uiNeedHide[i].FadeTo(1f, 0.5f, true);
        yield return WorldTime.WaitForSecondsIgnoreTimeScale(0.5f);
        IsHide = false;
    }

    private IEnumerator IFadeOutAnim()
    {
        IsHide = true;
        for (int i = 0; i < _uiNeedHide.Length; i++)
            _uiNeedHide[i].FadeTo(0f, 0.5f, false);
        yield return WorldTime.WaitForSecondsIgnoreTimeScale(0.5f);
    }

    // private void FadeTo(CanvasGroup widget, float endValue, float duration)
    // {
    //     DOTween.To(() => widget.alpha, delegate(float alpha)
    //     {
    //         widget.alpha = alpha;
    //         
    //     }, endValue, duration);
    // }
}