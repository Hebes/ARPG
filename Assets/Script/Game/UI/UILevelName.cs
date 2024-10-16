using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 关卡名称
/// </summary>
public class UILevelName : SMono<UILevelName>
{
    private CanvasGroup cg1;
    private Text Text;

    private void Awake()
    {
        cg1 = transform.Find("Panel").GetComponent<CanvasGroup>();
        Text = transform.Find("Panel/Text").GetComponent<Text>();
        cg1.FadeTo(0,false);
    }

    /// <summary>
    /// 显示
    /// </summary>
    /// <param name="levelName">关卡名称</param>
    /// <param name="fontSize">字段大小</param>
    /// <param name="duration">持续时间</param>
    /// <returns></returns>
    public Coroutine Show(string levelName, int fontSize, float duration)
    {
        return StartCoroutine(ShowCoroutine(levelName, fontSize, duration));
    }

    private IEnumerator ShowCoroutine(string levelName, int fontSize, float duration)
    {
        cg1.alpha = 0f;
        Text.text = levelName;
        Text.fontSize = fontSize;
        cg1.gameObject.SetActive(true);
        yield return DOTween.To(() => cg1.alpha, delegate(float a) { cg1.alpha = a; }, 1f, duration).WaitForCompletion();
        yield return new WaitForSeconds(1f);
        yield return DOTween.To(() => cg1.alpha, delegate(float a) { cg1.alpha = a; }, 0f, duration).WaitForCompletion();
        cg1.gameObject.SetActive(false);
    }
}