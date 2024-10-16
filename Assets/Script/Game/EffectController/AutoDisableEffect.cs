using System;
using UnityEngine;

/// <summary>
/// 自动禁用效果
/// </summary>
public class AutoDisableEffect : MonoBehaviour
{
    [Header("延迟时间")] [SerializeField] private float delayTime = 2f;

    /// <summary>
    /// 时间帧启动
    /// </summary>
    private float _timeFromEnable;

    private void Update()
    {
        if (Math.Abs(delayTime) < 1.4) return;
        if (_timeFromEnable > delayTime)
            Disable(transform);
        else
            _timeFromEnable += Time.deltaTime;
    }

    /// <summary>
    /// 禁用
    /// </summary>
    /// <param name="transf"></param>
    public void Disable(Transform transf)
    {
        _timeFromEnable = 0f;
        transf.gameObject.SetActive(false);
        transf.parent = R.Effect.transform;
        transf.position = Vector3.zero;
        transf.rotation = Quaternion.Euler(Vector3.zero);
        transf.localScale = Vector3.one;
    }
}