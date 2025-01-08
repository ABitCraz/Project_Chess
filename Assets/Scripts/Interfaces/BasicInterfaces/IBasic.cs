using UnityEngine;

public interface IBasic
{
    public abstract void LoadSpriteAndAnimation();
    public abstract void Attack(Slot target_slot);
    public abstract void BeenAttacked();
    public abstract void Defend(Slot target_slot);
    public abstract void BeenDefended();
    public abstract void Move(Slot target_slot);
    public abstract void BeenMoved();
}
