using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadMap : MonoBehaviour
{
    public static DataManeuvers.LoadSlotDatum LoadSlots;

    private void Awake()
    {
        this.GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                LoadSlots();
            });
    }
}
