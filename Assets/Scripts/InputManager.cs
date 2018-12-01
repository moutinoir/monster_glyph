using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void OnTrigger();
    public OnTrigger onTrigger;

    bool triggerRight;
    bool triggerLeft;

    private void Update()
    {
        if (Input.GetAxis("RightControllerTrigger") > 0.9f && triggerRight == false)
        {
            triggerRight = true;
            if(onTrigger != null)
            {
                onTrigger();
            }
        }
        else if (Input.GetAxis("RightControllerTrigger") < 0.01f)
        {
            triggerRight = false;
        }

        if(Input.GetAxis("LeftControllerTrigger") > 0.9f && triggerLeft == false)
        {
            triggerLeft = true;
            if (onTrigger != null)
            {
                onTrigger();
            }
        }
        else if (Input.GetAxis("LeftControllerTrigger") < 0.01f)
        {
            triggerLeft = false;
        }
    }
}
