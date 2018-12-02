using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [Header("Reference Transforms")]
    public Transform fallEnd;
    public Transform hiddenPosition;

    [Header("Reference")]
    public MonsterCarreauCrew monsterCrew; 

    Vector3 position;
    public void SetPosition(Vector3 playerPosition)
    {
        position = transform.position;
        position.x = playerPosition.x;
        position.y = 0;
        position.z = playerPosition.z;
        transform.position = position;
    }

    public void Hide()
    {
        transform.position = hiddenPosition.position;
    }
}
