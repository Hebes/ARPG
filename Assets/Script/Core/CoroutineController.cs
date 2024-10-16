using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// 携程脚本
/// https://blog.csdn.net/beihuanlihe130/article/details/76098844
/// https://blog.csdn.net/qq_40544338/article/details/115414085
/// https://blog.csdn.net/Czhenya/article/details/115936459 停止协程
/// </summary>
public class CoroutineController : SMono<CoroutineController>
{
    private readonly Dictionary<string, Coroutine> _coroutineDic = new Dictionary<string, Coroutine>();

    public Coroutine StartIEnumerator(IEnumerator routine)
    {
        Coroutine coroutine = StartCoroutine(routine);
        _coroutineDic.Add(nameof(routine), coroutine);
        return coroutine;
    }

    public void StopIEnumerator(IEnumerator routine)
    {
        //检查空删除
        foreach (KeyValuePair<string, Coroutine> data in _coroutineDic)
        {
            if (data.Value != null) continue;
            _coroutineDic.Remove(data.Key);
        }

        //删除空的
        Coroutine coroutine = _coroutineDic[nameof(routine)];
        StopCoroutine(coroutine);
        _coroutineDic.Remove(nameof(routine));
    }

    public void StopAll()
    {
        StopAllCoroutines();
    }
}

public static class CoroutineTool
{
    public static Coroutine StartIEnumerator(this IEnumerator routine) => CoroutineController.I.StartIEnumerator(routine);
    public static Coroutine StartIEnumerator(this string methodName) => CoroutineController.I.StartCoroutine(methodName);
    public static void StopIEnumerator(this IEnumerator routine) => CoroutineController.I.StopCoroutine(routine);
    public static void StopIEnumerator(this string methodName) => CoroutineController.I.StopCoroutine(methodName);
}