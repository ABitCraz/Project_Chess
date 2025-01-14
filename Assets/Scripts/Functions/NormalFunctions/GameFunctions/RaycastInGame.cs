using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class RaycastInGame : Singleton<RaycastInGame>
{
    public List<PlanActions> ActionList = new();
    public GameObject StatusSet;
    public GameObject ActionDropdown;
    public GameObject PlanContainer;
    public GameObject LeftBar;
    public Player CurrentPlayer = new();
    public bool IsInControl = true;
    public SavingDatum save;
    public ChessType PutChessType;
    public delegate void ActionController();
    ActionController EndPicking;
    ActionController EndDrawing;
    public ActionController ReadyForDrop;
    public ActionController EndDropping;
    Ray mouseray;
    SlotStatusShow sss = new();
    bool isactiondropdown;
    List<Slot> moveroute = new();
    Coroutine drawroutecoroutine;
    Coroutine picktargetcoroutine;
    Coroutine droptargetcoroutine;
    List<GameObject> ActionUnits = new();
    GameObject clear;
    Slot targetslot;
    bool ispickingtarget = false;
    bool isdrawingtarget = false;
    bool isdroppingtarget = false;

    protected override void Awake()
    {
        base.Awake();
        InitializedButtons();
        ReadyForDrop += () =>
        {
            isdroppingtarget = true;
            LeftBar.GetComponent<ShowLeftBar>().isleftopen = true;
            droptargetcoroutine ??= StartCoroutine(DroppingTargetSlot());
        };

        EndDropping += () =>
        {
            StopCoroutine(droptargetcoroutine);
            droptargetcoroutine = null;
            isdroppingtarget = false;
            CleanUpMap();
        };
    }

    private void Start()
    {
        CurrentPlayer.PlayerNo = PhotonNetwork.LocalPlayer.ActorNumber;
    }

    private void Update()
    {
        if (!isactiondropdown && !ispickingtarget && !isdrawingtarget && !isdroppingtarget)
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
        switch (count)
        {
            case <= 0:
                for (int i = 0; i < ActionUnits.Count; i++)
                {
                    if (ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x < 80)
                    {
                        ActionUnits[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x + 1,
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.y
                        );
                    }
                    else if (ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x > 80)
                    {
                        PlanContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x - 1,
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.y
                        );
                    }
                }
                break;
            case 1:
                for (int i = 0; i < ActionUnits.Count; i++)
                {
                    if (i > 0)
                    {
                        if (ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x > 80)
                        {
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition =
                                new Vector2(
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x
                                        + 1,
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.y
                                );
                        }
                        else if (
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x > 80
                        )
                        {
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition =
                                new Vector2(
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x
                                        - 1,
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.y
                                );
                        }
                    }
                    else
                    {
                        if (ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x < -50)
                        {
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition =
                                new Vector2(
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x
                                        + 1,
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.y
                                );
                        }
                        else if (
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x > -50
                        )
                        {
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition =
                                new Vector2(
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x
                                        - 1,
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.y
                                );
                        }
                    }
                }
                break;
            case 2:
                for (int i = 0; i < ActionUnits.Count; i++)
                {
                    if (i > 1)
                    {
                        if (ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x > 80)
                        {
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition =
                                new Vector2(
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x
                                        + 1,
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.y
                                );
                        }
                        else if (
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x > 80
                        )
                        {
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition =
                                new Vector2(
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x
                                        - 1,
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.y
                                );
                        }
                    }
                    else
                    {
                        if (ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x < -50)
                        {
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition =
                                new Vector2(
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x
                                        + 1,
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.y
                                );
                        }
                        else if (
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x > -50
                        )
                        {
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition =
                                new Vector2(
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x
                                        - 1,
                                    ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.y
                                );
                        }
                    }
                }
                break;
            case >= 3:
                for (int i = 0; i < ActionUnits.Count; i++)
                {
                    if (ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x < -50)
                    {
                        ActionUnits[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x + 1,
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.y
                        );
                    }
                    else if (ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x > -50)
                    {
                        ActionUnits[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.x - 1,
                            ActionUnits[i].GetComponent<RectTransform>().anchoredPosition.y
                        );
                    }
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
            movableslots = sss.ShowMovementRange(currentslot, save.SlotMap);
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
                    EndDrawing?.Invoke();
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
                        EndPicking?.Invoke();
                        break;
                    }
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator DroppingTargetSlot()
    {
        CleanUpMap();
        isdroppingtarget = true;
        while (true)
        {
            mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(mouseray);
            yield return new WaitForEndOfFrame();
        }
    }

    public void CleanUpMap()
    {
        for (int i = 0; i < save.SlotMap.FullSlotMap.Length; i++)
        {
            //save.SlotMap.FullSlotMap[i].SlotGameObject.GetComponent<SlotComponent>().UnfocusingActions();
        }
    }

    private void GiveButtonActions(Slot slot)
    {
        CleanUpButtonFunctions();
        PlanActions planaction = new(slot.Position, ActionType.Attack, slot, CurrentPlayer);
        ActionDropdown
            .transform.GetChild(1)
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
        ActionDropdown
            .transform.GetChild(2)
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
        ActionDropdown
            .transform.GetChild(3)
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
        ActionDropdown
            .transform.GetChild(4)
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
        ActionDropdown
            .transform.GetChild(5)
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                isactiondropdown = false;
                ActionList.Add(planaction.Repair());
                PickAnEmptyActionUnitAndFillIt(slot.Chess.ChessType, ActionType.Repair);
                ActionDropdown.SetActive(false);
                return;
            });
        ActionDropdown
            .transform.GetChild(6)
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
        ActionDropdown
            .transform.GetChild(1)
            .gameObject.GetComponent<Button>()
            .onClick.RemoveAllListeners();
        ActionDropdown
            .transform.GetChild(2)
            .gameObject.GetComponent<Button>()
            .onClick.RemoveAllListeners();
        ActionDropdown
            .transform.GetChild(3)
            .gameObject.GetComponent<Button>()
            .onClick.RemoveAllListeners();
        ActionDropdown
            .transform.GetChild(4)
            .gameObject.GetComponent<Button>()
            .onClick.RemoveAllListeners();
        ActionDropdown
            .transform.GetChild(5)
            .gameObject.GetComponent<Button>()
            .onClick.RemoveAllListeners();
        ActionDropdown
            .transform.GetChild(6)
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
        ActionUnits[0].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(RestoreAll);
        ActionUnits[1].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(RestoreAll);
        ActionUnits[2].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(RestoreAll);
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
}
