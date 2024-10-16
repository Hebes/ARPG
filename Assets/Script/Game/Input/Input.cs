using System;

/// <summary>
/// 输入
/// </summary>
public static class Input
{
    /// <summary>
    /// 操纵杆打开
    /// </summary>
    public static bool JoystickIsOpen = true;
    public static readonly InputButtonProcessor AnyKey = new InputButtonProcessor();

    public static class Game
    {
        public static readonly InputButtonProcessor MoveDown = new InputButtonProcessor();
        public static readonly InputButtonProcessor MoveUp = new InputButtonProcessor();
        public static readonly InputButtonProcessor MoveLeft = new InputButtonProcessor();
        public static readonly InputButtonProcessor MoveRight = new InputButtonProcessor();
        public static readonly InputButtonProcessor Atk = new InputButtonProcessor();//攻击
        public static readonly InputButtonProcessor CirtAtk = new InputButtonProcessor();//重攻击
        public static readonly InputButtonProcessor Jump = new InputButtonProcessor();
        public static readonly InputButtonProcessor UpRising = new InputButtonProcessor();
        public static readonly InputButtonProcessor HitGround = new InputButtonProcessor();
        public static readonly InputButtonProcessor Charging = new InputButtonProcessor();//充能
        public static readonly InputButtonProcessor Execute = new InputButtonProcessor();
        public static readonly InputButtonProcessor Defence = new InputButtonProcessor();
        public static readonly InputButtonProcessor JumpDown = new InputButtonProcessor();
        public static readonly InputButtonProcessor Chase = new InputButtonProcessor();
        public static readonly InputButtonProcessor FlashDown = new InputButtonProcessor();
        public static readonly InputButtonProcessor BladeStorm = new InputButtonProcessor();
        [Obsolete] public static readonly InputButtonProcessor ShadeAtk = new InputButtonProcessor();
        public static readonly InputButtonProcessor Search = new InputButtonProcessor();
        public static readonly InputButtonProcessor L2 = new InputButtonProcessor();
        public static readonly InputButtonProcessor R2 = new InputButtonProcessor();

        public static class Flash
        {
            public static readonly InputButtonProcessor Left = new InputButtonProcessor();
            public static readonly InputButtonProcessor Right = new InputButtonProcessor();
            public static readonly InputButtonProcessor Up = new InputButtonProcessor();
            public static readonly InputButtonProcessor Down = new InputButtonProcessor();
            public static readonly InputButtonProcessor RightUp = new InputButtonProcessor();
            public static readonly InputButtonProcessor LeftUp = new InputButtonProcessor();
            public static readonly InputButtonProcessor RightDown = new InputButtonProcessor();
            public static readonly InputButtonProcessor LeftDown = new InputButtonProcessor();
            public static readonly InputButtonProcessor FaceDirection = new InputButtonProcessor();
        }
    }

    public static class UI
    {
        public static readonly InputButtonProcessor Down = new InputButtonProcessor();
        public static readonly InputButtonProcessor Up = new InputButtonProcessor();
        public static readonly InputButtonProcessor Left = new InputButtonProcessor();
        public static readonly InputButtonProcessor Right = new InputButtonProcessor();
        public static readonly InputButtonProcessor Confirm = new InputButtonProcessor();
        public static readonly InputButtonProcessor Cancel = new InputButtonProcessor();
        public static readonly InputButtonProcessor Pause = new InputButtonProcessor();
        public static readonly InputButtonProcessor Debug = new InputButtonProcessor();
    }

    public static class Story
    {
        public static readonly InputButtonProcessor Skip = new InputButtonProcessor();//跳过
        public static readonly InputButtonProcessor BackGame = new InputButtonProcessor();//回到游戏
    }

    public static class Shi
    {
        public static readonly InputButtonProcessor Down = new InputButtonProcessor();
        public static readonly InputButtonProcessor Up = new InputButtonProcessor();
        public static readonly InputButtonProcessor Left = new InputButtonProcessor();
        public static readonly InputButtonProcessor Right = new InputButtonProcessor();
        public static readonly InputButtonProcessor Jump = new InputButtonProcessor();
        public static readonly InputButtonProcessor Pause = new InputButtonProcessor();
    }
}