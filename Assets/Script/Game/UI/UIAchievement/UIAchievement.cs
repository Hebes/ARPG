using System.Collections.Generic;
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
    private Transform parent;

    private void Awake()
    {
        Quit = transform.Find("Panel/Quit").GetComponent<UnityEngine.EventSystems.EventTrigger>();

        cg.FadeTo(0f);
        parent = transform.Find("Panel/Scroll View/Viewport/Content");
        Quit.AddEventTrigger(EventTriggerType.PointerClick, OnQuit);
        InstantiateAndGetUiTrophyItems();
    }

    /// <summary>
    /// 实例化成就Item
    /// </summary>
    private void InstantiateAndGetUiTrophyItems()
    {
        Transform child = parent.GetChild(0);
        Dictionary<int, AchievementInfo> infos = AchievementManager.I.AchievementInfoDic[AchievementManager.Language];
        foreach (int i in infos.Keys)
        {
            Transform transform2 = Instantiate<Transform>(child, parent, true);
            transform2.gameObject.SetActive(true);
            transform2.localScale = Vector3.one;
            transform2.GetComponent<UITrophyItem>().TrophyId = i;
        }
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

        for (int i = 0; i < parent.childCount; i++)
        {
            if (i == 0) continue;
            Transform child = parent.GetChild(i);
            child.GetComponent<UITrophyItem>().DataBind();
        }

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
        if (LevelManager.IsScene(CScene.InitScene01))
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