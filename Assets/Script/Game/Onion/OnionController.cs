using UnityEngine;

/// <summary>
/// 幻影控制器
/// </summary>
public class OnionController : MonoBehaviour
{
    private void OnEnable()
    {
        if (_meshFilter != null)
            _meshFilter.sharedMesh = null;

        if (_meshFilter == null)
            _meshFilter = GetComponent<MeshFilter>();
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
        if (_materials == null)return;
        if (_curve == null)
        {
            _curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            _disappearTime = 1f;
        }

        _scaleCurve ??= AnimationCurve.Linear(0f, 1f, 1f, 1f);

        _disappearTimer += Time.deltaTime;
        float num = Mathf.Clamp01(_curve.Evaluate(_disappearTimer / _disappearTime));
        tint.a *= num;
        float d = Mathf.Clamp01(_scaleCurve.Evaluate(_disappearTimer / _disappearTime));
        Vector3 localScale = _startScale * d;
        transform.localScale = localScale;
        Refresh();
        if (_disappearTimer > _disappearTime)
        {
            EffectController.TerminateEffect(gameObject);
        }
    }

    public void Clone(GameObject game)
    {
        _startScale = transform.localScale;
        Mesh mesh = game.GetComponent<MeshFilter>().mesh;
        _materials = game.GetComponent<Renderer>().materials;
        GetComponent<Renderer>().materials = _materials;
        if (_meshFilter == null)
        {
            _meshFilter = GetComponent<MeshFilter>();
        }

        _meshFilter.mesh = Instantiate(mesh);
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].shader = shader;
            _materials[i].SetFloat("_EmissionStrength", EmissionStrength);
            _materials[i].SetColor("_Color", tint);
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetColor("_Color", tint);
        }
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
    /// 散发强度
    /// </summary>
    public float EmissionStrength;

    /// <summary>
    /// 网状过滤器
    /// </summary>
    private MeshFilter _meshFilter;

    /// <summary>
    /// shader
    /// </summary>
    public Shader shader;

    /// <summary>
    /// 材质
    /// </summary>
    private Material[] _materials;

    /// <summary>
    /// 曲线
    /// </summary>
    private AnimationCurve _curve;

    /// <summary>
    /// 规模曲线
    /// </summary>
    private AnimationCurve _scaleCurve;

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