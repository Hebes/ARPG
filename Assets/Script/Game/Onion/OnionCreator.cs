using UnityEngine;

/// <summary>
/// 幻影创造
/// </summary>
public class OnionCreator : MonoBehaviour
{
    private void Update()
    {
        if (!_open) return;
        GeneratorMode generatorMode = this.generatorMode;
        if (generatorMode != GeneratorMode.Time)
        {
            if (generatorMode == GeneratorMode.Distance)
            {
                if (Vector3.Distance(_lastPostion, transform.position) > rate)
                {
                    GeneratorGhost(_lastPostion, transform.position, rate);
                    _lastPostion = transform.position;
                }
            }
        }
        else
        {
            _onionTimer += Time.deltaTime;
            if (_onionTimer >= rate)
            {
                _onionTimer = 0f;
                GeneratorGhost();
            }
        }

        if (!_autoClose)
        {
            return;
        }

        if (Time.time >= _autoCloseTime)
        {
            Close();
        }
    }

    public void Open(bool _autoClose = true, float _autoCloseTime = 1f, GameObject[] cloneds = null)
    {
        if (_open)
        {
            if (!_autoClose) return;
            float num = Time.time + _autoCloseTime;
            if (num < this._autoCloseTime) return;
        }

        _needBeClones = cloneds ?? new[] { gameObject };

        _open = true;
        if (_autoClose)
        {
            this._autoClose = true;
            this._autoCloseTime = Time.time + _autoCloseTime;
        }

        switch (generatorMode)
        {
            case GeneratorMode.Time:
                switch (executionMode)
                {
                    case ExecutionMode.Immediately:
                        _onionTimer = float.MaxValue;
                        break;
                    case ExecutionMode.NextUpdate:
                        _onionTimer = 0f;
                        break;
                }

                break;
            case GeneratorMode.Distance:
                switch (executionMode)
                {
                    case ExecutionMode.NextUpdate:
                        _lastPostion = transform.position;
                        break;
                    case ExecutionMode.Immediately:
                        _lastPostion = Vector3.zero;
                        break;
                }

                break;
        }
    }

    public void Close()
    {
        _open = false;
        _autoClose = false;
    }

    private void GeneratorGhost()
    {
        if (_needBeClones == null || _needBeClones.Length == 0)
        {
            return;
        }

        CloneGhostAtPos(transform.position);
    }

    private void GeneratorGhost(Vector3 fromPos, Vector3 toPos, float rate)
    {
        if (_needBeClones == null || _needBeClones.Length == 0) return;
        float num = Vector3.Distance(fromPos, toPos) / rate;
        int num2 = 0;
        while (num2 < num)
        {
            Vector3 pos = Vector3.Slerp(fromPos, toPos, num2 / num);
            CloneGhostAtPos(pos);
            num2++;
        }
    }

    private void CloneGhostAtPos(Vector3 pos)
    {
        int type = 1;
        switch (type)
        {
            case 1:
                for (int i = 0; i < _needBeClones.Length; i++)
                {
                    GameObject gameObject = _needBeClones[i];
                    if (gameObject.activeSelf)
                    {
                        Transform transform = R.Effect.Generate(_onionId, null, pos + new Vector3(0f, 0f, 0.001f), Vector3.zero);
                        _ghostParent ??= new GameObject("GhostParent").transform;
                        _ghostParent.parent = R.Effect.transform;
                        transform.parent = _ghostParent;
                        transform.localScale = this.transform.localScale;
                        OnionAnimController component2 = transform.GetComponent<OnionAnimController>();
                        component2.Clone(gameObject);
                        component2.SetDisappear(disappearCurve, disappearTime);
                    }
                }

                break;
            case 2:
                for (int i = 0; i < _needBeClones.Length; i++)
                {
                    GameObject gameObject = _needBeClones[i];
                    MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
                    if (gameObject.activeSelf && component.enabled)
                    {
                        Transform transform = R.Effect.Generate(_onionId, null, pos + new Vector3(0f, 0f, 0.001f), Vector3.zero);
                        if (_ghostParent == null)
                        {
                            _ghostParent = new GameObject("GhostParent").transform;
                            _ghostParent.parent = R.Effect.transform;
                        }

                        transform.parent = _ghostParent;
                        transform.localScale = this.transform.localScale;
                        OnionController component2 = transform.GetComponent<OnionController>();
                        component2.Clone(gameObject);
                        component2.SetDisappear(disappearCurve, disappearTime);
                    }
                }

                break;
        }
    }

    private static Transform _ghostParent;

    public float rate = 0.1f;

    public float disappearTime = 1f;

    public AnimationCurve disappearCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

    private bool _open;

    private bool _autoClose;

    private float _autoCloseTime = 1f;

    public GeneratorMode generatorMode = GeneratorMode.Time;

    public ExecutionMode executionMode;

    [SerializeField] private int _onionId = 218;

    private float _onionTimer = 10f;

    private Vector3 _lastPostion;

    private GameObject[] _needBeClones;

    public enum ExecutionMode
    {
        Immediately,
        NextUpdate
    }

    public enum GeneratorMode
    {
        Distance,
        Time
    }

    public void InitAwake()
    {
        
    }
}