using System;
using UnityEngine;

[ExecuteInEditMode]
// [AddComponentMenu("Camera Filter Pack/FB/AAA/Super Hexagon")]
public class CameraFilterPack_AAA_FBSuperHexagon : BaseBehaviour
{
	private Material material
	{
		get
		{
			if (this.SCMaterial == null)
			{
				this.SCMaterial = new Material(this.SCShader);
				this.SCMaterial.SetTexture("_NoiseTex", this.NoiseTexture);
				this.SCMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.SCMaterial;
		}
	}

	private void Start()
	{
		CameraFilterPack_AAA_FBSuperHexagon.Changecenter = this.center;
		CameraFilterPack_AAA_FBSuperHexagon.ChangeRadius = this.Radius;
		CameraFilterPack_AAA_FBSuperHexagon.ChangeValue = this.HexaSize;
		CameraFilterPack_AAA_FBSuperHexagon.ChangeAlphaHexa = this._AlphaHexa;
		CameraFilterPack_AAA_FBSuperHexagon.ChangeBorderSize = this._BorderSize;
		CameraFilterPack_AAA_FBSuperHexagon.ChangeBorderColor = this._BorderColor;
		CameraFilterPack_AAA_FBSuperHexagon.ChangeHexaColor = this._HexaColor;
		CameraFilterPack_AAA_FBSuperHexagon.ChangeSpotSize = this._SpotSize;
		this.SCShader = Shader.Find("CameraFilterPack/AAA_FBSuper_Hexagon");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.HexaSize);
			this.material.SetFloat("_PositionX", this.center.x);
			this.material.SetFloat("_PositionY", this.center.y);
			this.material.SetFloat("_Radius", this.Radius);
			this.material.SetFloat("_BorderSize", this._BorderSize);
			this.material.SetColor("_BorderColor", this._BorderColor);
			this.material.SetColor("_HexaColor", this._HexaColor);
			this.material.SetFloat("_AlphaHexa", this._AlphaHexa);
			this.material.SetFloat("_SpotSize", this._SpotSize);
			this.material.SetFloat("_NoiseSpeed", this._NoiseSpeed);
			this.material.SetFloat("_NoiseAlpha", this._NoiseAlpha);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}

	private void Update()
	{
	}

	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	public Shader SCShader;

	public Texture NoiseTexture;

	[Range(0f, 1f)]
	public float _AlphaHexa = 1f;

	[SerializeField]
	private float TimeX = 1f;

	private Vector4 ScreenResolution;

	private Material SCMaterial;

	[Range(0.2f, 10f)]
	public float HexaSize = 2.5f;

	public float _BorderSize = 1f;

	public Color _BorderColor = new Color(0.75f, 0.75f, 1f, 1f);

	public Color _HexaColor = new Color(0f, 0.5f, 1f, 1f);

	public float _SpotSize = 2.5f;

	[Range(1f, 10f)]
	public float _NoiseSpeed = 1f;

	[Range(0f, 1f)]
	public float _NoiseAlpha = 0.3f;

	public static float ChangeBorderSize = 1f;

	public static Color ChangeBorderColor;

	public static Color ChangeHexaColor;

	public static float ChangeSpotSize = 1f;

	public static float ChangeAlphaHexa = 1f;

	public static float ChangeValue;

	public Vector2 center = new Vector2(0.5f, 0.5f);

	public float Radius = 0.25f;

	public static Vector2 Changecenter;

	public static float ChangeRadius;
}
