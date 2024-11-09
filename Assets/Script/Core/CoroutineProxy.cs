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

    /// <summary>
    /// 使用这个方法，开启的携程请携程方法结束的时候再次调用StopIEnumerator方法
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Coroutine StartIEnumerator(IEnumerator routine)
    {
        string temp = routine.GetType().FullName;
        if (string.IsNullOrEmpty(temp))
            throw new Exception("携程字符串为空");
        Coroutine coroutine = StartCoroutine(routine);
        _coroutineDic.Add(temp, coroutine);
        return coroutine;
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
    //官方
    public static Coroutine StartCoroutine(this IEnumerator routine, [UnityEngine.Internal.DefaultValue("null")] object value)
    {
        string temp = routine.GetType().FullName;
        return CoroutineProxy.I.StartCoroutine(temp, value);
    }

    public static void StopIEnumerator(this IEnumerator routine)
    {
        string temp = routine.GetType().FullName;
        CoroutineProxy.I.StopCoroutine(temp);
    }

    //自己
    public static Coroutine StartIEnumerator(this IEnumerator routine) => CoroutineProxy.I.StartIEnumerator(routine);
}