using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleManager : MonoBehaviour
{
    [Header("Timeline")]
    public float timelinePercentage = 0;

    TempleRoot currentTemple;

    private void Update()
    {
        if(currentTemple != null)
        {
            currentTemple.SetPosition(timelinePercentage);
        }
        else
        {
            currentTemple = FindObjectOfType<TempleRoot>();
        }
    }
}
