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
public class CoroutineProxy : SMono<CoroutineProxy>
{
    private readonly Dictionary<string, Coroutine> _coroutineDic = new Dictionary<string, Coroutine>();

    public Coroutine StartIEnumerator(IEnumerator routine)
    {
        string temp = routine.GetType().FullName;
        if (string.IsNullOrEmpty(temp))
            throw new Exception("携程字符串为空");
        Coroutine coroutine = StartCoroutine(routine);
        _coroutineDic.Add(temp, coroutine);
        return coroutine;
    }

    private void Update()
    {
        //检查空删除
        foreach (KeyValuePair<string, Coroutine> data in _coroutineDic)
        {
            if (data.Value != null) continue;
            _coroutineDic.Remove(data.Key);
        }
    }

    public void StopIEnumerator(IEnumerator routine)
    {
        string temp = routine.GetType().FullName;
        if (string.IsNullOrEmpty(temp))
            throw new Exception("携程字符串为空");
        if (_coroutineDic.ContainsKey(temp))
        {
            Coroutine coroutine = _coroutineDic[temp];
            StopCoroutine(coroutine);
            _coroutineDic.Remove(temp);
        }
    }

    public void StopAll()
    {
        StopAllCoroutines();
    }
}

public static class CoroutineTool
{
    public static Coroutine StartIEnumerator(this IEnumerator routine) => CoroutineProxy.I.StartIEnumerator(routine);
    public static Coroutine StartCoroutine(this IEnumerator routine) => CoroutineProxy.I.StartCoroutine(routine);
    public static Coroutine StartIEnumerator(this string methodName) => CoroutineProxy.I.StartCoroutine(methodName);
    public static void StopIEnumerator(this IEnumerator routine) => CoroutineProxy.I.StopIEnumerator(routine);
    public static void StopIEnumerator(this string methodName) => CoroutineProxy.I.StopCoroutine(methodName);
}