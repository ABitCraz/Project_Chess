using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RaycastInGame : MonoBehaviour
{
    public List<PlanActions> ActionList = new();
    public List<Chess> PlannedChesses = new();
    public GameObject StatusSet;
    public GameObject ActionDropdown;
    public Player CurrentPlayer;
    delegate void ActionController();
    ActionController EndPicking;
    ActionController EndDrawing;
    Ray mouseray;
    SlotStatusShow sss = new();
    SlotCalculator sc = new();
    SavingDatum save;
    Coroutine loadedsave = null;
    GameObject previousslot;
    bool isactiondropdown = false;
    List<Slot> moveroute = new();
    Coroutine drawroutecoroutine;
    Coroutine picktargetcoroutine;
    Slot targetslot;
    bool ispickingtarget = false;
    bool isdrawingtarget = false;

    private void Awake()
    {
        loadedsave ??= StartCoroutine(LoadSave());
    }

    private void Update()
    {
        if (!isactiondropdown && !ispickingtarget && !isdrawingtarget)
        {
            mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hitsth = ShootingRaycast(ref mouseray);
        }
        else if (Input.GetMouseButton(1))
        {
            ActionDropdown.SetActive(false);
            isactiondropdown = false;
        }
    }

    private bool ShootingRaycast(ref Ray mouseray)
    {
        RaycastHit[] hits = Physics.RaycastAll(mouseray);
        for (int i = 0; i < hits.Length; i++)
        {
            GameObject hitobject = hits[i].collider.gameObject;
            if (hitobject.CompareTag("Slot"))
            {
                previousslot = hitobject;
                Slot currentslot = hitobject.GetComponent<SlotComponent>().thisSlot;
                ControlCurrentSlot(ref currentslot);
                return true;
            }
        }
        return false;
    }

    private void ControlCurrentSlot(ref Slot slot)
    {
        sss.ShowStatus(ref slot, ref StatusSet);
        if (slot.Chess != null)
        {
            sss.ShowAttackRange(slot, save.SlotMap);
            sss.ShowVisionRange(slot, save.SlotMap);
        }
        if (PlannedChesses.Contains(slot.Chess) || slot.Chess == null)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            isactiondropdown = true;
            ActionDropdown.SetActive(true);
            ActionDropdown.GetComponent<RectTransform>().position = Input.mousePosition;
            sss.ShowMovementRange(slot, save.SlotMap);
            GiveButtonActions(slot);
        }
    }

    IEnumerator DrawRoute(Slot currentslot)
    {
        CleanUpMap();
        isdrawingtarget = true;
        Slot[] movableslots = new Slot[] { };
        while (true)
        {
            CleanUpMap();
            Slot[] previousmovableslots = sss.ShowMovementRange(currentslot, save.SlotMap);
            for (int i = 0; i < moveroute.Count; i++)
            {
                moveroute[i].SlotGameObject.GetComponent<SlotComponent>().IsRouteFocusing = true;
            }
            movableslots = sss.ShowMovementRange(currentslot, save.SlotMap);
            for (int i = 0; i < movableslots.Length; i++)
            {
                if (!moveroute.Contains(movableslots[i]))
                    movableslots[i].SlotGameObject.GetComponent<SlotComponent>().IsMovingFocusing =
                        true;
            }
            if (movableslots.Length <= 0)
            {
                EndDrawing();
            }
            bool gotpicked = false;
            while (true)
            {
                mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(mouseray);
                for (int i = 0; i < hits.Length; i++)
                {
                    GameObject hitslot = hits[i].collider.gameObject;
                    if (
                        hitslot.CompareTag("Slot")
                        && movableslots.Contains(hitslot.GetComponent<SlotComponent>().thisSlot)
                        && !moveroute.Contains(hitslot.GetComponent<SlotComponent>().thisSlot)
                        && Input.GetMouseButton(0)
                    )
                    {
                        print("Shoot ya.");
                        moveroute.Add(hitslot.GetComponent<SlotComponent>().thisSlot);
                        currentslot = hitslot.GetComponent<SlotComponent>().thisSlot;
                        break;
                    }
                }
                if (gotpicked)
                {
                    break;
                }
                if (Input.GetMouseButton(1))
                {
                    EndDrawing();
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator PickTargetSlot(Slot currentslot)
    {
        CleanUpMap();
        ispickingtarget = true;
        Slot[] attackableslots = sss.ShowAttackRange(currentslot, save.SlotMap);
        for (int i = 0; i < attackableslots.Length; i++)
        {
            attackableslots[i].SlotGameObject.GetComponent<SlotComponent>().IsAttackFocusing = true;
        }
        if (attackableslots.Length <= 0)
        {
            StopCoroutine(drawroutecoroutine);
            drawroutecoroutine = null;
        }
        while (true)
        {
            mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(mouseray);
            for (int i = 0; i < hits.Length; i++)
            {
                GameObject hitslot = hits[i].collider.gameObject;
                if (
                    hitslot.CompareTag("Slot")
                    && attackableslots.Contains(hitslot.GetComponent<SlotComponent>().thisSlot)
                    && Input.GetMouseButton(0)
                )
                {
                    targetslot = hitslot.GetComponent<SlotComponent>().thisSlot;
                    EndPicking();
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void CleanUpMap()
    {
        for (int i = 0; i < save.SlotMap.FullSlotMap.Length; i++)
        {
            save.SlotMap.FullSlotMap[i].SlotGameObject
                .GetComponent<SlotComponent>()
                .UnfocusingActions();
        }
    }

    private void GiveButtonActions(Slot slot)
    {
        PlanActions planaction = new(slot.Position, ActionType.Attack, slot.Chess, CurrentPlayer);
        ActionDropdown.transform
            .GetChild(1)
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                ActionDropdown.SetActive(false);
                picktargetcoroutine = StartCoroutine(PickTargetSlot(slot));
                isactiondropdown = false;
                EndPicking += () =>
                {
                    PlannedChesses.Add(slot.Chess);
                    if (picktargetcoroutine != null)
                    {
                        StopCoroutine(picktargetcoroutine);
                    }
                    picktargetcoroutine = null;
                    if (targetslot != null)
                    {
                        ActionList.Add(planaction.Attack(targetslot));
                    }
                    ispickingtarget = false;
                    targetslot = null;
                    CleanUpMap();
                };
            });
        ActionDropdown.transform
            .GetChild(2)
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                ActionDropdown.SetActive(false);
                drawroutecoroutine = StartCoroutine(DrawRoute(slot));
                isactiondropdown = false;
                EndDrawing += () =>
                {
                    PlannedChesses.Add(slot.Chess);
                    if (drawroutecoroutine != null)
                    {
                        StopCoroutine(drawroutecoroutine);
                    }
                    drawroutecoroutine = null;
                    if (moveroute.Count > 0)
                    {
                        ActionList.Add(planaction.Move(moveroute.ToArray()));
                    }
                    isdrawingtarget = false;
                    CleanUpMap();
                    moveroute = new();
                };
            });
        ActionDropdown.transform
            .GetChild(3)
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                ActionDropdown.SetActive(false);
                drawroutecoroutine = StartCoroutine(DrawRoute(slot));
                isactiondropdown = false;
                EndDrawing += () =>
                {
                    PlannedChesses.Add(slot.Chess);
                    if (drawroutecoroutine != null)
                    {
                        StopCoroutine(drawroutecoroutine);
                    }
                    drawroutecoroutine = null;
                    if (moveroute.Count > 0)
                    {
                        ActionList.Add(planaction.Push(moveroute.ToArray()));
                    }
                    isdrawingtarget = false;
                    CleanUpMap();
                    moveroute = new();
                };
            });
        ActionDropdown.transform
            .GetChild(4)
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                ActionDropdown.SetActive(false);
                drawroutecoroutine = StartCoroutine(DrawRoute(slot));
                isactiondropdown = false;
                EndDrawing += () =>
                {
                    PlannedChesses.Add(slot.Chess);
                    if (drawroutecoroutine != null)
                    {
                        StopCoroutine(drawroutecoroutine);
                    }
                    drawroutecoroutine = null;
                    ActionList.Add(planaction.Alert(moveroute.ToArray()));
                    isdrawingtarget = false;
                    CleanUpMap();
                    moveroute = new();
                };
            });
        ActionDropdown.transform
            .GetChild(5)
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                isactiondropdown = false;
                PlannedChesses.Add(slot.Chess);
                ActionDropdown.SetActive(false);
                ActionList.Add(planaction.Repair());
            });
        ActionDropdown.transform
            .GetChild(6)
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                isactiondropdown = false;
                PlannedChesses.Add(slot.Chess);
                ActionDropdown.SetActive(false);
                ActionList.Add(planaction.Hold());
            });
    }

    IEnumerator LoadSave()
    {
        while (!this.GetComponent<LoadTargetMap>().IsLoadDone)
        {
            yield return new WaitForEndOfFrame();
        }
        save = this.GetComponent<LoadTargetMap>().save;
        StopCoroutine(loadedsave);
        loadedsave = null;
        print("Load is Done");
    }
}
