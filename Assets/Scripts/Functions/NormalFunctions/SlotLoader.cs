using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotLoader
{
    public static void LoadGameObjectFromType(ref GameObject slotobject)
    {
        Slot slot = slotobject.GetComponent<SlotComponent>().thisSlot;
        if (
            slot.Landscape != null
            && slot.Landscape.LandscapeType.GetType() == typeof(LandscapeType)
        )
        {
            GameObject CreatedObject = MonoBehaviour.Instantiate(
                EssenitalDatumLoader.GameObjectDictionary[Prefab.Landscape]
            );
            slot.Landscape.UnitSprite = CreatedObject.GetComponent<SpriteRenderer>().sprite;
            slot.Landscape.UnitGameObject = CreatedObject;
            Landscape targetlandscape = CreatedObject
                .GetComponent<LandscapeComponent>()
                .thislandscape;
            targetlandscape = slot.Landscape;
            targetlandscape.PutToSlotPosition(ref slotobject);
            targetlandscape.LoadLandscapeSprite();
            CreatedObject.transform.SetParent(slotobject.transform);
            CreatedObject.transform.localPosition = new Vector3(0, 0, 0.2f);
        }
        if (
            slot.Construction != null
            && slot.Construction.ConstructionType.GetType() == typeof(ConstructionType)
        )
        {
            GameObject CreatedObject = MonoBehaviour.Instantiate(
                EssenitalDatumLoader.GameObjectDictionary[Prefab.Construction]
            );
            slot.Construction.UnitSprite = CreatedObject.GetComponent<SpriteRenderer>().sprite;
            slot.Construction.UnitGameObject = CreatedObject;
            Construction targetconstruction = CreatedObject
                .GetComponent<ConstructionComponent>()
                .thisconstruction;
            targetconstruction = slot.Construction;
            targetconstruction.PutToSlotPosition(ref slotobject);
            targetconstruction.LoadConstructionSprite();
            CreatedObject.transform.localScale *= 0.85f;
            CreatedObject.transform.SetParent(slotobject.transform);
            CreatedObject.transform.localPosition = new Vector3(0, 0, 0.4f);
        }
        if (slot.Chess != null && slot.Chess.ChessType.GetType() == typeof(ChessType))
        {
            GameObject CreatedObject = MonoBehaviour.Instantiate(
                EssenitalDatumLoader.GameObjectDictionary[Prefab.Chess]
            );
            slot.Chess.UnitSprite = CreatedObject.GetComponent<SpriteRenderer>().sprite;
            slot.Chess.UnitGameObject = CreatedObject;
            Chess targetchess = CreatedObject.GetComponent<ChessComponent>().thischess;
            targetchess = slot.Chess;
            targetchess.PutToSlotPosition(ref slotobject);
            targetchess.LoadChessSprite();
            CreatedObject.transform.localScale *= 0.7f;
            CreatedObject.transform.SetParent(slotobject.transform);
            CreatedObject.transform.localPosition = new Vector3(0, 0, 0.6f);
        }
    }
}
