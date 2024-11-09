using System;
using UnityEngine;

public class CameraEffectAAASuperHexagonProxy : ControllerProxyBase<CameraFilterPack_AAA_FBSuperHexagon>
{
	protected override void ResetToDefault()
	{
	}

	protected override void ThisUpdate()
	{
		base.effect._AlphaHexa = this.alphaHexa;
		base.effect.HexaSize = this.hexaSize;
		base.effect._BorderSize = this.borderSize;
		base.effect._BorderColor = this.borderColor;
		base.effect._HexaColor = this.hexaColor;
		base.effect._SpotSize = this.spotSize;
		base.effect._NoiseSpeed = this.noiseSpeed;
		base.effect._NoiseAlpha = this.noiseAlpha;
		base.effect.Radius = this.radius;
	}

	public float alphaHexa = 0.45f;

	public float hexaSize = 1.4f;

	public float borderSize = 1f;

	public Color borderColor = new Color(0.75f, 0.75f, 1f);

	public Color hexaColor = new Color(0f, 0.5f, 1f);

	public float spotSize = 2.5f;

	public float noiseSpeed = 1f;

	public float noiseAlpha = 0.48f;

	public float radius = 0.25f;
}
