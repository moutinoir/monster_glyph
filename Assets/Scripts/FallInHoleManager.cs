using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallInHoleManager : MonoBehaviour
{
    Hole hole;
    TempleRoot temple;
    Player player;

    private void GetReferences()
    {
        if(hole == null)
        {
            hole = FindObjectOfType<Hole>();
        }

        if(temple == null)
        {
            temple = FindObjectOfType<TempleRoot>();
        }

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }

    public void MoveHoleDisableFloor()
    {
        GetReferences();
        hole.SetPosition(player.transform.position);
        temple.HideFloor();
    }

    public void DisplayFloorHideHole()
    {
        GetReferences();
        temple.ShowFloor();
        hole.Hide();
    }
}
