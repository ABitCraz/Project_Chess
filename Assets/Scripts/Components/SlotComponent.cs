using UnityEditor;
using UnityEngine;

public class SlotComponent : MonoBehaviour
{
    public Slot thisSlot = new(LandscapeType.Wildlessness);
    public GameObject SlotContainer;
    public bool IsAttackFocusing = false;
    public bool IsVisionFocusing = false;
    public bool IsUnfocusing = true;
    SpriteColorFunctions scf = new();
    SlotUpdateBehaviour.SlotUpdateActions AttackFocusingActions;
    SlotUpdateBehaviour.SlotUpdateActions VisionFocusingActions;
    public SlotUpdateBehaviour.SlotUpdateActions UnfocusingActions;

    private void Awake()
    {
        AttackFocusingActions += () =>
        {
            int speed = 5;
            for (int i = 0; i < speed; i++)
            {
                bool isdone = scf.ColorFadingInTargetColor(this.gameObject, 128, 0);
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
                bool isdone = scf.ColorFadingInTargetColor(this.gameObject, 128, 2);
                if (isdone)
                {
                    break;
                }
            }
        };

        UnfocusingActions += () =>
        {
            IsUnfocusing = true;
            IsAttackFocusing = false;
            IsVisionFocusing = false;
            bool isdone = scf.ColorFadingToNormal(this.gameObject, 4);
        };
    }

    private void FixedUpdate()
    {
        if (IsAttackFocusing || IsVisionFocusing)
        {
            IsUnfocusing = false;
        }
        else if (!IsAttackFocusing && !IsVisionFocusing)
        {
            IsUnfocusing = true;
        }

        if (IsUnfocusing)
        {
            IsUnfocusing = true;
            IsAttackFocusing = false;
            IsVisionFocusing = false;
            UnfocusingActions();
        }
        else
        {
            if (IsAttackFocusing)
            {
                AttackFocusingActions();
            }
            if (IsVisionFocusing)
            {
                VisionFocusingActions();
            }
        }
    }
}
