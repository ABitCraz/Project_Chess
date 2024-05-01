using System;
using UnityEngine;

public class BasicUnit
{
    public GameObject UnitGameObject;
    public Sprite UnitSprite;

    protected void SwapSprite(Sprite loadsprite)
    {
        this.UnitSprite = loadsprite;
        this.UnitGameObject.GetComponent<SpriteRenderer>().sprite = loadsprite;
    }

    public void PutToSlotPosition(ref GameObject slotobject)
    {
        UnitGameObject.transform.position = slotobject.transform.position;
    }
}
