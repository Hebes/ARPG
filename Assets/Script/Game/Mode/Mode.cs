using System;
using System.Collections.Generic;

/// <summary>
/// 游戏模式模式，控制游戏按键输入
/// </summary>
public class Mode
{
    public Mode() => SetInputMode(AllMode.Normal);
    
    /// <summary>
    /// 模式栈
    /// </summary>
    private readonly Stack<AllMode> _modeStack = new Stack<AllMode>();

    /// <summary>
    /// 当前模式
    /// </summary>
    private AllMode _currentMode;
    
    /// <summary>
    /// 获取当前模式
    /// </summary>
    public AllMode CurrentMode => _currentMode;

    /// <summary>
    /// 检查当前的模式是否和输入进来的模式相匹配
    /// </summary>
    /// <returns></returns>
    public bool CheckMode(AllMode modeValue) => _currentMode == modeValue;


    /// <summary>
    /// 重置模式
    /// </summary>
    public void Reset()
    {
        _modeStack.Clear();
        _currentMode = AllMode.Normal;
        SetInputMode(AllMode.Normal);
        InputSetting.Assistant = true;
        InputSetting.Resume(true);
    }

    /// <summary>
    /// 进入模式
    /// </summary>
    /// <param name="nextMode"></param>
    public void EnterMode(AllMode nextMode)
    {
        LogBefore(nextMode, true);
        _modeStack.Push(_currentMode);
        SetInputMode(nextMode);//设置输入模式
        _currentMode = nextMode;
        LogAfter(nextMode, true);
        if (nextMode == AllMode.UI || nextMode == AllMode.Story)
        {
            MobileInputPlayer.I.Visible = false;
        }
    }

    /// <summary>
    /// 退出模式
    /// </summary>
    /// <param name="mode"></param>
    public void ExitMode(AllMode mode)
    {
        LogBefore(mode, false);
        if (_currentMode != mode)
        {
            string.Concat("当前模式与退出意图不符，当前模式为 ", _currentMode, " 退出模式为", mode).Error();
            return;
        }

        if (_modeStack.Count == 0)
        {
            "没有状态可以退出".Error();
            return;
        }

        AllMode allMode = _modeStack.Pop();
        SetInputMode(allMode);
        _currentMode = allMode;
        LogAfter(mode, false);
        if (mode == AllMode.UI || mode == AllMode.Story)
        {
            MobileInputPlayer.I.Visible = true;
        }
    }

    private void LogBefore(AllMode mode, bool isEnter)
    {
        $"当前模式{_currentMode},{(isEnter ? "进入" : "退出")}模式:{mode}".Log();
    }

    private void LogAfter(AllMode mode, bool isEnter)
    {
        $"{(isEnter ? "进入" : "退出")}模式成功，当前模式{_currentMode}".Log();
    }

    /// <summary>
    /// 设置输入模式
    /// </summary>
    /// <param name="mode"></param>
    private static void SetInputMode(AllMode mode)
    {
        switch (mode)
        {
            case AllMode.Normal:
                SetAllGameInputState(true);
                Input.Game.Search.IsOpen = true;
                SetAllModeInputState(AllMode.UI,false);
                Input.UI.Pause.IsOpen = true;
                Input.UI.Debug.IsOpen = true;
                SetAllModeInputState(AllMode.Story,false);
                SetAllModeInputState(AllMode.Shi,false);
                break;
            case AllMode.Battle:
                SetAllGameInputState(true);
                Input.Game.Search.IsOpen = false;
                SetAllModeInputState(AllMode.UI,false);
                Input.UI.Pause.IsOpen = true;
                Input.UI.Debug.IsOpen = true;
                SetAllModeInputState(AllMode.Story,false);
                SetAllModeInputState(AllMode.Shi,false);
                break;
            case AllMode.Story:
                SetAllGameInputState(false);
                SetAllModeInputState(AllMode.UI,false);
                SetAllModeInputState(AllMode.Story,true);
                SetAllModeInputState(AllMode.Shi,false);
                break;
            case AllMode.UI:
                SetAllGameInputState(false);
                SetAllModeInputState(AllMode.UI,true);
                SetAllModeInputState(AllMode.Story,false);
                SetAllModeInputState(AllMode.Shi,false);
                break;
            case AllMode.Shi:
                SetAllGameInputState(false);
                SetAllModeInputState(AllMode.UI,false);
                Input.UI.Debug.IsOpen = true;
                SetAllModeInputState(AllMode.Story,false);
                SetAllModeInputState(AllMode.Shi,true);
                break;
        }
    }

    /// <summary>
    /// 设置所有游戏的输入状态
    /// </summary>
    /// <param name="open"></param>
    private static void SetAllGameInputState(bool open)
    {
        Input.Game.MoveDown.IsOpen = open;
        Input.Game.MoveUp.IsOpen = open;
        Input.Game.MoveLeft.IsOpen = open;
        Input.Game.MoveRight.IsOpen = open;
        Input.Game.Atk.IsOpen = open;
        Input.Game.CirtAtk.IsOpen = open;
        Input.Game.Jump.IsOpen = open;
        Input.Game.Flash.Left.IsOpen = open;
        Input.Game.Flash.Right.IsOpen = open;
        Input.Game.Flash.Up.IsOpen = open;
        Input.Game.Flash.Down.IsOpen = open;
        Input.Game.Flash.RightUp.IsOpen = open;
        Input.Game.Flash.LeftUp.IsOpen = open;
        Input.Game.Flash.RightDown.IsOpen = open;
        Input.Game.Flash.LeftDown.IsOpen = open;
        Input.Game.Flash.FaceDirection.IsOpen = open;
        Input.Game.UpRising.IsOpen = open;
        Input.Game.HitGround.IsOpen = open;
        Input.Game.Charging.IsOpen = open;
        Input.Game.Execute.IsOpen = open;
        Input.Game.Defence.IsOpen = open;
        Input.Game.JumpDown.IsOpen = open;
        Input.Game.Chase.IsOpen = open;
        Input.Game.FlashDown.IsOpen = open;
        Input.Game.BladeStorm.IsOpen = open;
        Input.Game.ShadeAtk.IsOpen = open;
        Input.Game.Search.IsOpen = open;
        Input.Game.L2.IsOpen = open;
        Input.Game.R2.IsOpen = open;
    }

    /// <summary>
    /// 设置输入状态
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="open"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static void SetAllModeInputState(AllMode mode, bool open)
    {
        switch (mode)
        {
            case AllMode.Story://设置所有故事输入状态
                Input.Story.Skip.IsOpen = open;
                Input.Story.BackGame.IsOpen = open;
                break;
            case AllMode.UI://设置所有UI输入状态
                Input.UI.Down.IsOpen = open;
                Input.UI.Up.IsOpen = open;
                Input.UI.Left.IsOpen = open;
                Input.UI.Right.IsOpen = open;
                Input.UI.Confirm.IsOpen = open;
                Input.UI.Cancel.IsOpen = open;
                Input.UI.Pause.IsOpen = open;
                Input.UI.Debug.IsOpen = open;
                break;
            case AllMode.Shi:
                Input.Shi.Down.IsOpen = open;
                Input.Shi.Up.IsOpen = open;
                Input.Shi.Left.IsOpen = open;
                Input.Shi.Right.IsOpen = open;
                Input.Shi.Jump.IsOpen = open;
                Input.Shi.Pause.IsOpen = open;
                break;
            default:
                throw new Exception($"未添加这个模式{nameof(mode)}");
        }
    }

    /// <summary>
    /// 所有模式
    /// </summary>
    public enum AllMode
    {
        Normal, //无
        Battle, //战斗
        Story, //故事
        UI, //UI
        Shi
    }
}