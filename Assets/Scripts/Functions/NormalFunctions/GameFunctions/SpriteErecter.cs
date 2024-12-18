using UnityEngine;

public class SpriteErecter : MonoBehaviour
{
    public SavingDatum save;

    public void Update()
    {
        if (save.SlotMap != null)
        {
            KeepSpriteErect.KeepErect(ref save.SlotMap, Camera.main.gameObject);
        }
    }
}
