using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 效果控制器
/// </summary>
public class EffectController : SMono<EffectController>
{
    /// <summary>
    /// fx序列化数据
    /// </summary>
    [SerializeField] private List<EffectAttr> _fxSerializedData;

    /// <summary>
    /// fx数据
    /// </summary>
    private Dictionary<int, EffectAttr> _fxData;

    /// <summary>
    /// 对象池字典
    /// </summary>
    private Dictionary<string, ObjectPool> _objectPoolDict;

    /// <summary>
    /// 允许预加载
    /// </summary>
    public static bool AllowPreload = true;

    public List<EffectAttr> fxSerializedData
    {
        get => _fxSerializedData;
        set
        {
            _fxSerializedData = value;
            _fxData = null;
        }
    }

    public Dictionary<int, EffectAttr> fxData
    {
        get
        {
            if (_fxData == null)
            {
                _fxData = new Dictionary<int, EffectAttr>();
                for (int i = 0; i < _fxSerializedData.Count; i++)
                {
                    EffectAttr effectAttr = _fxSerializedData[i];
                    _fxData.Add(effectAttr.id, effectAttr);
                }
            }

            return _fxData;
        }
    }

    private void Start()
    {
        _fxSerializedData = DB.EffectAttrList;
        if (AllowPreload)
            Preload();
    }

    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="effectId">效果id</param>
    /// <param name="target">目标</param>
    /// <param name="position">位置</param>
    /// <param name="rotation">旋转</param>
    /// <param name="scale">大小</param>
    /// <param name="useFxZNum">使用特效Z轴尺寸</param>
    /// <returns></returns>
    public Transform Generate(int effectId, Transform target = null, Vector3 position = default(Vector3), Vector3 rotation = default(Vector3),
        Vector3 scale = default(Vector3), bool useFxZNum = true)
    {
        $"生成特效{effectId}".Log();
        EffectAttr effectAttr = fxData[effectId];
        GameObject gameObject = UsePool(effectAttr.effect.name);
        Transform transform = gameObject == null ? Instantiate(effectAttr.effect) : gameObject.transform;
        if (target != null)
        {
            transform.parent = target;
            transform.localPosition = position;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            if (!effectAttr.isFollow)
            {
                transform.parent = null;
                transform.position = target.position + position;
            }
        }
        else
        {
            transform.parent = null;
            transform.position = position;
        }

        switch (effectAttr.rotation)
        {
            case FXRotationCondition.HaveEnemyDirectionY:
                transform.localRotation = Quaternion.Euler(new Vector3(rotation.x, (target.localScale.x <= 0f) ? 0 : 180, rotation.z));
                break;
            case FXRotationCondition.RandomRotationZ:
                transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0, 360)));
                break;
            case FXRotationCondition.Preference:
                transform.localRotation = Quaternion.Euler(rotation);
                break;
            case FXRotationCondition.HaveEnemyDirectionZ:
                transform.localRotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y, (target.localScale.x <= 0f) ? 0 : 180));
                break;
            case FXRotationCondition.FollowPlayer:
                transform.localRotation = Quaternion.Euler(new Vector3(rotation.x, (target.localScale.x >= 0f) ? 0 : 180, rotation.z));
                break;
            case FXRotationCondition.FollowTargeRotation:
                transform.localRotation = target.rotation;
                break;
        }

        transform.localScale = scale == Vector3.zero ? scale : effectAttr.scale;
        if (useFxZNum)
        {
            Vector3 position2 = transform.position;
            position2.z = LayerManager.ZNum.Fx;
            transform.position = position2;
        }

        transform.gameObject.SetActive(true);
        return transform;
    }

    public GameObject UsePool(string effectName)
    {
        return _objectPoolDict.ContainsKey(effectName) ? _objectPoolDict[effectName].GetObject() : null;
    }

    /// <summary>
    /// 终止的影响
    /// </summary>
    /// <param name="target"></param>
    public static void TerminateEffect(GameObject target)
    {
        AutoDisableEffect component = target.GetComponent<AutoDisableEffect>();
        if (component)
        {
            component.Disable(target.transform);
        }
        else
        {
            Destroy(target);
        }
    }

    /// <summary>
    /// 预加载
    /// </summary>
    private void Preload()
    {
        _objectPoolDict = PoolController.EffectDict;
        for (int i = 0; i < fxSerializedData.Count; i++)
        {
            try
            {
                EffectAttr effectAttr = fxSerializedData[i];
                if (effectAttr.effect != null)
                {
                    AutoDisableEffect component = effectAttr.effect.GetComponent<AutoDisableEffect>();
                    if (!_objectPoolDict.ContainsKey(effectAttr.effect.name) && component != null)
                    {
                        ObjectPool objectPool = new ObjectPool();
                        StartCoroutine(objectPool.Init(effectAttr.effect.gameObject, gameObject, effectAttr.effectStartCount,
                            effectAttr.maxCount));
                        _objectPoolDict.Add(effectAttr.effect.name, objectPool);
                    }
                }
            }
            catch (Exception)
            {
                i.Error();
            }
        }
    }
}

/// <summary>
/// 效果旋转情况
/// </summary>
public enum FXRotationCondition
{
    /// <summary>
    /// 敌人方向是Y
    /// </summary>
    HaveEnemyDirectionY = 1,

    /// <summary>
    /// 随机旋转Z
    /// </summary>
    RandomRotationZ,

    /// <summary>
    /// 优先
    /// </summary>
    Preference,

    /// <summary>
    /// 敌人方向是Z
    /// </summary>
    HaveEnemyDirectionZ,

    /// <summary>
    ///  跟随玩家
    /// </summary>
    FollowPlayer,

    /// <summary>
    /// 跟随目标旋转
    /// </summary>
    FollowTargeRotation
}