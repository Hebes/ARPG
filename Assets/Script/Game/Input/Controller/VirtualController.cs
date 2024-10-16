using UnityEngine;

/// <summary>
/// 虚拟控制器
/// </summary>
public class VirtualController : IController
{
    private MobileInputPlayer _player => MobileInputPlayer.I;

    public void Update()
    {
        Options.Update(_player.GetButton("Options") || UnityEngine.Input.GetKey(KeyCode.Escape));
        return;
        Button1.Update(_player.GetButton("Button1"));
        Button2.Update(_player.GetButton("Button2"));
        Button3.Update(_player.GetButton("Button3"));
        Button4.Update(_player.GetButton("Button4"));
        Button5.Update(_player.GetButton("Button5"));
        Button6.Update(_player.GetButton("Options"));
       
        AnyKey.Update(Button1.Pressed || Button2.Pressed || Button3.Pressed ||
                                        Button4.Pressed || Button5.Pressed || UnityEngine.Input.touchCount > 0);
        L2.Update(_player.GetButton("L2"));
        R2.Update(_player.GetButton("R2"));
        LeftJoystick.Update(_player.GetJoystick("Joystick"));
        LeftSwipe.Update(_player.GetJoystick("Swipe"));
    }

    public static readonly InputButtonProcessor AnyKey = new InputButtonProcessor();
    public static readonly InputButtonProcessor Button1 = new InputButtonProcessor();
    public static readonly InputButtonProcessor Button2 = new InputButtonProcessor();
    public static readonly InputButtonProcessor Button3 = new InputButtonProcessor();
    public static readonly InputButtonProcessor Button4 = new InputButtonProcessor();
    public static readonly InputButtonProcessor Button5 = new InputButtonProcessor();
    public static readonly InputButtonProcessor Button6 = new InputButtonProcessor();
    public static readonly InputButtonProcessor Options = new InputButtonProcessor();
    public static readonly InputButtonProcessor L2 = new InputButtonProcessor();
    public static readonly InputButtonProcessor R2 = new InputButtonProcessor();
    public static readonly InputJoystickProcessor LeftJoystick = new InputJoystickProcessor();
    public static readonly InputJoystickProcessor LeftSwipe = new InputJoystickProcessor();
}