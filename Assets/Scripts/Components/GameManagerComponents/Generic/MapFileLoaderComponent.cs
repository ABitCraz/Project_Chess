using UnityEngine;

public class MapFileLoaderComponent : MonoBehaviour
{
    public GameObject MapLoaderGameObject;
    public MapFileLoader map_file_loader = new();

    private void Awake()
    {
        map_file_loader.InitializeMapFileLoader(ref MapLoaderGameObject);
    }
}
