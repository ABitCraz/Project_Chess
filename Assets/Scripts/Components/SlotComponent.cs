using UnityEditor;
using UnityEngine;

public class SlotComponent : MonoBehaviour
{
    public Slot thisSlot = new(LandscapeType.Wilderness);
    public GameObject SlotContainer;
    public bool IsAttackFocusing = false;
    public bool IsVisionFocusing = false;
    public bool IsMovingFocusing = false;
    public bool IsRouteFocusing = false;
    public bool IsDropFocusing = false;
    public bool IsUnfocusing = true;
    readonly SpriteColorFunctions scf = new();
    SlotUpdateBehaviour.SlotUpdateActions AttackFocusingActions;
    SlotUpdateBehaviour.SlotUpdateActions VisionFocusingActions;
    SlotUpdateBehaviour.SlotUpdateActions MoveFocusingActions;
    SlotUpdateBehaviour.SlotUpdateActions RouteFocusingActions;
    SlotUpdateBehaviour.SlotUpdateActions DropFocusingActions;
    public SlotUpdateBehaviour.SlotUpdateActions UnfocusingActions;

    private void Awake()
    {
        AttackFocusingActions += () =>
        {
            int speed = 5;
            for (int i = 0; i < speed; i++)
            {
                bool is_done = scf.ColorFadingInTargetColor(this.gameObject, 32, 0);
                if (is_done)
                {
                    break;
                }
            }
        };

        RouteFocusingActions += () =>
        {
            int speed = 5;
            for (int i = 0; i < speed; i++)
            {
                bool isdone = scf.ColorFadingInTargetColor(
                    this.gameObject,
                    new Color32(128, 128, 255, 255)
                );
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
                bool isdone = scf.ColorFadingInTargetColor(this.gameObject, 32, 2);
                if (isdone)
                {
                    break;
                }
            }
        };

        MoveFocusingActions += () =>
        {
            int speed = 5;
            for (int i = 0; i < speed; i++)
            {
                bool isdone = scf.ColorFadingInTargetColor(this.gameObject, 0, 1);
                if (isdone)
                {
                    break;
                }
            }
        };

        DropFocusingActions += () =>
        {
            int speed = 5;
            for (int i = 0; i < speed; i++)
            {
                bool isdone = scf.ColorFadingInTargetColor(
                    this.gameObject,
                    new Color(32, 32, 255, 255)
                );
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
            IsMovingFocusing = false;
            IsRouteFocusing = false;
            IsDropFocusing = false;
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
            IsMovingFocusing = false;
            IsRouteFocusing = false;
            IsDropFocusing = false;
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
            if (IsMovingFocusing)
            {
                MoveFocusingActions();
            }
            if (IsRouteFocusing)
            {
                RouteFocusingActions();
            }
            if(IsDropFocusing)
            {
                DropFocusingActions();
            }
        }
    }
}
