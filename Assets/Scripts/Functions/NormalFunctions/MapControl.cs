using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    private void Update()
    {
        if(!Input.GetKey(KeyCode.LeftShift))
        {
            NoShiftControl();
        }
    }

    private void NoShiftControl()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            this.transform.localScale = this.transform.localScale - Vector3.one * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E) || Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            this.transform.localScale = this.transform.localScale + Vector3.one * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.R))
        {
            this.transform.localScale = Vector3.one;
        }
    }
}
