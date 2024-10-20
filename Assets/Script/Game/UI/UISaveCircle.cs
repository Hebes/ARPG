using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 存档界面
/// </summary>
public class UISaveCircle : SMono<UISaveCircle>
{
    private Text Content;
    private Coroutine _coroutine;

    private void Awake()
    {
        Content = transform.Find("Content").GetComponent<UnityEngine.UI.Text>();
        Content.color = new Color(0, 0, 0, 0);
    }

    public void StartShow()
    {
        _coroutine ??= Fade().StartIEnumerator();
    }

    private IEnumerator Fade()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return DOTween.To(delegate(float a) { Content.color = new Color(0, 0, 0, a); }, 0f, 1f, 0.3f).WaitForCompletion();
            yield return DOTween.To(delegate(float a) { Content.color = new Color(0, 0, 0, a); }, 1f, 0f, 0.3f).WaitForCompletion();
        }
    }
}