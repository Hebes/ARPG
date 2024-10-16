using UnityEngine;

/// <summary>
/// 幻影控制器
/// </summary>
public class OnionAnimController : MonoBehaviour
{
    private void OnEnable()
    {
        if (_meshFilter == null)
            _meshFilter = GetComponent<SpriteRenderer>();
        _startColor = tint;
    }

    private void OnDisable()
    {
        _disappearTime = 1f;
        _disappearTimer = 0f;
        _meshFilter = null;
        tint = _startColor;
    }

    private void Update()
    {
        if (_curve == null)
        {
            _curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            _disappearTime = 1f;
        }

        _scaleCurve ??= AnimationCurve.Linear(0f, 1f, 1f, 1f);

        _disappearTimer += Time.deltaTime;
        float num = Mathf.Clamp01(_curve.Evaluate(_disappearTimer / _disappearTime));
        //tint.a *= num;
        float d = Mathf.Clamp01(_scaleCurve.Evaluate(_disappearTimer / _disappearTime));
        // Vector3 localScale = _startScale * d;
        // transform.localScale = localScale;
        Refresh();
        if (_disappearTimer > _disappearTime)
        {
            EffectController.TerminateEffect(gameObject);
        }
    }

    public void Clone(GameObject game)
    {
        //_startScale = transform.localScale;
        _meshFilter ??= GetComponent<SpriteRenderer>();
        //_meshFilter.sprite = game.GetComponent<SpriteRenderer>().sprite;
    }

    /// <summary>
    /// 刷新
    /// </summary>
    private void Refresh()
    {
        _meshFilter.color = tint;
    }

    public void SetDisappear(AnimationCurve disappearCurve, float disappearTime)
    {
        _curve = (_curve == null ? AnimationCurve.Linear(0f, 1f, 1f, 0f) : disappearCurve);
        _disappearTime = disappearTime;
    }

    /// <summary>
    /// 底色
    /// </summary>
    public Color tint = new Color(0f, 0f, 0.5f, 0.5f);

    /// <summary>
    /// 图片
    /// </summary>
    private SpriteRenderer _meshFilter;

    /// <summary>
    /// 曲线
    /// </summary>
    public AnimationCurve _curve;

    /// <summary>
    /// 规模曲线
    /// </summary>
    public AnimationCurve _scaleCurve;

    /// <summary>
    /// 开始比例
    /// </summary>
    private Vector3 _startScale;

    /// <summary>
    /// 消失的时间
    /// </summary>
    private float _disappearTime;

    /// <summary>
    /// 消失的计时器
    /// </summary>
    private float _disappearTimer;

    /// <summary>
    /// 开始颜色
    /// </summary>
    private Color _startColor;
}