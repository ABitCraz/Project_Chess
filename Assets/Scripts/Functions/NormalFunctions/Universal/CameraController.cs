using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static void CameraControl(ref GameObject controlled_camera_game_object)
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (
                (Input.GetKey(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") < 0)
                && controlled_camera_game_object.transform.localPosition.z < -0.5f
            )
            {
                controlled_camera_game_object.transform.localPosition =
                    controlled_camera_game_object.transform.localPosition
                    - Vector3.forward * Time.deltaTime;
            }
            if (
                (Input.GetKey(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") > 0)
                && controlled_camera_game_object.transform.localPosition.z > -100f
            )
            {
                controlled_camera_game_object.transform.localPosition =
                    controlled_camera_game_object.transform.localPosition
                    + Vector3.forward * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.R))
            {
                controlled_camera_game_object.transform.localPosition = Vector3.one * -10;
            }
        }
    }
}
