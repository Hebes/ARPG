using UnityEngine;

/// <summary>
/// 输入操纵杆处理器
/// </summary>
public class InputJoystickProcessor
{
    public bool OnPressed => Input.JoystickIsOpen && IsOpen && _isPressed && !_wasPressed;

    public bool Pressed => Input.JoystickIsOpen && IsOpen && _isPressed;

    public bool OnReleased => Input.JoystickIsOpen && IsOpen && !_isPressed && _wasPressed;

    public bool Released => Input.JoystickIsOpen && IsOpen && !_isPressed;

    public void Update(Vector2 axis, Vector2 axisRaw = default(Vector2))
    {
        if (IsOpen)
        {
            Value = axis;
            ValueRaw = axisRaw;
        }
        else
        {
            Value = default;
            ValueRaw = default;
        }

        _wasPressed = _isPressed;
        _isPressed = (axis.sqrMagnitude >= 0.0100000007f);
    }

    private const float Threshold = 0.0100000007f;

    private bool _wasPressed;

    private bool _isPressed;

    public bool IsOpen = true;

    public Vector2 Value;

    public Vector2 ValueRaw;
}