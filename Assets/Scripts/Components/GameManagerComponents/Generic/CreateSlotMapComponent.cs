using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateSlotMapComponent : MonoBehaviour
{
    CreateSlotMap create_slot_map = new();
    public GameObject MapLoaderGameObject;
    GameObject GenerateMapButton;
    GameObject XSlotInputGameObject;
    GameObject YSlotInputGameObject;

    private void Awake()
    {
        GenerateMapButton = this.transform.GetChild(0).gameObject;
        XSlotInputGameObject = this.transform.GetChild(1).gameObject;
        YSlotInputGameObject = this.transform.GetChild(2).gameObject;
        GenerateMapButton
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                create_slot_map.InitializeMapGameObject(
                    ref MapLoaderGameObject,
                    XSlotInputGameObject.GetComponent<TMP_InputField>().text,
                    YSlotInputGameObject.GetComponent<TMP_InputField>().text
                );
            });
    }
}
