using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TempleManager : MonoBehaviour
{
    [Header("Timeline Percentage")]
    public float timelinePercentage = 0;

    [Header("Current Temple")]
    public TempleRoot currentTemple;

    private void Update()
    {
        if(currentTemple != null)
        {
            currentTemple.SetPosition(timelinePercentage);
        }
    }
}
