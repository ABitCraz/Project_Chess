using System;
using System.Collections.Generic;
using UnityEngine;

public class EssentialDatumLoader
{
    public void ReloadDictionary(
        ref Dictionary<Enum, GameObject> game_object_dictionary,
        ref Dictionary<Enum, Sprite> sprite_dictionary
    )
    {
        game_object_dictionary = new();
        sprite_dictionary = new();
        LoadWholeGameObjectDictionary(ref game_object_dictionary);
        LoadWholeSpriteDictionary(ref sprite_dictionary);
    }

    private void LoadWholeGameObjectDictionary(
        ref Dictionary<Enum, GameObject> game_object_dictionary
    )
    {
        for (int i = 1; i < Enum.GetNames(typeof(Prefab)).Length; i++)
        {
            game_object_dictionary.Add(
                (Prefab)i,
                Resources.Load<GameObject>(ResourcePaths.Resources[(Prefab)i])
            );
        }
    }

    private void LoadWholeSpriteDictionary(ref Dictionary<Enum, Sprite> sprite_dictionary)
    {
        for (int i = 1; i < Enum.GetNames(typeof(LandscapeType)).Length; i++)
        {
            sprite_dictionary.Add(
                (LandscapeType)i,
                Resources.Load<Sprite>(ResourcePaths.TargetSprites[(LandscapeType)i])
            );
        }
        for (int i = 1; i < Enum.GetNames(typeof(ConstructionType)).Length; i++)
        {
            sprite_dictionary.Add(
                (ConstructionType)i,
                Resources.Load<Sprite>(ResourcePaths.TargetSprites[(ConstructionType)i])
            );
        }
        for (int i = 1; i < Enum.GetNames(typeof(ChessType)).Length; i++)
        {
            sprite_dictionary.Add(
                (ChessType)i,
                Resources.Load<Sprite>(ResourcePaths.TargetSprites[(ChessType)i])
            );
        }
        for (int i = 1; i < Enum.GetNames(typeof(ActionType)).Length; i++)
        {
            sprite_dictionary.Add(
                (ActionType)i,
                Resources.Load<Sprite>(ResourcePaths.TargetSprites[(ActionType)i])
            );
        }
    }
}
