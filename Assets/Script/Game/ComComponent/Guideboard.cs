using UnityEngine;

/// <summary>
/// 路牌
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Guideboard : MonoBehaviour
{
    [Header("是否已经显示")] private bool _hasShown;
    [Header("关卡名称")] [SerializeField] private string levelName = "请填写名称";
    [Header("默认大小")] [SerializeField] private int normalSize = 85;
    [Header("持续时间")] [SerializeField] private float duration = 1f;

    private void Start()
    {
        levelName = DB.GuideboardNameDic[LevelManager.SceneName];
        //_levelName = ScriptLocalization.Get("ui/levelName/" + LevelManager.SceneName);
        if (normalSize == 144)
            normalSize = 85;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hasShown) return;
        if (!collision.CompareTag(ConfigTag.Player)) return;
        UILevelName.I.Show(levelName, normalSize, duration);
        _hasShown = true;
    }
}