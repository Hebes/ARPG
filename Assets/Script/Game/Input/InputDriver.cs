using System;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// 输入驱动
/// </summary>
public class InputDriver : SMono<InputDriver>
{
    public static int Solution { get; set; }

    private void Update()
    {
        foreach (var t in _controllers)
            t.Update();
        UpdateGlobalInput();
        UpdateGameInput();
        UpdateUIInput();
        UpdateStoryInput();
        UpdateShiInput();
    }


    /// <summary>
    /// 更新全局输入
    /// </summary>
    private void UpdateGlobalInput()
    {
        Input.AnyKey.Update(PCController.AnyKey.Pressed);
    }

    /// <summary>
    /// 更新游戏输入
    /// false |= true false 被表示为 0，而 true 被表示为 1按位或运算符 | 表示两个操作数中的任意一个为1，则结果为1。 
    /// </summary>
    private void UpdateGameInput()
    {
        Vector2 vector = Vector2.zero;
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        bool flag5 = false;
        bool flag6 = false;
        bool flag7 = false;
        bool flag8 = false;
        bool flag9 = false;
        bool flag10 = false;
        bool flag11 = false;
        bool flag12 = false;
        bool flag13 = false;
        bool flag14 = false;
        bool flag15 = false;
        bool flag16 = false;
        bool flag17 = false;
        bool flag18 = false;
        bool flag19 = false;
        bool flag20 = false;
        bool flag21 = false;
        bool flag22 = false;
        bool flag23 = false;
        bool flag24 = false;
        bool flag25 = false;
        bool flag26 = false;
        bool flag27 = false;
        bool flag28 = false;
        bool flag29 = false;

        flag1 = PCController.Down.Pressed; //下
        flag2 = PCController.Left.Pressed; //左
        flag3 = PCController.Right.Pressed; //右
        flag4 = PCController.Up.Pressed; //上
        flag5 = PCController.Jump.Pressed; //跳跃
        flag6 = PCController.Attack.Pressed; //攻击
        flag8 = PCController.CirtAttack.Pressed; //重攻击
        flag16 = PCController.FlashDown.Pressed; //闪
        flag9 = PCController.Options.Pressed; //选项
        flag14 |= (flag4 && flag6);
        flag13 |= (flag1 && flag6);
        Input.Game.MoveDown.Update(flag1); //下
        Input.Game.MoveLeft.Update(flag2); //左
        Input.Game.MoveRight.Update(flag3); //右
        Input.Game.MoveUp.Update(flag4); //上
        Input.Game.Jump.Update(flag5); //跳
        Input.Game.Atk.Update(flag6); //攻击
        Input.Game.CirtAtk.Update(flag8); //重攻击
        Input.Game.UpRising.Update(flag14); //上挑
        Input.Game.HitGround.Update(flag13); //攻击地面

        Input.Game.Flash.Left.Update(flag2 && flag16);//左闪
        Input.Game.Flash.Right.Update(flag3 && flag16);//右闪
        Input.Game.Flash.FaceDirection.Update(flag16);//闪按玩家方向

        Input.Game.JumpDown.Update(flag15);
        Input.Game.BladeStorm.Update(flag6);
        Input.Game.Execute.Update(flag7);
        Input.Game.Charging.Update(flag11);
        Input.Game.Search.Update(flag12);

        Input.Game.Flash.Up.Update(flag20 && flag16);
        Input.Game.Flash.Down.Update(flag21 && flag16);
        Input.Game.Flash.RightUp.Update(flag24 && flag16);
        Input.Game.Flash.RightDown.Update(flag25 && flag16);
        Input.Game.Flash.LeftUp.Update(flag26 && flag16);
        Input.Game.Flash.LeftDown.Update(flag27 && flag16);
        Input.Game.L2.Update(flag28);
        Input.Game.R2.Update(flag29);
    }

    /// <summary>
    /// 更新UI输入
    /// </summary>
    private void UpdateUIInput()
    {
        Input.UI.Up.Update(PCController.Up.Pressed);
        Input.UI.Down.Update(PCController.Down.Pressed);
        Input.UI.Left.Update(PCController.Left.Pressed);
        Input.UI.Right.Update(PCController.Right.Pressed);
        bool pressed3 = VirtualController.Options.Pressed;
        bool flag = VirtualController.Button4.Pressed || UnityEngine.Input.GetKey(KeyCode.Escape);
        Input.UI.Cancel.Update(flag); //取消
        Input.UI.Pause.Update(pressed3);
        // Input.UI.Up.Update(DS4Controller.Up.Pressed || DS4Controller.LSUp.Pressed);
        // Input.UI.Down.Update(DS4Controller.Down.Pressed || DS4Controller.LSDown.Pressed);
        // Input.UI.Left.Update(DS4Controller.Left.Pressed || DS4Controller.LSLeft.Pressed);
        // Input.UI.Right.Update(DS4Controller.Right.Pressed || DS4Controller.LSRight.Pressed);
        // bool pressed = DS4Controller.Circle.Pressed;
        // bool pressed2 = DS4Controller.Cross.Pressed;
        // bool pressed3 = DS4Controller.Options.Pressed;
        // bool pressed4 = VirtualController.Button5.Pressed;
        // bool flag = VirtualController.Button4.Pressed || UnityEngine.Input.GetKey(KeyCode.Escape);
        // bool pressed5 = VirtualController.Options.Pressed;
        // Input.UI.Confirm.Update(pressed || pressed4);
        // Input.UI.Cancel.Update(pressed2 || flag);
        // Input.UI.Pause.Update(pressed3 || pressed5);
        // Input.UI.Debug.Update(DS4Controller.R3.Pressed || UnityEngine.Input.GetKey(KeyCode.BackQuote));
    }

    /// <summary>
    /// 更新故事输入
    /// </summary>
    private void UpdateStoryInput()
    {
        // Input.Story.Skip.Update(DS4Controller.Triangle.Pressed ||
        //                         UnityEngine.Input.GetKey(KeyCode.Escape) ||
        //                         VirtualController.Button3.Pressed);
    }


    private void UpdateShiInput()
    {
        // Vector2 to = new Vector2(Mathf.Abs(VirtualController.LeftJoystick.Value.x), Mathf.Abs(VirtualController.LeftJoystick.Value.y));
        // bool flag = Vector2.Angle(Vector2.up, to) <= 45f;
        // bool flag2 = VirtualController.LeftJoystick.Value.x > 0f;
        // bool flag3 = VirtualController.LeftJoystick.Value.x < 0f;
        // bool flag4 = VirtualController.LeftJoystick.Value.y > 0f && flag;
        // bool flag5 = VirtualController.LeftJoystick.Value.y < 0f && flag;
        // Input.Shi.Down.Update(DS4Controller.Down.Pressed || DS4Controller.LSDown.Pressed || flag5);
        // Input.Shi.Up.Update(DS4Controller.Up.Pressed || DS4Controller.LSUp.Pressed || flag4);
        // Input.Shi.Left.Update(DS4Controller.Left.Pressed || DS4Controller.LSLeft.Pressed || flag3);
        // Input.Shi.Right.Update(DS4Controller.Right.Pressed || DS4Controller.LSRight.Pressed || flag2);
        // Input.Shi.Jump.Update(DS4Controller.Square.Pressed || DS4Controller.Cross.Pressed || VirtualController.Button5.Pressed);
        // Input.Shi.Pause.Update(DS4Controller.Options.Pressed);
    }

    /// <summary>
    /// 控制器
    /// </summary>
    private IController[] _controllers =
    {
        new PCController(),
        new VirtualController()
    };

    /// <summary>
    /// 控制器是否已连接
    /// </summary>
    private bool _isControllerConnected;

    [CompilerGenerated] private static Predicate<string> _003C_003Ef__mg_0024cache0;

    [CompilerGenerated] private static Predicate<string> _003C_003Ef__mg_0024cache1;

    public static class Vibration
    {
        /// <summary>
        /// 设置振动
        /// </summary>
        /// <param name="leftMotorValue"></param>
        /// <param name="rightMotorValue"></param>
        public static void SetVibration(float leftMotorValue, float rightMotorValue)
        {
        }
    }
}