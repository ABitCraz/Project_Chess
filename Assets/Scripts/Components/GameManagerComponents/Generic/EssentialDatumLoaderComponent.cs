using System;
using System.Collections.Generic;
using UnityEngine;

public class EssentialDatumLoaderComponent : MonoBehaviour
{
    EssentialDatumLoader essential_datum_loader = new();
    public static Dictionary<Enum, GameObject> GameObjectDictionary;
    public static Dictionary<Enum, Sprite> SpriteDictionary;

    private void Awake()
    {
        essential_datum_loader.ReloadDictionary(ref GameObjectDictionary, ref SpriteDictionary);
    }
}
