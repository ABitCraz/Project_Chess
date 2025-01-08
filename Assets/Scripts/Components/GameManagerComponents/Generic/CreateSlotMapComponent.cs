using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateSlotMapComponent : MonoBehaviour
{
    CreateSlotMap create_slot_map = new();
    public GameObject MapLoaderGameObject;
    public GameObject EssentialDatumGameObject;

    private void Start()
    {
        GameObject current_game_object = this.gameObject;
        create_slot_map.CreateSlotMapComponentActionsOnStart(
            ref current_game_object,
            MapLoaderGameObject,
            EssentialDatumGameObject
        );
    }
}
