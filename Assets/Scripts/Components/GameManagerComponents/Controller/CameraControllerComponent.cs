using UnityEngine;

public class CameraControllerComponent : MonoBehaviour
{
    GameObject MainCamera;

    private void Awake()
    {
        MainCamera = Camera.main.gameObject;
    }

    private void Update()
    {
        CameraController.CameraControl(ref MainCamera);
    }
}
