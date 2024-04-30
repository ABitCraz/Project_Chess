using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicUnit
{
    [SerializeField]public GameObject UnitGameObject;
    [SerializeField]public Sprite UnitSprite;
    [SerializeField]public Vector2 PositionOnMap;

    protected void SwapSprite(Sprite loadsprite)
    {
        this.UnitSprite = loadsprite;
        this.UnitGameObject.GetComponent<SpriteRenderer>().sprite = loadsprite;
    }

    public void PutToSlotPosition(ref GameObject slotobject)
    {
        PositionOnMap = slotobject.GetComponent<SlotComponent>().thisSlot.Position;
        UnitGameObject.transform.position = slotobject.transform.position;
    }
}
