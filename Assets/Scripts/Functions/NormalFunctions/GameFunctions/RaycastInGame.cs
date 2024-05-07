using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class RaycastInGame : Singleton<RaycastInGame>
{
    public List<PlanActions> ActionList = new();
    public GameObject StatusSet;
    public GameObject ActionDropdown;
    public GameObject PlanContainer;
    public Player CurrentPlayer;
    public bool IsInControl = true;
    public SavingDatum save;
    delegate void ActionController();
    ActionController EndPicking;
    ActionController EndDrawing;
    Ray mouseray;
    SlotStatusShow sss = new();
    Coroutine loadedsave = null;
    bool isactiondropdown = false;
    List<Slot> moveroute = new();
    Coroutine drawroutecoroutine;
    Coroutine picktargetcoroutine;
    List<GameObject> ActionUnits = new();
    GameObject clear;
    Slot targetslot;
    bool ispickingtarget = false;
    bool isdrawingtarget = false;

    protected override void Awake()
    {
        base.Awake();
        loadedsave ??= StartCoroutine(LoadSave());
        InitializedButtons();
    }

    private void Start()
    {
        CurrentPlayer.PlayerNo = PhotonNetwork.LocalPlayer.ActorNumber;
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
        OnShowThoseVisiable();
    }

    private void OnShowThoseVisiable()
    {
        int count = ActionList.Count;
        Vector2 plancontainerrt = PlanContainer.GetComponent<RectTransform>().anchoredPosition;
        switch (count)
        {
            case <= 0:
                if (plancontainerrt.x < 230)
                {
                    PlanContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                        plancontainerrt.x + 1,
                        plancontainerrt.y
                    );
                }
                else if (plancontainerrt.x > 230)
                {
                    PlanContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                        plancontainerrt.x - 1,
                        plancontainerrt.y
                    );
                }
                break;
            case 1:
                if (plancontainerrt.x < 75)
                {
                    PlanContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                        plancontainerrt.x + 1,
                        plancontainerrt.y
                    );
                }
                else if (plancontainerrt.x > 75)
                {
                    PlanContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                        plancontainerrt.x - 1,
                        plancontainerrt.y
                    );
                }
                break;
            case 2:
                if (plancontainerrt.x < -75)
                {
                    PlanContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                        plancontainerrt.x + 1,
                        plancontainerrt.y
                    );
                }
                else if (plancontainerrt.x > -75)
                {
                    PlanContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                        plancontainerrt.x - 1,
                        plancontainerrt.y
                    );
                }
                break;
            case >= 3:
                if (plancontainerrt.x < -245)
                {
                    PlanContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                        plancontainerrt.x + 1,
                        plancontainerrt.y
                    );
                }
                else if (plancontainerrt.x > -245)
                {
                    PlanContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                        plancontainerrt.x - 1,
                        plancontainerrt.y
                    );
                }
                break;
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
        if (!IsInControl)
        {
            return;
        }
        if (slot.Chess == null || ActionList.Count >= 3)
        {
            return;
        }
        for (int i = 0; i < ActionList.Count; i++)
        {
            if (
                save.SlotMap.FullSlotDictionary[ActionList[i].CurrentSlotPosition].Chess
                == slot.Chess
            )
            {
                return;
            }
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
                    )
                    {
                        if (Input.GetMouseButton(0))
                        {
                            moveroute.Add(hitslot.GetComponent<SlotComponent>().thisSlot);
                            currentslot = hitslot.GetComponent<SlotComponent>().thisSlot;
                            gotpicked = true;
                            break;
                        }
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
                )
                {
                    if (Input.GetMouseButton(0))
                    {
                        targetslot = hitslot.GetComponent<SlotComponent>().thisSlot;
                        EndPicking();
                        break;
                    }
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
        CleanUpButtonFunctions();
        PlanActions planaction = new(slot.Position, ActionType.Attack, slot, CurrentPlayer);
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
                    if (picktargetcoroutine != null)
                    {
                        StopCoroutine(picktargetcoroutine);
                    }
                    picktargetcoroutine = null;
                    if (targetslot != null)
                    {
                        ActionList.Add(planaction.Attack(targetslot));
                        PickAnEmptyActionUnitAndFillIt(slot.Chess.ChessType, ActionType.Attack);
                    }
                    ispickingtarget = false;
                    targetslot = null;
                    CleanUpMap();
                    return;
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
                    if (drawroutecoroutine != null)
                    {
                        StopCoroutine(drawroutecoroutine);
                    }
                    drawroutecoroutine = null;
                    if (moveroute.Count > 0)
                    {
                        List<Vector2Int> paths = new();
                        for (int i = 0; i < moveroute.Count; i++)
                        {
                            paths.Add(moveroute[i].Position);
                        }
                        ActionList.Add(planaction.Move(paths.ToArray()));
                        PickAnEmptyActionUnitAndFillIt(slot.Chess.ChessType, ActionType.Move);
                    }
                    Debug.Log(ActionList.Count);
                    isdrawingtarget = false;
                    CleanUpMap();
                    moveroute = new();
                    return;
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
                    if (drawroutecoroutine != null)
                    {
                        StopCoroutine(drawroutecoroutine);
                    }
                    drawroutecoroutine = null;
                    if (moveroute.Count > 0)
                    {
                        List<Vector2Int> paths = new();
                        for (int i = 0; i < moveroute.Count; i++)
                        {
                            paths.Add(moveroute[i].Position);
                        }
                        ActionList.Add(planaction.Push(paths.ToArray()));
                        PickAnEmptyActionUnitAndFillIt(slot.Chess.ChessType, ActionType.Push);
                    }
                    isdrawingtarget = false;
                    CleanUpMap();
                    moveroute = new();
                    return;
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
                    if (drawroutecoroutine != null)
                    {
                        StopCoroutine(drawroutecoroutine);
                    }
                    drawroutecoroutine = null;
                    List<Vector2Int> paths = new();
                    for (int i = 0; i < moveroute.Count; i++)
                    {
                        paths.Add(moveroute[i].Position);
                    }
                    ActionList.Add(planaction.Alert(paths.ToArray()));
                    PickAnEmptyActionUnitAndFillIt(slot.Chess.ChessType, ActionType.Alert);
                    isdrawingtarget = false;
                    CleanUpMap();
                    moveroute = new();
                    return;
                };
            });
        ActionDropdown.transform
            .GetChild(5)
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                isactiondropdown = false;
                ActionList.Add(planaction.Repair());
                PickAnEmptyActionUnitAndFillIt(slot.Chess.ChessType, ActionType.Repair);
                ActionDropdown.SetActive(false);
                return;
            });
        ActionDropdown.transform
            .GetChild(6)
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                isactiondropdown = false;
                ActionList.Add(planaction.Hold());
                PickAnEmptyActionUnitAndFillIt(slot.Chess.ChessType, ActionType.Hold);
                ActionDropdown.SetActive(false);
                return;
            });
    }

    private void CleanUpButtonFunctions()
    {
        ActionDropdown.transform
            .GetChild(1)
            .gameObject.GetComponent<Button>()
            .onClick.RemoveAllListeners();
        ActionDropdown.transform
            .GetChild(2)
            .gameObject.GetComponent<Button>()
            .onClick.RemoveAllListeners();
        ActionDropdown.transform
            .GetChild(3)
            .gameObject.GetComponent<Button>()
            .onClick.RemoveAllListeners();
        ActionDropdown.transform
            .GetChild(4)
            .gameObject.GetComponent<Button>()
            .onClick.RemoveAllListeners();
        ActionDropdown.transform
            .GetChild(5)
            .gameObject.GetComponent<Button>()
            .onClick.RemoveAllListeners();
        ActionDropdown.transform
            .GetChild(6)
            .gameObject.GetComponent<Button>()
            .onClick.RemoveAllListeners();
        EndDrawing = null;
        EndPicking = null;
    }

    private GameObject PickAnEmptyActionUnitAndFillIt(ChessType chesstype, ActionType actiontype)
    {
        for (int i = 0; i < 3; i++)
        {
            if (!ActionUnits[i].GetComponent<OrderShowing>().IsShowing)
            {
                ActionUnits[i].GetComponent<OrderShowing>().IsShowing = true;
                ActionUnits[i].transform.GetChild(0).GetComponent<Image>().sprite =
                    Resources.Load<Sprite>(ResourcePaths.TargetSprites[chesstype]);
                ActionUnits[i].transform.GetChild(1).GetComponent<Image>().sprite =
                    Resources.Load<Sprite>(ResourcePaths.TargetSprites[actiontype]);
                return ActionUnits[i];
            }
        }
        return null;
    }

    private void InitializedButtons()
    {
        ActionUnits.Add(PlanContainer.transform.GetChild(0).gameObject);
        ActionUnits.Add(PlanContainer.transform.GetChild(1).gameObject);
        ActionUnits.Add(PlanContainer.transform.GetChild(2).gameObject);
        clear = PlanContainer.transform.GetChild(3).gameObject;
        clear.GetComponent<Button>().onClick.AddListener(RestoreAll);
    }

    private void RestoreAll()
    {
        ActionList.Clear();
        for (int i = 0; i < 3; i++)
        {
            ActionUnits[i].GetComponent<OrderShowing>().IsShowing = false;
            ActionUnits[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
            ActionUnits[i].transform.GetChild(1).GetComponent<Image>().sprite = null;
        }
        GC.Collect();
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
        GameController.GetInstance().save = save;
    }
}
