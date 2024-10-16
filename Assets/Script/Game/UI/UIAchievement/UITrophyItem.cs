using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 奖杯Item
/// </summary>
public class UITrophyItem : MonoBehaviour
{
    private int _trophyId;
    
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private Image _trophyIcon;
    [SerializeField] private Text _trophyName;
    [SerializeField] private Text _trophyDetail;

    /// <summary>
    /// 奖杯ID
    /// </summary>
    public int TrophyId
    {
        get => _trophyId;
        set
        {
            _trophyId = value;
            DataBind();
        }
    }

    public Sprite TrophyIconSprite
    {
        get => _trophyIcon.sprite;
        set => _trophyIcon.sprite = value;
    }

    /// <summary>
    /// 奖杯名称
    /// </summary>
    public string TrophyName
    {
        get => _trophyName.text;
        set => _trophyName.text = value;
    }

    /// <summary>
    /// 奖杯描述
    /// </summary>
    public string TrophyDetail
    {
        get => _trophyDetail.text;
        set => _trophyDetail.text = value;
    }

    private void OnEnable()
    {
        //DataBind();
    }

    /// <summary>
    /// 数据绑定
    /// </summary>
    private void DataBind()
    {
        AchievementInfo achievementInfo = AchievementManager.I.GetAchievementInfo(TrophyId);//成就信息
        bool achievementUnlockState = AchievementManager.I.GetAchievementUnlockState(TrophyId);//是否解锁成就
        TrophyName = achievementInfo.Name;
        TrophyDetail = achievementInfo.Detail;
        //TrophyIconSprite =  $"{TrophyId}{(!achievementUnlockState ? "_l" : string.Empty)}";
        cg.alpha = achievementUnlockState ? 1f : 0.5f;
    }
}