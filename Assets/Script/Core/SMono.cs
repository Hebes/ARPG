using System;
using UnityEngine;

public class SMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object Lock = new object();
    public static bool ApplicationIsQuitting = false;

    public static T I
    {
        get
        {
            if (_instance) return _instance;
            object @lock = Lock;
            lock (@lock) _instance ??= FindObjectOfType<T>();
            if (_instance) return _instance;
            var obj = new GameObject(typeof(T).Name);
            _instance = obj.AddComponent<T>();
            DontDestroyOnLoad(obj);
            return _instance;
        }
        protected set
        {
            if (!_instance) return;
            _instance = value;
            DontDestroyOnLoad(_instance);
        }
    }

    protected virtual void OnDestroy()
    {
        ApplicationIsQuitting = true;
    }
}

/// <summary>
/// 内存中的
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : new()
{
    private static T _instance;
    public static T I => _instance ?? new T();
}

public class Singleton1<T> where T : class, new()
{
    public static readonly T I = Activator.CreateInstance<T>();
}