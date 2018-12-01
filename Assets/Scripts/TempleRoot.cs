using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleRoot : MonoBehaviour
{
    [Header("Temple Dimensions")]
    public float length = 20;

    [Header("Position and Direction")]
    public Transform startPositionAndDirection;

    Vector3 position;
    /// <summary>
    /// percentage 0 : the temple is at its start position
    /// percentage 1 : the temple moved all its length along its direction
    /// </summary>
    /// <param name="percentage">from 0 to 1</param>
    public Vector3 SetPlayerPosition(float percentage)
    {
        position = startPositionAndDirection.position + length * percentage * startPositionAndDirection.forward;
        return position;
    }
}
