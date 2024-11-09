using System;
using UnityEngine;

public class ChipManager
{
    public static bool HasChipInScene()
    {
        EnemyChipMove[] array = UnityEngine.Object.FindObjectsOfType<EnemyChipMove>();
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].gameObject != null && array[i].gameObject.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
}