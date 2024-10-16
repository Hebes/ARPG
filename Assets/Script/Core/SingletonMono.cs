using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object Lock = new object();
    private static bool ApplicationIsQuitting;

    public static T I
    {
        get
        {
            if (ApplicationIsQuitting)
            {
                Debug.LogWarning("[单例] 实例化 '" + typeof(T) + "申请退出时已销毁。不会再次创建-返回null");
                return null;
            }

            object @lock = Lock;
            lock (@lock) _instance ??= FindObjectOfType<T>();
            return _instance;
        }
    }

    protected virtual void OnDestroy()
    {
        ApplicationIsQuitting = true;
    }

    public static bool IsValid()
    {
        return _instance != null;
    }
}