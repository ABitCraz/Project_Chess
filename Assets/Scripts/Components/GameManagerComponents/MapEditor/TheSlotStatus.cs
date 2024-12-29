using UnityEngine;
using static ObjectActions;

public class TheSlotStatus : MonoBehaviour
{
    public static ShowTargetGameObjectInfoToStatusTextBox ShowTargetSlotStatus;
    public static GameObjectAction DisableStatusGameObject;

    private void Awake()
    {
        ShowTargetSlotStatus += ShowSlotStatusDetails;
        DisableStatusGameObject += () =>
        {
            this.gameObject.SetActive(false);
        };
    }

    private void ShowSlotStatusDetails(ref GameObject slot_game_object)
    {
        GameObject current_status_game_object = this.gameObject;
        if (!this.gameObject.activeSelf)
            current_status_game_object.SetActive(true);
        ShowStatus.ShowSlotStatus(ref slot_game_object, ref current_status_game_object);
    }
}
