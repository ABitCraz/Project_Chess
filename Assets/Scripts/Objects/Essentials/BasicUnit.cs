using System;
using UnityEngine;

public abstract class BasicUnit : IBasic
{
    public GameObject UnitGameObject;
    public Sprite UnitSprite;

    public abstract void LoadSpriteAndAnimation();

    public void LoadSprite(Sprite load_sprite)
    {
        this.UnitSprite = load_sprite;
        this.UnitGameObject.GetComponent<SpriteRenderer>().sprite = load_sprite;
    }

    public virtual void Attack(Slot target_slot)
    {
        target_slot.Chess?.BeenAttacked();
        target_slot.Landscape?.BeenAttacked();
        target_slot.Construction?.BeenAttacked();
    }

    public virtual void BeenAttacked() { }

    public virtual void Defend(Slot target_slot)
    {
        target_slot.Chess?.BeenDefended();
        target_slot.Landscape?.BeenDefended();
        target_slot.Construction?.BeenDefended();
    }

    public virtual void BeenDefended() { }

    public virtual void Move(Slot target_slot)
    {
        target_slot.Chess?.BeenMoved();
        target_slot.Landscape?.BeenMoved();
        target_slot.Construction?.BeenMoved();
    }

    public virtual void BeenMoved() { }

    public void PutToSlotPosition(ref GameObject slot_object)
    {
        UnitGameObject.transform.position = slot_object.transform.position;
    }
}
