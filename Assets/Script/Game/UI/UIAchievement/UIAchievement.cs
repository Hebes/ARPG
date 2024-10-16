using Colorful;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 成就
/// </summary>
public class UIAchievement : SMono<UIAchievement>
{
    public CanvasGroup cg;
    public EventTrigger Quit;
    private AnalogTV _analogTv;

    private void Awake()
    {
        cg.FadeTo(0f);
        Quit = transform.Find("Panel/Quit").GetComponent<UnityEngine.EventSystems.EventTrigger>();
        Quit.AddEventTrigger(EventTriggerType.PointerClick, OnQuit);
    }
    
    private void OnQuit(PointerEventData obj)
    {
        Close();
    }

    private void Update()
    {
        if (Input.UI.Cancel.OnClick && cg.CgAlphaOpen())
            Close();
    }

    /// <summary>
    /// 打开
    /// </summary>
    public void Open()
    {
        //_analogTv = UICamera.I.GetComponent<AnalogTV>();
        //_analogTv.enabled = false;
        UIPause.I.Enabled = false;
        UIPause.I.canvasGroup.FadeTo(0f);
        cg.FadeTo(1f);
    }

    /// <summary>
    /// 关闭
    /// </summary>
    public void Close()
    {
        //_analogTv.enabled = true;
        UIPause.I.Enabled = true;
        if (LevelManager.SceneName.Equals(CScene.InitScene))
        {
            UIStart.I.canvasGroup.FadeTo(1f);
        }
        else
        {
            UIPause.I.canvasGroup.FadeTo(1f);
        }
        
        cg.FadeTo(0f);
    }
}