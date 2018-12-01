using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksRoot : MonoBehaviour
{
    float length;

    [Header("Reference Points")]
    public Transform startPositionAndDirection;
    public Transform endPosition;

    void InitLength()
    {
        length = Vector3.Distance(startPositionAndDirection.position, endPosition.position);
    }

    void OnEnable()
    {
        InitLength();
    }

    /// <summary>
    /// percentage 0 : the blocks group is at its start position (same as temple)
    /// percentage 1 : the blocks group moved all its length along its direction (the end of the group matches the end of the temple)
    /// </summary>
    /// <param name="percentage">from 0 to 1</param>
    public Vector3 ComputeBlocksPosition(float percentage)
    {
        return startPositionAndDirection.position + length * percentage * -startPositionAndDirection.forward;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

}
