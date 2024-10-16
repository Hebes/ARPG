using UnityEngine;

/// <summary>
/// 输入设置
/// </summary>
public static class InputSetting
{
    /// <summary>
    /// 是否正在工作
    /// </summary>
    /// <returns></returns>
    public static bool IsWorking() => Input.JoystickIsOpen;

    /// <summary>
    /// 输入停止
    /// </summary>
    /// <param name="noRecord">没有记录</param>
    public static void Stop(bool noRecord = false)
    {
        "停止输入系统".Log();
        if (noRecord)
        {
            Input.JoystickIsOpen = false;
            MobileInputPlayer.I.Visible = false;
        }
        else
        {
            _pauseCount += 1u;
            if (InputSetting._pauseCount > 0u)
            {
                Input.JoystickIsOpen = false;
                if (R.Mode.CurrentMode == Mode.AllMode.UI) return;
                if (R.Mode.CurrentMode == Mode.AllMode.Story) return;
                MobileInputPlayer.I.Visible = false;
            }
        }
    }

    /// <summary>
    /// 复位
    /// </summary>
    /// <param name="noRecord">没有记录</param>
    public static void Resume(bool noRecord = false)
    {
        "恢复输入系统".Log();
        if (noRecord)
        {
            CheckReturn();
            return;
        }

        if (InputSetting._pauseCount == 0u)
        {
            "输入设置在恢复时从不暂停".Warning();
            return;
        }

        InputSetting._pauseCount -= 1u;
        if (InputSetting._pauseCount <= 0u)
            CheckReturn();

        void CheckReturn()
        {
            Input.JoystickIsOpen = true;
            if (R.Mode.CurrentMode == Mode.AllMode.UI) return;
            if (R.Mode.CurrentMode == Mode.AllMode.Story) return;
            MobileInputPlayer.I.Visible = true; //可以看见输入面板
        }
    }

    /// <summary>
    /// 判断方向
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static int JudgeDir(Vector3 from, Vector3 to) => InputSetting.JudgeDir(from.x, to.x);

    /// <summary>
    /// 判断方向
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static int JudgeDir(float from, float to) => to - from <= 0f ? -1 : 1;

    /// <summary>
    /// 是否是副的
    /// </summary>
    public static bool Assistant = true;

    /// <summary>
    /// 暂停次数
    /// </summary>
    private static uint _pauseCount;
}