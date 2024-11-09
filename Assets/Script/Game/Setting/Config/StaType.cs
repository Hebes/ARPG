/// <summary>
/// 通用状态(玩家和敌人公用状态，有些敌人没有一些状态)
/// </summary>
public enum EnemyStaEnum
{
    Idle, //等待1
    Idle2, //等待2 卖萌
    Idle3, //等待3
    Death, //死亡
    Run, //跑
    
    Hurt, //受伤
    
    Land, //着陆
    Atk1, //攻击1轻攻击类型1
    Atk2, //攻击2轻攻击类型2
    Atk3, //攻击3轻攻击类型3
    Atk4, //攻击4一般是重攻击
    AtkRemote, //远程攻击
    SlammedOn,//猛踩
    Jump, //跳跃
    Jumping, //跳跃中
    Fall, //落下
    Falling, //落下中
    JumpBack, //往后跳
    UnderAtk1, //受伤
    UnderAtkHitToFly, //受伤倒飞
    HitGround,//攻击地面
    HitToFly1,//在空中受伤1
    HitToFly2,//在空中受伤1
    HitToFly3,//在空中受伤3
    FallHitGround,//降落攻击地面
}

public enum PlayerStaEnum
{
    Atk1, //攻击1轻攻击类型1
    Atk2, //攻击2轻攻击类型2
    Atk3, //攻击3轻攻击类型3
    Atk4, //攻击4一般是重攻击

    AirAtk1, //空中攻击1
    AirAtk2, //空中攻击2
    AirAtk3, //空中攻击3

    AtkRemote, //远程攻击
    
    HitGround1,//下劈开始
    HitGround2, //结束
    
    UpRising,//上升攻击
    
    DoubleFlash,//双闪
    AtkFlashRollEnd,//攻击翻滚结束
    
    Cast1,//投射攻击

    Flash,//闪
    Flash2,//闪2
    FlashEnd,//闪结束
    
    Run, //跑
    RunSlow, //跑步慢下来

    Idle, //等待1
    Idle2, //等待2 卖萌
    Idle3, //等待3
    GetUp,//起立
    
    UnderAtk1, //受伤
    UnderAtkHitToFly, //受到攻击起飞
    UnderAtkFlyToFall, //受伤攻击掉落
    UnderAtkHitGround, //受伤攻击在地面
    UnderAtkGetUp, //受伤攻击起立
    
    Death, //死亡
    Jump, //跳跃
    Jumping, //跳跃中
    Fall1, //落下
    Falling, //落下中
    HitToFly3,
    JumpBack, //往后跳
}