using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GroundOffsetAdjustment : MonoBehaviour {

    // TODO adjust when starting level

    public bool adjustPlease;

    public float gridCenterToHeadOffsetRatio = 1;

    public PlayerHeadGroundOffset detectableOffset;

    public void Adjust()
    {
        transform.localScale = new Vector3(
            transform.localScale.x,
            detectableOffset == null ? transform.localScale.y : detectableOffset.transform.localPosition.y * gridCenterToHeadOffsetRatio,
            transform.localScale.z
        );
    }

	void Update () {

        bool willAdjust = false;

        if (Application.isEditor)
        {
            willAdjust = true;
        }

        if (adjustPlease)
        {
            adjustPlease = false;
            willAdjust = true;
        }

        if (willAdjust)
        {
            Adjust();
        }

        if (detectableOffset == null)
        {
            detectableOffset = FindObjectOfType<PlayerHeadGroundOffset>();
        }

	}
}
