using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController
{
    static float speed = 5f;

    public static void CameraControl(ref GameObject controlled_camera_game_object)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ShiftControl();
        }
        else
        {
            NoShiftControl(ref controlled_camera_game_object);
        }
    }

    private static void NoShiftControl(ref GameObject controlled_camera_game_object)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            controlled_camera_game_object.transform.Translate(
                speed * Time.deltaTime * Vector2.left,
                Space.Self
            );
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            controlled_camera_game_object.transform.Translate(
                speed * Time.deltaTime * Vector2.right,
                Space.Self
            );
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            controlled_camera_game_object.transform.Translate(
                speed * Time.deltaTime * Vector2.up,
                Space.Self
            );
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            controlled_camera_game_object.transform.Translate(
                speed * Time.deltaTime * Vector2.down,
                Space.Self
            );
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            controlled_camera_game_object.transform.position = new Vector3(0, 0, -10);
            speed = 2.5f;
        }
        if (
            (Input.GetKey(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") < 0)
            && controlled_camera_game_object.transform.localPosition.z > -20f
        )
        {
            controlled_camera_game_object.transform.localPosition =
                controlled_camera_game_object.transform.localPosition
                - (Vector3.forward * speed * 25f * Time.deltaTime);
        }
        if (
            (Input.GetKey(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") > 0)
            && controlled_camera_game_object.transform.localPosition.z < -5f
        )
        {
            controlled_camera_game_object.transform.localPosition =
                controlled_camera_game_object.transform.localPosition
                + (Vector3.forward * speed * 25f * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            InitializeCameraGameObject(ref controlled_camera_game_object);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            RotateCameraGameObjectToRotatedRotation(ref controlled_camera_game_object);
        }
    }

    private static void ShiftControl()
    {
        if (
            (Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") < 0)
            && speed > 0.25f
        )
        {
            speed /= 2f;
        }
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") > 0) && speed < 10f)
        {
            speed *= 2f;
        }
    }

    private static void InitializeCameraGameObject(ref GameObject controlled_camera_game_object)
    {
        RotateCameraGameObjectToOriginalRotation(ref controlled_camera_game_object);
        MoveCameraGameObjectToOriginalPosition(ref controlled_camera_game_object);
    }

    private static void RotateCameraGameObjectToRotatedRotation(
        ref GameObject controlled_camera_game_object
    )
    {
        controlled_camera_game_object.transform.Rotate(Vector3.left, 45f, Space.World);
        controlled_camera_game_object.transform.Rotate(Vector3.forward, 45f, Space.World);
    }

    private static void RotateCameraGameObjectToOriginalRotation(
        ref GameObject controlled_camera_game_object
    )
    {
        controlled_camera_game_object.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private static void MoveCameraGameObjectToOriginalPosition(
        ref GameObject controlled_camera_game_object
    )
    {
        controlled_camera_game_object.transform.localPosition = Vector3.forward * -10;
    }
}
