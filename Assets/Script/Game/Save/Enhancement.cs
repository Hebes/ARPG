using System;
using System.Collections.Generic;

/// <summary>
/// 增强
/// </summary>
[Serializable]
public class Enhancement
{
    static Dictionary<string, int> _003C_003Ef__switch_0024map19;

    public Enhancement()
    {
        SetToDefault();
    }

    public int this[string name]
    {
        get
        {
            switch (name)
            {
                case "attack": return Attack;
                case "knockout": return Knockout;
                case "avatarAttack": return AvatarAttack;
                case "airAttack": return AirAttack;
                case "airCombo2": return AirCombo2;//空中战斗2
                case "airAvatarAttack": return AirAvatarAttack;
                case "airCombo1": return AirCombo1;//空战1
                case "tripleAttack": return TripleAttack;//三倍攻击
                case "combo2": return Combo2;//组合2
                case "upperChop": return UpperChop;
                case "combo1": return Combo1;
                case "bladeStorm": return BladeStorm;
                case "shadeAttack": return ShadeAttack;
                case "charging": return Charging;
                case "chase": return Chase;
                case "hitGround": return HitGround;
                case "maxHP": return MaxHp;
                case "maxEnergy": return MaxEnergy;
                case "flashAttack": return FlashAttack;
                case "recover": return Recover;
            }

            throw new Exception($"名称{name}这个技能不存在");
        }
        set
        {
            if (name != null)
            {
                _003C_003Ef__switch_0024map19 ??= new Dictionary<string, int>(20)
                {
                    { "attack", 0 },
                    { "knockout", 1 },
                    { "avatarAttack", 2 },
                    { "airAttack", 3 },
                    { "airCombo2", 4 },
                    { "airAvatarAttack", 5 },
                    { "airCombo1", 6 },
                    { "tripleAttack", 7 },
                    { "combo2", 8 },
                    { "upperChop", 9 },
                    { "combo1", 10 },
                    { "bladeStorm", 11 },
                    { "shadeAttack", 12 },
                    { "charging", 13 },
                    { "chase", 14 },
                    { "hitGround", 15 },
                    { "maxHP", 16 },
                    { "maxEnergy", 17 },
                    { "flashAttack", 18 },
                    { "recover", 19 }
                };

                int num;
                if (_003C_003Ef__switch_0024map19.TryGetValue(name, out num))
                {
                    switch (num)
                    {
                        case 0: Attack = value; break;
                        case 1: Knockout = value; break;
                        case 2: AvatarAttack = value; break;
                        case 3: AirAttack = value; break;
                        case 4: AirCombo2 = value; break;
                        case 5: AirAvatarAttack = value; break;
                        case 6: AirCombo1 = value; break;
                        case 7: TripleAttack = value; break;
                        case 8: Combo2 = value; break;
                        case 9: UpperChop = value; break;
                        case 10: Combo1 = value; break;
                        case 11: BladeStorm = value; break;
                        case 12: ShadeAttack = value; break;
                        case 13: Charging = value; break;
                        case 14: Chase = value; break;
                        case 15: HitGround = value; break;
                        case 16: MaxHp = value; break;
                        case 17: MaxEnergy = value; break;
                        case 18: FlashAttack = value; break;
                        case 19: Recover = value; break;
                        case 20: goto IL_27A;
                        default: goto IL_27A;
                    }

                    return;
                }
            }

            IL_27A:
            throw new KeyNotFoundException();
        }
    }

    public void SetAllTo(int value)
    {
        Attack = value;
        Knockout = value;
        AvatarAttack = value;
        AirAttack = value;
        AirCombo2 = value;
        AirAvatarAttack = value;
        AirCombo1 = value;
        TripleAttack = value;
        Combo2 = value;
        UpperChop = value;
        Combo1 = value;
        BladeStorm = value;
        ShadeAttack = value;
        Charging = value;
        Chase = value;
        HitGround = value;
        MaxHp = value;
        Post("maxHP", value);
        MaxEnergy = value;
        FlashAttack = value;
        Recover = value;
        R.Player.Attribute.flashLevel = value;
    }

    public void SetToDefault()
    {
        Attack = 1;
        Knockout = 0;
        AvatarAttack = 0;
        AirAttack = 1;
        AirCombo2 = 0;
        AirCombo1 = 0;
        AirAvatarAttack = 0;
        AirCombo1 = 1;
        TripleAttack = 1;
        Combo2 = 0;
        UpperChop = 1;
        Combo1 = 1;
        BladeStorm = 0;
        ShadeAttack = 0;
        Charging = 1;
        Chase = 0;
        HitGround = 1;
        MaxHp = 1;
        MaxEnergy = 1;
        FlashAttack = 1;
        Recover = 1;
    }

    public void Post(string name, int upToLevel)
    {
        GameEvent.EnhanceLevelup.Trigger(new EnhanceArgs(name, upToLevel));
    }

    
    public int AirAttack;
    public int AirAvatarAttack;
    public int AirCombo1;
    public int AirCombo2;
    public int Attack;
    public int AvatarAttack;
    public int BladeStorm;
    public int Charging;
    public int Chase;
    public int Combo1;
    public int Combo2;
    public int FlashAttack;
    public int HitGround;//技能下劈
    public int Knockout;
    public int MaxEnergy;
    public int MaxHp;
    public int ShadeAttack;
    public int TripleAttack;
    public int UpperChop;
    public int Recover;
}