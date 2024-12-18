using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CameraControl : MonoBehaviour
{
    Camera main_camera;
    float speed = 2.5f;

    private void Awake()
    {
        main_camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ShiftControl();
        }
        else
        {
            NoShiftControl();
        }
    }

    private void NoShiftControl()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            main_camera.transform.Translate(speed * Time.deltaTime * Vector2.left, Space.Self);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            main_camera.transform.Translate(speed * Time.deltaTime * Vector2.right, Space.Self);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            main_camera.transform.Translate(speed * Time.deltaTime * Vector2.up, Space.Self);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            main_camera.transform.Translate(speed * Time.deltaTime * Vector2.down, Space.Self);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            main_camera.transform.position = new Vector3(0, 0, -10);
            speed = 2.5f;
        }
    }

    private void ShiftControl()
    {
        if (
            (Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") < 0) && speed > 0.25f
        )
        {
            speed /= 1.5f;
        }
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") > 0) && speed < 10f)
        {
            speed *= 1.5f;
        }
    }
}
