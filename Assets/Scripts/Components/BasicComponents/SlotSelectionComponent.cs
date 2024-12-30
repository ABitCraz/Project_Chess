using Unity.VisualScripting;
using UnityEngine;
using static ObjectActions;

public class SlotSelectionComponent : MonoBehaviour
{
    public static GetGameObjectFromRaycasts ShowHoverGameObject;
    public static GetGameObjectFromRaycasts ToggleSelectionGameObject;
    public static GetGameObjectFromRaycasts HideHoverGameObject;
    public static GameObjectAction DisableHoverAndSelectionGameObject;
    GameObject hover_game_object;
    GameObject selection_game_object;

    private void Awake()
    {
        hover_game_object = this.transform.GetChild(0).gameObject;
        selection_game_object = this.transform.GetChild(1).gameObject;
        ToggleSelectionGameObject += ToggleSelection;
        ShowHoverGameObject += EnableHover;
        HideHoverGameObject += DisableHover;
        DisableHoverAndSelectionGameObject += DisableHoverAndSelection;
    }

    private void EnableHover(ref GameObject hit_game_object)
    {
        if (hit_game_object == this.gameObject && !hover_game_object.activeSelf)
            hover_game_object.SetActive(true);
    }

    private void ToggleSelection(ref GameObject hit_game_object)
    {
        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            if (hit_game_object == this.gameObject)
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
        if (hit_game_object != this.gameObject && hover_game_object.activeSelf)
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
