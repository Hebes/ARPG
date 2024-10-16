using UnityEngine;

/// <summary>
/// 路牌
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Guideboard : MonoBehaviour
{
    /// <summary>
    /// 是否已经显示
    /// </summary>
    private bool HasShown { get; set; }

    /// <summary>
    /// 关卡名称
    /// </summary>
    [SerializeField] private string _levelName = "请填写名称";

    /// <summary>
    /// 默认尺寸
    /// </summary>
    [SerializeField] private int _normalSize = 85;

    /// <summary>
    /// 持续时间
    /// </summary>
    [SerializeField] private float _duration = 1f;

    private void Start()
    {
        //_levelName = ScriptLocalization.Get("ui/levelName/" + LevelManager.SceneName);
        if (_normalSize == 144)
            _normalSize = 85;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(CTag.Player)) return;
        if (HasShown) return;
        UILevelName.I.Show(_levelName, _normalSize, _duration);
        HasShown = true;
    }
}