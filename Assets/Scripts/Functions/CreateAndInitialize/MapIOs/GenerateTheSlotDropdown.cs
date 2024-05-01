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
        TMP_Dropdown landscapedropdown = this.transform.GetChild(0).GetComponent<TMP_Dropdown>();
        List<OptionData> landscapes = new();
        for (int i = 0; i < Enum.GetNames(typeof(LandscapeType)).Length; i++)
        {
            landscapes.Add(
                new OptionData(
                    i switch
                    {
                        1 => "荒地",
                        2 => "高地",
                        3 => "遗迹",
                        4 => "废墟",
                        5 => "沙漠",
                        6 => "峡谷",
                        _ => "空"
                    }
                )
            );
        }
        landscapedropdown.AddOptions(landscapes);

        TMP_Dropdown constructiondropdown = this.transform.GetChild(1).GetComponent<TMP_Dropdown>();
        List<OptionData> constructions = new();
        for (int i = 0; i < Enum.GetNames(typeof(ConstructionType)).Length; i++)
        {
            constructions.Add(
                new OptionData(
                    i switch
                    {
                        1 => "城市",
                        _ => "空"
                    }
                )
            );
        }
        constructiondropdown.AddOptions(constructions);
        
        TMP_Dropdown chessdropdown = this.transform.GetChild(2).GetComponent<TMP_Dropdown>();
        List<OptionData> chesses = new();
        for (int i = 0; i < Enum.GetNames(typeof(ChessType)).Length; i++)
        {
            chesses.Add(
                new OptionData(
                    i switch
                    {
                        1 => "尖兵",
                        2 => "反装甲步兵",
                        3 => "战车",
                        4 => "坦克",
                        5 => "迫击炮",
                        6 => "火炮",
                        7 => "将军",
                        _ => "空"
                    }
                )
            );
        }
        chessdropdown.AddOptions(chesses);
    }
}
