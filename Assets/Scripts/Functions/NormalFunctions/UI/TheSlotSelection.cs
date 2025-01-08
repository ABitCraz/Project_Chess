using Unity.VisualScripting;
using UnityEngine;
using static ObjectActions;

public class SlotSelection
{
    public GetGameObjectFromRaycasts ShowHoverGameObject;
    public GetGameObjectFromRaycasts ToggleSelectionGameObject;
    public GetGameObjectFromRaycasts HideHoverGameObject;
    public GameObjectAction DisableHoverAndSelectionGameObject;

    GameObject slot_selection_game_object;
    GameObject hover_game_object;
    GameObject selection_game_object;

    public SlotSelection(ref GameObject slot_selection_game_object)
    {
        this.slot_selection_game_object = slot_selection_game_object;
    }

    private void SlotSelectionActionsOnAwake()
    {
        hover_game_object = slot_selection_game_object.transform.GetChild(0).gameObject;
        selection_game_object = slot_selection_game_object.transform.GetChild(1).gameObject;
        ToggleSelectionGameObject += ToggleSelection;
        ShowHoverGameObject += EnableHover;
        HideHoverGameObject += DisableHover;
        DisableHoverAndSelectionGameObject += DisableHoverAndSelection;
    }

    private void EnableHover(ref GameObject hit_game_object)
    {
        if (
            hit_game_object == slot_selection_game_object.gameObject
            && !hover_game_object.activeSelf
        )
            hover_game_object.SetActive(true);
    }

    private void ToggleSelection(ref GameObject hit_game_object)
    {
        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            if (hit_game_object == slot_selection_game_object.gameObject)
            {
                selection_game_object.SetActive(true);
            }
            else
            {
                selection_game_object.SetActive(false);
            }
        }
    }

    private void DisableHover(ref GameObject hit_game_object)
    {
        if (
            hit_game_object != slot_selection_game_object.gameObject
            && hover_game_object.activeSelf
        )
            hover_game_object.SetActive(false);
    }

    private void DisableHoverAndSelection()
    {
        hover_game_object.SetActive(false);
        selection_game_object.SetActive(false);
    }

    private void OnDestroy()
    {
        ToggleSelectionGameObject = null;
        ShowHoverGameObject = null;
        HideHoverGameObject = null;
        DisableHoverAndSelectionGameObject = null;
    }
}
