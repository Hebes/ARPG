using System;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [HideInInspector]
    //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public event Action OnDestroy;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startTime > destroyTime)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        if (!destroyed)
        {
            Destroy(gameObject);
            OnDestroy?.Invoke();
            destroyed = true;
        }
    }

    [SerializeField]
    public float destroyTime = 2f;

    private float startTime;

    private bool destroyed;
}