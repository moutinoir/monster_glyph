using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleManager : MonoBehaviour
{
    [Header("Timeline")]
    public float timelinePercentage = 0;

    TempleRoot currentTemple;
    Player player;

    private void Update()
    {
        if(currentTemple != null && player != null)
        {
            player.SetPosition(currentTemple.SetPlayerPosition(timelinePercentage));
        }

        if (currentTemple == null)
        {
            currentTemple = FindObjectOfType<TempleRoot>();
        }

        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }
}
