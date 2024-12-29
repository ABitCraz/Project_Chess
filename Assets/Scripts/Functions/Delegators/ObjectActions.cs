using UnityEngine;

public class ObjectActions
{
    public delegate void SlotUpdateActions();
    public delegate void GetGameObjectFromRaycasts(ref GameObject hit_game_object);
    public delegate void GetGameObjectsFromRaycasts(ref GameObject[] hit_game_objects);
    public delegate void ShowTargetGameObjectInfoToStatusTextBox(ref GameObject status_game_object);
    public delegate void GameObjectAction();
}
