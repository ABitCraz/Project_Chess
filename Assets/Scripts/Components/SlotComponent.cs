using UnityEditor;
using UnityEngine;

public class SlotComponent : MonoBehaviour
{
    public Slot thisSlot = new();
    public GameObject SlotContainer;
    public bool IsAttackFocusing = false;
    public bool IsVisionFocusing = false;
    public bool IsUnfocusing = false;
    SpriteColorFunctions scf = new();
    SlotUpdateBehaviour.SlotUpdateActions AttackFocusingActions;
    SlotUpdateBehaviour.SlotUpdateActions VisionFocusingActions;
    SlotUpdateBehaviour.SlotUpdateActions UnfocusingActions;

    private void Awake()
    {
        AttackFocusingActions += () =>
        {
            int speed = 5;
            for (int i = 0; i < speed; i++)
            {
                bool isdone = scf.ColorFadingInTargetColor(this.gameObject, 64, 0);
                if (isdone)
                {
                    break;
                }
            }
        };

        VisionFocusingActions += () =>
        {
            int speed = 5;
            for (int i = 0; i < speed; i++)
            {
                bool isdone = scf.ColorFadingInTargetColor(this.gameObject, 64, 2);
                if (isdone)
                {
                    break;
                }
            }
        };

        UnfocusingActions += () =>
        {
            IsAttackFocusing = false;
            IsVisionFocusing = false;
            bool isdone = scf.ColorFadingToNormal(this.gameObject, 4);
            if (isdone)
            {
                IsUnfocusing = false;
            }
        };
    }

    private void FixedUpdate()
    {
        if (IsAttackFocusing)
        {
            AttackFocusingActions();
        }
        if (IsVisionFocusing)
        {
            VisionFocusingActions();
        }
        if (IsUnfocusing)
        {
            UnfocusingActions();
        }
    }
}
