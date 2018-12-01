using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadGroundOffset : MonoBehaviour {

    public float GetOffset()
    {
        return transform.localPosition.y;
    }

    public void SetOffset(float offset)
    {
        transform.localPosition = Vector3.up * offset;
    }

}
