using UnityEngine;

/// <summary>
/// 游戏区
/// </summary>
[ExecuteInEditMode]
public class GameArea : SingletonMono<GameArea>
{
    [HideInInspector] public static Rect MapRange;
    [HideInInspector] public static Rect CameraRange;
    [HideInInspector] public static Rect PlayerRange;
    [HideInInspector] public static Rect EnemyRange;
    [Header("显示地图")] [SerializeField] private bool _showMap;
    [Header("地图范围")] [SerializeField] private Color _mapRangeColor = Color.red;
    [Header("显示摄像机")] [SerializeField] private bool _showCamera;
    [Header("相机范围颜色")] [SerializeField] private Color _cameraRangeColor = Color.green;
    [Header("显示玩家")] [SerializeField] private bool _showPlayer;
    [Header("玩家范围颜色")] [SerializeField] private Color _playerRangeColor = Color.cyan;
    [Header("显示敌人")] [SerializeField] private bool _showEnemy;
    [Header("敌人范围颜色")] [SerializeField] private Color _enemyRangeColor = Color.yellow;
    [Header("左下角")] [SerializeField] private Transform bottomLeft;
    [Header("右上角")] [SerializeField] private Transform TopRight;

    public void Awake() => Init();

    [ContextMenu("Init")]
    private void Init()
    {
        MapRange.min = bottomLeft.position;
        MapRange.max = TopRight.position;
        CameraRange = MapRange;
        PlayerRange.min = new Vector2(CameraRange.min.x - 2f, CameraRange.min.y);
        PlayerRange.max = new Vector2(CameraRange.max.x + 2f, CameraRange.max.y);
        EnemyRange = CameraRange;
    }

    public void OnDrawGizmos()
    {
        if (_showCamera)
            DebugX.DrawRect(CameraRange, _cameraRangeColor);
        if (_showPlayer)
            DebugX.DrawRect(PlayerRange, _playerRangeColor);
        if (_showEnemy)
            DebugX.DrawRect(EnemyRange, _enemyRangeColor);
        if (_showMap)
            DebugX.DrawRect(MapRange, _mapRangeColor);
    }
}