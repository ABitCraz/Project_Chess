using Unity.VisualScripting;
using UnityEngine;

public class ShootMouseRayComponent : MonoBehaviour
{
    public ShootMouseRay shoot_mouse_ray = new();

    private void Update()
    {
        shoot_mouse_ray.MouseRayComponentUpdateAction?.Invoke();
    }
}
