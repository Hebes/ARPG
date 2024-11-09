using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private PlayerAttribute Attribute => R.Player.Attribute;
    public GameData GameData => R.GameData;

    private void Awake()
    {
        hp1Bar = transform.Find("Panel/HPBG/hp1Bar").GetComponent<UnityEngine.UI.Image>();
        hp2Bar = transform.Find("Panel/HPBG/hp2Bar").GetComponent<UnityEngine.UI.Image>();
        _roleNameLabel = transform.Find("Panel/role_name").GetComponent<UnityEngine.UI.Text>();
    }

    private void Start()
    {
        _uiNeedHide = new CanvasGroup[2];
        _uiNeedHide[0] = transform.Find("Panel").GetComponent<CanvasGroup>();
        _uiNeedHide[1] = UIOptions.I.canvasGroup;
        foreach (var c in _uiNeedHide)
            c.FadeTo(0, false);
        
        StartCoroutine(HPValueHistoryRecorder());
        StartCoroutine(HPChangeAnim());
    }

    private void Update()
    {
        //更新气血
        if (Attribute != null)
        {
            UpdateHpBarShape();
            UpdateCharInfo();
        }
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
                c.FadeTo(1);
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

    private void UpdateHpBarShape()
    {
        "更新血条值的外形".Log();
        // if (this._lastPlayerMaxHP != R.Player.Attribute.maxHP)
        // {
        //     this._lastPlayerMaxHP = R.Player.Attribute.maxHP;
        //     int num = (int)((double)this._lastPlayerMaxHP * 2.48);
        //     this._hp1Sprite.width = num;
        //     this._hp2Sprite.width = num;
        //     this._hpBgSprite.width = num + 56;
        // }
    }
    
    private void UpdateCharInfo()
    {
        float num = (float)Attribute.currentHP / (float)Mathf.Max(Attribute.maxHP, 1); 
        if (Math.Abs(hp1Bar.fillAmount - num) > 1.401298E-45f)
        {
            hp1Bar.fillAmount = num;
        }

        if (hpValueHistory.Count >= 10 &&
            Mathf.Abs(hp1Bar.fillAmount - hpValueHistory.Peek()) < 0.0001 &&
            Mathf.Abs(hp1Bar.fillAmount - hpValueWhenLastPlayAnim) > 0.0001)
        {
            hpValueWhenLastPlayAnim = hp1Bar.fillAmount;
            play = true;
        }

        if (Attribute.currentHP == 0)
        {
            hpValueWhenLastPlayAnim = hp1Bar.fillAmount;
            play = true;
        }

        if (energyController)
        {
            "设置护盾值".Log();
            energyController.EnergyMaxValue = Attribute.maxEnergy * 2;
            energyController.EnergyValue = Attribute.currentEnergy * 2;
        }
        
        if (_roleName != GameData.RoleName)
        {
            _roleName = GameData.RoleName;
            _roleNameLabel.text = _roleName;
        }
    }

    private IEnumerator HPValueHistoryRecorder()
    {
        for (;;)
        {
            hpValueHistory.Enqueue(hp1Bar.fillAmount);
            if (hpValueHistory.Count > 10)
            {
                hpValueHistory.Dequeue();
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
    
    private IEnumerator HPChangeAnim()
    {
        for (;;)
        {
            if (play)
            {
                play = false;
               
                float start = hp2Bar.fillAmount;
                float targe = hp1Bar.fillAmount;
                for (float x = 0f; x < 1f; x += 0.02f)
                {
                    float current = start + (targe - start) * x;
                    hp2Bar.fillAmount = current;
                    yield return new WaitForSeconds(0.02f);
                }
            }

            yield return null;
        }

        yield break;
    }

    [SerializeField] private Image hp2Bar;
    [SerializeField] private Image hp1Bar;
    [SerializeField] private Text _roleNameLabel;
    private bool play;
    private string _roleName;
    private UIEnergyController energyController;

    /// <summary>
    /// 历史生命值
    /// </summary>
    private readonly Queue<float> hpValueHistory = new Queue<float>();

    /// <summary>
    /// 上次播放动画时的hp值
    /// </summary>
    private float hpValueWhenLastPlayAnim = 1f;
}