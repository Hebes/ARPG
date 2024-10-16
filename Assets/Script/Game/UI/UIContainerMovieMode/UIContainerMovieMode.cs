using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 电影模式
/// </summary>
public class UIContainerMovieMode : SMono<UIContainerMovieMode>
{
    [SerializeField] private RectTransform up;
    [SerializeField] private RectTransform down;

    private void Awake()
    {
        OnReset();
    }

    public void Set()
    {
        Vector3 temp1 = up.anchoredPosition;
        temp1.y = -Mathf.Abs(temp1.y);
        up.DOAnchorPos(temp1, 1);
        
        Vector3 temp2 = down.anchoredPosition;
        temp2.y = Mathf.Abs(temp2.y);
        down.DOAnchorPos(temp2, 1);
    }

    private void OnReset()
    {
        Vector3 temp1 = up.anchoredPosition;
        temp1.y = Mathf.Abs(temp1.y);
        up.anchoredPosition = temp1;

        Vector3 temp2 = down.anchoredPosition;
        temp2.y = -Mathf.Abs(temp2.y);
        down.anchoredPosition = temp2;
    }
}