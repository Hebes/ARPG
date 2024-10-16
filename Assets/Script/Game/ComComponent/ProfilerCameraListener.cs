using System;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ProfilerCameraListener : MonoBehaviour
{
    protected Stopwatch Stopwatch
    {
        get
        {
            if (this._stopwatch == null)
            {
                this._stopwatch = new Stopwatch();
            }

            return this._stopwatch;
        }
    }

    public Camera Camera
    {
        get
        {
            if (this._camera == null)
            {
                this._camera = base.GetComponent<Camera>();
            }

            return this._camera;
        }
    }

    private void OnPreCull()
    {
        if (!base.isActiveAndEnabled)
        {
            return;
        }

        this.Stopwatch.Start();
    }

    private void OnPostRender()
    {
        if (!base.isActiveAndEnabled)
        {
            return;
        }

        double totalSeconds = this._stopwatch.Elapsed.TotalSeconds;
        this.Stopwatch.Stop();
        this.Stopwatch.Reset();
        if (this.RenderDurationCallback == null)
        {
            UnityEngine.Object.Destroy(this);
            return;
        }

        this.RenderDurationCallback(this, totalSeconds);
    }

    private Camera _camera;

    private Stopwatch _stopwatch;

    public Action<ProfilerCameraListener, double> RenderDurationCallback;
}