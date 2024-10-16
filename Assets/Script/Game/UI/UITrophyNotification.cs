using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 奖杯通知
/// </summary>
public class UITrophyNotification : SMono<UITrophyNotification>
{
    [SerializeField] private CanvasGroup Panel;
    [SerializeField] private Text trophyName;
    [SerializeField] private Image trophyIcon;

    private bool _isPlaying;
    private readonly Queue<Trophy> _trophyQueue = new Queue<Trophy>();
    private float y;

    private void Awake()
    {
        trophyName = transform.Find("Panel/trophyName").GetComponent<UnityEngine.UI.Text>();
        trophyIcon = transform.Find("Panel/trophyIcon").GetComponent<UnityEngine.UI.Image>();
        Panel = transform.Find("Panel").GetComponent<UnityEngine.CanvasGroup>();
        y = (ResolutionOption.I.Resolution.height / 2f) - ((RectTransform)Panel.transform).rect.height;
    }

    /// <summary>
    /// 奖杯显示入口
    /// </summary>
    /// <param name="trophyName"></param>
    /// <param name="spriteName"></param>
    public void AwardTrophy(string trophyName, string spriteName)
    {
        _trophyQueue.Enqueue(new Trophy(trophyName, spriteName));
        if (!_isPlaying)
            AwardTrophy();
    }

    /// <summary>
    /// 奖的奖杯
    /// </summary>
    /// <returns></returns>
    private Coroutine AwardTrophy()
    {
        Trophy trophy = _trophyQueue.Dequeue();
        trophyName.text = trophy.TrophyName;
        //_trophyIcon.sprite = trophy.SpriteName;
        Panel.FadeTo(1, false);
        Panel.transform.localPosition = new Vector3(0f, ResolutionOption.I.Resolution.height / 2f, 0f);
        return StartCoroutine(AwardTrophyCoroutine());
    }

    /// <summary>
    /// 奖项奖杯协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator AwardTrophyCoroutine()
    {
        _isPlaying = true;
        yield return Panel.transform.DOLocalMoveY(y, 0.5f).WaitForCompletion();
        yield return new WaitForSeconds(1f);
        yield return Panel.DOFade(0f, 0.5f).WaitForCompletion();
        _isPlaying = false;
        if (_trophyQueue.Count != 0)
            AwardTrophy();
    }
}

/// <summary>
/// 奖杯
/// </summary>
public class Trophy
{
    public Trophy(string trophyName, string spriteName)
    {
        TrophyName = trophyName;
        SpriteName = spriteName;
    }

    /// <summary>
    /// 奖杯名称
    /// </summary>
    public string TrophyName { get; set; }

    /// <summary>
    /// 图片名称
    /// </summary>
    public string SpriteName { get; set; }
}