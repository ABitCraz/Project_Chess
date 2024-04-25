using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePrefabs : MonoBehaviour
{
    public static readonly Dictionary<Prefab,string> Resources = new()
    {
        {Prefab.Slot,"Prefabs/Slots/Slot"}
    };
    public static readonly Dictionary<Manager,string> Managers = new()
    {
        {Manager.MouseChecker,"Prefabs/Managers/MouseChecker"}
    };
    public static readonly Dictionary<UI,string> s = new()
    {
        {UI.SlotDropDown,"Prefabs/UIs/SlotDropDown"}
    };
}
