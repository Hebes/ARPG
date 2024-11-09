using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 能量控制器
/// </summary>
public class UIEnergyController : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        if (EnergyValue > EnergyMaxValue)
        {
            EnergyValue = EnergyMaxValue;
        }

        for (int i = 0; i < energyItems.Length; i++)
        {
            energyItems[i].enabled = (i < EnergyValue);
        }

        for (int j = 0; j < energyGroup.Length; j++)
        {
            energyGroup[j].enabled = (j < EnergyMaxValue / 2);
        }
    }

    [Range(0f, 20f)] [SerializeField] public int EnergyValue;
    [SerializeField] [Range(6f, 20f)] public int EnergyMaxValue;
    [SerializeField] private Image[] energyItems;
    [SerializeField] private Image[] energyGroup;
}