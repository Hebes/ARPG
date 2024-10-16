using System;
using UnityEngine;

public interface IPivot
{
    Vector3 GetGameAssistantOffset();

    Vector3 GetAttackHurtEffectOffset();

    Vector2 GetAttackHurtNumberOffset();

    Vector2 GetHPBarOffset();
}