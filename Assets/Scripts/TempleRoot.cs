using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleRoot : MonoBehaviour
{
    float length;

    [Header("Reference Points")]
    public Transform startPositionAndDirection;
    public Transform endPosition;
    public Transform glyphTablePositionAndDirection;

    void InitLength()
    {
    //    length = Vector3.Dot((endPosition.position - startPositionAndDirection.position), startPositionAndDirection.forward);
        length = Vector3.Distance(startPositionAndDirection.position, endPosition.position);
    }

    //private void Awake()
    void OnEnable()
    {
        InitLength();
    }

    /// <summary>
    /// percentage 0 : the temple is at its start position
    /// percentage 1 : the temple moved all its length along its direction
    /// </summary>
    /// <param name="percentage">from 0 to 1</param>
    public Vector3 ComputePlayerPosition(float percentage)
    {
        return startPositionAndDirection.position + length * percentage * startPositionAndDirection.forward;
    }
}
