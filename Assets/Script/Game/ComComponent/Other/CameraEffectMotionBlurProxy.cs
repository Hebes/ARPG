using UnityEngine;

public class CameraEffectMotionBlurProxy : ControllerProxyBase<CameraController>
{
	protected override void ThisStart()
	{
		_autoEnable = false;
	}

	protected override void OnThisEnable()
	{
		base.OnThisEnable();
		if (ReferenceEquals(effectLock, this) && effect != null)
		{
			//effect.OpenMotionBlur(frame / 60f, scale, transform.position);
		}
	}

	protected override void OnThisDisable()
	{
		if (ReferenceEquals(effectLock, this) && effect != null)
		{
			//effect.CloseMotionBlur();
		}
	}

	[SerializeField]
	private float scale = 1f;

	[SerializeField]
	private uint frame = 8u;
}
