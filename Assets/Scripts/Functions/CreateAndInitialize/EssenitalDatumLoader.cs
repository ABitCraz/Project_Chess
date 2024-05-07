using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenitalDatumLoader : MonoBehaviour
{
    public static Dictionary<Enum, Sprite> SpriteDictionary = new();
    public static Dictionary<Enum, GameObject> GameObjectDictionary = new();

    private void Awake()
    {
        LoadWholeGameObjectDictionary();
        LoadWholeSpriteDictionary();
    }

    public static void ReloadDictionary()
    {
        SpriteDictionary = new();
        GameObjectDictionary = new();
    }

    private void LoadWholeGameObjectDictionary()
    {
        for (int i = 1; i < Enum.GetNames(typeof(Prefab)).Length; i++)
        {
            GameObjectDictionary.Add(
                (Prefab)i,
                Resources.Load<GameObject>(ResourcePaths.Resources[(Prefab)i])
            );
        }
    }

    private void LoadWholeSpriteDictionary()
    {
        for (int i = 1; i < Enum.GetNames(typeof(LandscapeType)).Length; i++)
        {
            SpriteDictionary.Add(
                (LandscapeType)i,
                Resources.Load<Sprite>(ResourcePaths.TargetSprites[(LandscapeType)i])
            );
        }
        for (int i = 1; i < Enum.GetNames(typeof(ConstructionType)).Length; i++)
        {
            SpriteDictionary.Add(
                (ConstructionType)i,
                Resources.Load<Sprite>(ResourcePaths.TargetSprites[(ConstructionType)i])
            );
        }
        for (int i = 1; i < Enum.GetNames(typeof(ChessType)).Length; i++)
        {
            SpriteDictionary.Add(
                (ChessType)i,
                Resources.Load<Sprite>(ResourcePaths.TargetSprites[(ChessType)i])
            );
        }
        for (int i = 1; i < Enum.GetNames(typeof(ActionType)).Length; i++)
        {
            SpriteDictionary.Add(
                (ActionType)i,
                Resources.Load<Sprite>(ResourcePaths.TargetSprites[(ActionType)i])
            );
        }
    }
}
