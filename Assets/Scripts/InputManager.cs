using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void OnTrigger();
    public OnTrigger onTrigger;

    private void Update()
    {
        if (Input.GetAxis("RightControllerTrigger") > 0.1f)
        {
            if(onTrigger != null)
            {
                onTrigger();
            }
        }
        else if(Input.GetAxis("LeftControllerTrigger") > 0.1f)
        {
            if (onTrigger != null)
            {
                onTrigger();
            }
        }
    }
}
