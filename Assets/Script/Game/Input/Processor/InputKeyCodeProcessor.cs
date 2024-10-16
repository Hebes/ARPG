using UnityEngine;

/// <summary>
/// 输入键盘控制器
/// </summary>
public class InputKeyCodeProcessor
{
    /// <summary>
    /// 输入是否开启
    /// </summary>
    public bool IsOpen = true;

    /// <summary>
    /// 是否按下
    /// </summary>
    private bool _isPressed;

    /// <summary>
    /// 过去是否按下
    /// </summary>
    private bool _wasPressed;

    /// <summary>
    /// 按下时间
    /// </summary>
    private float _pressedTime;

    /// <summary>
    /// 按下的帧
    /// </summary>
    private int _pressedFrame;

    /// <summary>
    /// 长按时间
    /// </summary>
    private const float LongPressTime = 0.3f;

    /// <summary>
    /// 是否点击
    /// </summary>
    public bool OnClick => OnReleased && _pressedTime < 0.3f;


    /// <summary>
    /// 是否按了指定的秒数
    /// </summary>
    /// <param name="second">秒数</param>
    /// <param name="accelerate">加速</param>
    /// <returns></returns>
    public bool OnPressedForSeveralSeconds(float second = 0.333333343f, bool accelerate = true)
    {
        int num = WorldTime.SecondToFrame(second);
        int num2 = WorldTime.SecondToFrame(0.0833333358f);
        int b = num2;
        if (accelerate)
        {
            int a = num - _pressedFrame / num2;
            num = Mathf.Max(a, b);
        }

        return OnPressed || (Pressed && _pressedFrame != 0 && _pressedFrame % num == 0);
    }

    /// <summary>
    /// 是否释放了
    /// </summary>
    public bool OnReleased => IsOpen && !_isPressed && _wasPressed;

    /// <summary>
    /// 是否按下
    /// </summary>
    public bool Pressed => IsOpen && _isPressed;

    /// <summary>
    /// 是否长时间按下
    /// </summary>
    public bool LongPressed => Pressed && _pressedTime >= 0.3f;

    /// <summary>
    /// 是否释放了
    /// </summary>
    public bool Released => IsOpen && !_isPressed;
    
    
    /// <summary>
    /// 是否按下
    /// </summary>
    public bool OnPressed => Pressed && !_wasPressed;

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="isPressed"></param>
    public void Update(bool isPressed)
    {
        if (_wasPressed && isPressed)
        {
            _pressedTime += Time.unscaledDeltaTime;
            _pressedFrame++;
        }
        else if (!_wasPressed && !isPressed)
        {
            _pressedTime = 0f;
            _pressedFrame = 0;
        }

        _wasPressed = _isPressed;
        _isPressed = isPressed;
    }
}