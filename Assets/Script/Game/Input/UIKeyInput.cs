using System.Collections.Generic;
using UnityEngine;

public class UIKeyInput : SMono<UIKeyInput>
{
	private static readonly Stack<GameObject> LastHoveredObjects = new Stack<GameObject>();
	private static int _pauseButtonClickCount;
	private int _debugButtonClickCount;

	private void Update()
	{
		UpdateInput();
	}

	private void UpdateInput()
	{
		if (Input.UI.Pause.OnClick || Input.Shi.Pause.OnClick)
			OnPauseClick();
		// if (Core.Input.UI.Debug.OnClick && UnityEngine.Debug.isDebugBuild)
		// 	OnDebugClick();
	}

	/// <summary>
	/// 暂停界面
	/// </summary>
	/// <returns></returns>
	public static YieldInstruction OnPauseClick()
	{
		_pauseButtonClickCount++;
		if (_pauseButtonClickCount % 2 != 0)
			return UIPause.I.PauseThenOpenWithAnim();
		return UIPause.I.CloseWithAnimThenResume();
	}

	// public void OnDebugClick()
	// {
	// 	this._debugButtonClickCount++;
	// 	if (this._debugButtonClickCount % 2 != 0)
	// 	{
	// 		SingletonMono<UIDebug>.Instance.Open();
	// 	}
	// 	else
	// 	{
	// 		SingletonMono<UIDebug>.Instance.Close();
	// 	}
	// }

	/// <summary>
	/// 暂停应用程序
	/// </summary>
	/// <param name="pause"></param>
	private void OnApplicationPause(bool pause)
	{
		//"暂停应用程序".Log();
		return;
		if (!pause && UIPause.I.Enabled && !WorldTime.IsPausing)
			OnPauseClick();
	}
}
