using UnityEngine;

public class EssentialDatumLoaderComponent : MonoBehaviour
{
    private void Awake()
    {
        EssentialDatumLoader.ReloadDictionary();
    }
}
