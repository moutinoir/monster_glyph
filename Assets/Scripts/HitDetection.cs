using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    void OnTriggerEnter(Collider objectHit)
    {
        if(HitManager.Instance != null)
        {
            HitManager.Instance.OnObstacleHit();
        }
        Debug.Log(objectHit);
    }
}
