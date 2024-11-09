using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 辅助介绍界面
/// </summary>
public class UITutorial : SMono<UITutorial>
{
    private List<CanvasGroup> _tutorial=new List<CanvasGroup>();
    
    private bool Tutorial1
    {
        get => RoundStorage.Get("Tutorial1", true);
        set => RoundStorage.Set("Tutorial1", value);
    }

    private void Awake()
    {
        Transform panel = transform.Find("Panel");
        int count = panel.childCount;
        for (int i = 0; i < count; i++)
            _tutorial.Add(panel.GetChild(i).GetComponent<CanvasGroup>());
        Kill();
    }

    public void Kill()
    {
        foreach (var canvasGroup in _tutorial)
            canvasGroup.gameObject.SetActive(false);
        UITutorial.I.ShowTutorial().StopIEnumerator();
    }

    public IEnumerator ShowTutorial()
    {
        if (Tutorial1)
        {
            WaitForSeconds waitForSeconds1 = new WaitForSeconds(5);
            yield return UITutorial.I.Show(0);
            yield return waitForSeconds1;
            yield return UITutorial.I.Hide(0);
            yield return new WaitForSeconds(2);
            yield return UITutorial.I.Show(1);
            yield return waitForSeconds1;
            yield return UITutorial.I.Hide(1);
            Tutorial1 = false;
        }
    }

    private YieldInstruction Show(int id)
    {
        if (!_tutorial[id].gameObject.activeInHierarchy)
        {
            _tutorial[id].gameObject.SetActive(true);
            _tutorial[id].alpha = 0f;
            return FadeIn(_tutorial[id]);
        }

        return FadeOut(_tutorial[id]);
    }

    public YieldInstruction Hide(int? id = null)
    {
        return StartCoroutine(HideCoroutine(id));
    }

    private IEnumerator HideCoroutine(int? id)
    {
        CanvasGroup widget = _tutorial[id.Value];
        if (widget.gameObject.activeInHierarchy && widget.alpha > 0f)
        {
            yield return FadeOut(widget);
            widget.gameObject.SetActive(false);
        }
    }

    private YieldInstruction FadeIn(CanvasGroup target, float during = 1f, bool ignoreTimeScale = false)
    {
        return FadeTo(target, 1f, during, ignoreTimeScale);
    }

    private YieldInstruction FadeOut(CanvasGroup target, float during = 1f, bool ignoreTimeScale = false)
    {
        return FadeTo(target, 0f, during, ignoreTimeScale);
    }

    private YieldInstruction FadeTo(CanvasGroup target, float endValue, float during, bool ignoreTimeScale)
    {
        return DOTween.To(() => target.alpha, delegate(float alpha) { target.alpha = alpha; }, endValue, during).SetUpdate(ignoreTimeScale).WaitForCompletion();
    }
}