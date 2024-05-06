using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepSpriteErect
{
    public void KeepErect(ref SlotMap currentmap,GameObject cameraobject)
    {
        List<GameObject> wholespritegos = new();
        Slot[] cm = currentmap.FullSlotMap;
        for (int i = 0; i < cm.Length; i++)
        {
            if (cm[i].Landscape != null)
            {
                wholespritegos.Add(cm[i].Landscape.UnitGameObject);
            }
            if (cm[i].Construction != null)
            {
                wholespritegos.Add(cm[i].Construction.UnitGameObject);
            }
            if (cm[i].Chess != null)
            {
                wholespritegos.Add(cm[i].Chess.UnitGameObject);
            }
        }
        
        for(int i=0;i<wholespritegos.Count;i++)
        {
            wholespritegos[i].transform.rotation = cameraobject.transform.rotation;
        }
    }
}
