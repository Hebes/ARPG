using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Boss血条
/// </summary>
public class UIBossHpBar : SMono<UIBossHpBar>
{
    [Header("boss属性")] private static EnemyAttribute boss;
    [Header("boss数据")] private BossHpBarData bossData;
    [SerializeField] private CanvasGroup Panel;
    [SerializeField] private Text _numLabel;
    [SerializeField] private RectTransform hp1Bar;
    [SerializeField] private RectTransform hp2Bar;
    [SerializeField] private CanvasGroup cursorSprite;
    private CanvasGroup cursorTweenAlpha;
    [Header("是否播放")] private bool play;
    [Header("是否可见")] public bool Visible;
    [Header("历史值")] private readonly Queue<float> hpValueHistory = new Queue<float>();
    [Header("上次播放动画时的hp值")] private float hpValueWhenLastPlayAnim = 1f;

    /// <summary>
    /// Boss血条数据
    /// </summary>
    public class BossHpBarData
    {
        public BossHpBarData(List<int> phaseHp)
        {
            MaxHps = phaseHp;
            HpBarCount = phaseHp.Count;
        }

        public readonly int HpBarCount;
        public readonly List<int> MaxHps;
        public int CurrentHpBarIndex;
        public int CurrentAppearHp;
        public int FullMaxHp => boss.maxHp;
        public int FullCurrentHp => boss.currentHp;

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            int num = 0;
            for (int i = HpBarCount - 1; i >= 0; i--)
            {
                num += MaxHps[i];
                if (FullCurrentHp <= num)
                {
                    CurrentHpBarIndex = i;
                    CurrentAppearHp = FullCurrentHp - (num - MaxHps[i]);
                    break;
                }
            }
        }
    }


    private void Awake()
    {
        if (!LevelManager.SceneName.Equals(CScene.InitScene01))return;
        cursorTweenAlpha = cursorSprite;
        StartCoroutine(HPValueHistoryRecorder());
        StartCoroutine(HPChangeAnim());
    }

    private void Update()
    {
        if (boss == null) return;
        BindData();
        if (bossData.FullCurrentHp <= 0)
        {
            Disappear();
        }
    }

    /// <summary>
    /// 生命值改变动画
    /// </summary>
    /// <returns></returns>
    private IEnumerator HPChangeAnim()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.02f);
        for (;;)
        {
            if (play)
            {
                play = false;
                cursorTweenAlpha.enabled = true;
                cursorSprite.enabled = true;
                float start = hp2Bar.localScale.x;
                float targe = hp1Bar.localScale.x;
                for (float x = 0f; x < 1f; x += 0.02f)
                {
                    float current = start + (targe - start) * x;
                    hp2Bar.localScale = new Vector3(hp2Bar.localScale.x, hp2Bar.localScale.y, current);
                    yield return waitForSeconds;
                }

                cursorTweenAlpha.enabled = false;
                cursorSprite.alpha = 0f;
            }

            yield return null;
        }

        yield break;
    }

    /// <summary>
    /// HP值历史记录仪
    /// </summary>
    /// <returns></returns>
    private IEnumerator HPValueHistoryRecorder()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
        for (;;)
        {
            hpValueHistory.Enqueue(hp1Bar.localScale.x);
            if (hpValueHistory.Count > 10)
                hpValueHistory.Dequeue();
            yield return waitForSeconds;
        }

        yield break;
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    private void BindData()
    {
        // bossData.Update();
        // hp1Bar.value = bossData.CurrentAppearHp / Math.Max(1f, bossData.MaxHps[bossData.CurrentHpBarIndex]);
        // hp1Bar.value = (float)bossData.CurrentAppearHp / Mathf.Max(bossData.MaxHps[bossData.CurrentHpBarIndex], 1);
        // if (hpValueHistory.Count >= 10 && Mathf.Abs(hp1Bar.value - hpValueHistory.Peek()) < 0.0001 &&
        //     Mathf.Abs(hp1Bar.value - hpValueWhenLastPlayAnim) > 0.0001)
        // {
        //     hpValueWhenLastPlayAnim = hp1Bar.value;
        //     play = true;
        // }
        //
        // if (Math.Abs(hp1Bar.value - 1f) < 0.0001)
        // {
        //     hp2Bar.value = 1f;
        //     cursorTweenAlpha.enabled = false;
        //     cursorSprite.alpha = 0f;
        //     hpValueWhenLastPlayAnim = 1f;
        // }
        //
        // int num = bossData.HpBarCount - bossData.CurrentHpBarIndex;
        // //_numLabel.alpha = (float)(num != 1 ? 1 : 0);
        // _numLabel.text = StringTools.Int2String[bossData.HpBarCount - bossData.CurrentHpBarIndex];
    }

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="phaseHp"></param>
    public void Create(EnemyAttribute enemy, List<int> phaseHp)
    {
        if (!Visible)
        {
            boss = enemy;
            bossData = new BossHpBarData(phaseHp);
            FadeTo(1f, 1f, delegate { Visible = true; });
        }
        else
        {
            "重复生成Boss血条".Error();
        }
    }

    /// <summary>
    /// 消失
    /// </summary>
    public void Disappear()
    {
        if (Visible)
        {
            Visible = false;
            FadeTo(0f, 1f);
            UIBossHpBar.boss = null;
            StopCoroutine(HPValueHistoryRecorder());
            StopCoroutine(HPChangeAnim());
        }
        else
        {
            "重复隐藏Boss血条".Error();
        }
    }

    public YieldInstruction FadeTo(float endValue, float duration)
    {
        return DOTween.To(() => Panel.alpha, delegate(float alpha) { Panel.alpha = alpha; }, endValue, duration).WaitForCompletion();
    }

    public YieldInstruction FadeTo(float endValue, float duration, TweenCallback onComplete)
    {
        return DOTween.To(() => Panel.alpha, delegate(float alpha) { Panel.alpha = alpha; }, endValue, duration).OnComplete(onComplete)
            .WaitForCompletion();
    }
}