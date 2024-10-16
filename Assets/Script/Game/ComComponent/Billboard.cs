using UnityEngine;

public class Billboard : BaseBehaviour
{
    private void Awake()
    {
        if (AutoInitCamera)
        {
            Camera = Camera.main;
            Active = true;
        }
        camT = Camera.transform;
        contT = transform;
    }

    private void Update()
    {
        if (Active)
        {
            contT.LookAt(contT.position + camT.rotation * Vector3.back, camT.rotation * Vector3.up);
        }
    }

    public Camera Camera;

    public bool Active = true;

    public bool AutoInitCamera = true;

    private GameObject myContainer;

    private Transform camT;

    private Transform contT;
}