using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI死亡界面
/// </summary>
public class UIDeath : SMono<UIDeath>
{
	private CanvasGroup Panel;
	
    private int DeathCount
	{
		get => RoundStorage.Get("DeathCount", 0);
		set => RoundStorage.Set("DeathCount", value);
	}

	private bool HasEnteredLevelSelectSystem
	{
		get => SaveStorage.Get("HasEnteredLevelSelectSystem", false);
		set => SaveStorage.Set("HasEnteredLevelSelectSystem", value);
	}

	private void Awake()
	{
		Panel = transform.Find("Panel").GetComponent<UnityEngine.CanvasGroup>();
		Panel.FadeTo(0);
		GameEvent.OnPlayerDead.Register(OnPlayerDead);
	}

	private void OnPlayerDead(object sender)
	{
		EventArgs e = (EventArgs)sender;
		if (R.SceneData.BloodPalaceMode)return;
		//if (UILanguage.IsChinese)
		if (true)
		{
			List<List<int>> list = new List<List<int>>
			{
				new List<int>
				{
					1,
					5,
					8,
					10
				},
				new List<int>
				{
					2,
					6,
					9,
					10
				},
				new List<int>
				{
					3,
					7,
					8,
					9,
					10
				},
				new List<int>
				{
					4,
					5,
					6,
					7,
					8,
					9,
					10
				}
			};
			string text = "又㕛叒叕";
			if (DeathCount <= 10)
			{
				StringBuilder stringBuilder = new StringBuilder("你");
				for (int i = 0; i < 4; i++)
				{
					if (list[i].Contains(DeathCount))
					{
						stringBuilder.Append(text[i]);
					}
				}
				stringBuilder.Append("死了");
				_youAreDie.text = stringBuilder.ToString();
			}
			else
			{
				_youAreDie.text = "我已经记不清了";
			}
		}
		else if (DeathCount == 0)
		{
			_youAreDie.text = "YOU DIED";
		}
		else if (DeathCount <= 10)
		{
			StringBuilder stringBuilder2 = new StringBuilder("YOU DIE AGAIN");
			for (int j = 0; j < DeathCount - 1; j++)
			{
				stringBuilder2.Append('N');
			}
			_youAreDie.text = stringBuilder2.ToString();
		}
		else
		{
			_youAreDie.text = "I CAN NEVER REMEMBER";
		}
		OpenWithAnim();
	}

	private void Open()
	{
		_backBtn.transform.parent.gameObject.SetActive(HasEnteredLevelSelectSystem);
		R.Mode.EnterMode(Mode.AllMode.UI);
		UIPause.I.Enabled = false;
		//UIKeyInput.SaveAndSetHoveredObject(_resumeBtn);
		playerDeadWindow.gameObject.SetActive(true);
		//CameraFilterUtils.Create<CameraFilterPack_TV_80>(R.Ui.CameraGO);
		// AnalogTV analogTV = R.Ui.CameraGO.AddComponent<AnalogTV>();
		// analogTV.Shader = Shader.Find("Hidden/Colorful/Analog TV");
		// analogTV.NoiseIntensity = 1f;
		// analogTV.ScanlinesIntensity = 0f;
		// analogTV.ScanlinesCount = 696;
		// analogTV.Distortion = 0.18f;
		// analogTV.CubicDistortion = 0f;
		// analogTV.Scale = 1.02f;
	}

	private YieldInstruction OpenWithAnim()
	{
		return StartCoroutine(OpenWithAnimCoroutine());
	}

	private IEnumerator OpenWithAnimCoroutine()
	{
		R.Audio.StopBGM();
		yield return LevelManager.LoadLevelByGateId("empty");
		Open();
		// AnalogTV analogTV = R.Ui.CameraGO.GetComponent<AnalogTV>();
		// analogTV.Scale = 0f;
		// yield return DOTween.To(() => analogTV.Scale, delegate(float scale)
		// {
		// 	analogTV.Scale = scale;
		// }, 1.02f, 0.5f).SetUpdate(true).WaitForCompletion();
	}

	private void Close()
	{
		// CameraFilterUtils.Remove<CameraFilterPack_TV_80>(R.Ui.CameraGO);
		// Destroy(R.Ui.CameraGO.GetComponent<AnalogTV>());
		playerDeadWindow.gameObject.SetActive(false);
		// UIKeyInput.LoadHoveredObject();
		UIPause.I.Enabled = true;
		R.Mode.ExitMode(Mode.AllMode.UI);
	}

	private YieldInstruction CloseWithAnim()
	{
		Close();;
		// AnalogTV analogTV = R.Ui.CameraGO.GetComponent<AnalogTV>();
		// return DOTween.To(() => analogTV.Scale, delegate(float scale)
		// {
		// 	analogTV.Scale = scale;
		// }, 0f, 0.5f).SetUpdate(true).OnComplete(delegate
		// {
		// 	Close();
		// }).WaitForCompletion();
		return new YieldInstruction();
	}

	public void Go2StartScene()
	{
		if (!Input.JoystickIsOpen)return;
		StartCoroutine(OnRoundOverCoroutine());
	}

	private IEnumerator OnRoundOverCoroutine()
	{
		InputSetting.Stop();
		UIBlackPanel.I.FadeBlack();
		//R.Ui.BlackScene.FadeBlack(0.3f, false);
		yield return CloseWithAnim();
		UIBlackPanel.I.FadeTransparent();
		//R.Ui.BlackScene.FadeTransparent(0.3f, false);
		yield return LevelManager.OnRoundOver();
		InputSetting.Resume();
		MobileInputPlayer.I.Visible = false;
	}

	public void Resurrect()
	{
		if (!Input.JoystickIsOpen)return;
		StartCoroutine(OnPlayerDieCoroutine());
	}

	private IEnumerator OnPlayerDieCoroutine()
	{
		InputSetting.Stop();
		UIBlackPanel.I.FadeBlack();
		//R.Ui.BlackScene.FadeBlack(0.3f, false);
		yield return CloseWithAnim();
		yield return LevelManager.OnPlayerDie();
		UIBlackPanel.I.FadeTransparent();
		//R.Ui.BlackScene.FadeTransparent(0.3f, false);
		InputSetting.Resume();
	}

	[SerializeField] private GameObject playerDeadWindow;
	[SerializeField] private GameObject _resumeBtn;
	[SerializeField] private GameObject _backBtn;
	[SerializeField] private Text _youAreDie;
}