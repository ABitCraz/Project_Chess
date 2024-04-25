using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static TMPro.TMP_Dropdown;

public class GenerateTheSlotDropdown : MonoBehaviour
{

    private void Awake()
    {
        ReconstructDropDown();
    }

    private void ReconstructDropDown()
    {
        TMP_Dropdown landscapedropdown = this.transform
            .GetChild(0)
            .GetComponent<TMP_Dropdown>();
        for (int i = 0; i < Enum.GetNames(typeof(LandscapeType)).Length; i++)
        {
            landscapedropdown.options.Add(
                new OptionData(
                    i switch
                    {
                        0 => "荒地",
                        1 => "高地",
                        2 => "遗迹",
                        3 => "废墟",
                        4 => "沙漠",
                        5 => "峡谷",
                        _ => null
                    }
                )
            );
        }
        TMP_Dropdown constructiondropdown = this.transform
            .GetChild(1)
            .GetComponent<TMP_Dropdown>();
        for (int i = 0; i < Enum.GetNames(typeof(ConstructionType)).Length; i++)
        {
            landscapedropdown.options.Add(
                new OptionData(
                    i switch
                    {
                        0 => "城市",
                        _ => null
                    }
                )
            );
        }
        TMP_Dropdown chessdropdown = this.transform
            .GetChild(2)
            .GetComponent<TMP_Dropdown>();
        for (int i = 0; i < Enum.GetNames(typeof(ChessType)).Length; i++)
        {
            landscapedropdown.options.Add(
                new OptionData(
                    i switch
                    {
                        0 => "尖兵",
                        1 => "反装甲步兵",
                        2 => "轻型战车",
                        3 => "坦克",
                        4 => "迫击炮",
                        5 => "火炮",
                        6 => "将军",
                        _ => null
                    }
                )
            );
        }
    }
}