using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CameraControl : MonoBehaviour
{
    Camera maincamera;
    float speed = 1.0f;

    private void Awake()
    {
        maincamera = Camera.main;
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
            maincamera.transform.Translate(speed * Time.deltaTime * Vector2.left, Space.Self);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            maincamera.transform.Translate(speed * Time.deltaTime * Vector2.right, Space.Self);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            maincamera.transform.Translate(speed * Time.deltaTime * Vector2.up, Space.Self);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            maincamera.transform.Translate(speed * Time.deltaTime * Vector2.down, Space.Self);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            maincamera.transform.position = new Vector3(0, 0, -10);
            speed = 1.0f;
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
