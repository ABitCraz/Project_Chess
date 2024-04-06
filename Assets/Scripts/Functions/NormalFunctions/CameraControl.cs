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
    public GameObject cameraspeed;
    public GameObject speeddashboard;
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
            maincamera.transform.Translate(speed * Time.deltaTime * Vector2.left);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            maincamera.transform.Translate(speed * Time.deltaTime * Vector2.right);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            maincamera.transform.Translate(speed * Time.deltaTime * Vector2.up);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            maincamera.transform.Translate(speed * Time.deltaTime * Vector2.down);
        }
    }

    private void ShiftControl()
    {
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") < 0) && speed > 0.25f)
        {
            speed /= 1.5f;
            CalibrateSpeedToggle(speed);
        }
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") > 0) && speed < 10f)
        {
            speed *= 1.5f;
            CalibrateSpeedToggle(speed);
        }
    }

    private void CalibrateSpeedToggle(float speed)
    {
        int speedlog = (int)Mathf.Log(speed, 1.5f);
        speeddashboard.GetComponent<TMP_Text>().text = string.Format("x1.5(^{0})", speedlog);
        cameraspeed.GetComponent<Slider>().value = (speedlog + 5f) / 10f;
    }
}
