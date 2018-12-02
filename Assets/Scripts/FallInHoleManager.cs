using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallInHoleManager : MonoBehaviour
{
    Hole hole;
    TempleRoot temple;
    Player player;

    public delegate void OnHitBottom();
    public OnHitBottom onHitBottom;

    [Header("Fall")]
    public float fallDuration = 8;

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

    Vector3 fallStart;
    Vector3 fallEnd;
    float fallTime;
    bool fall;
    public void MoveHoleDisableFloor()
    {
        GetReferences();
        hole.SetPosition(player.transform.position);
        fallStart = player.transform.position;
        fallEnd = hole.fallEnd.position;
        fallTime = Time.time;
        temple.HideFloor();
        fall = true;
    }

    public void FallUpdate()
    {
        if(fall)
        {
            player.SetPosition(Vector3.Lerp(fallStart, fallEnd, Mathf.Clamp01((Time.time - fallTime)/fallDuration)));
            if((Time.time - fallTime) / fallDuration > 1)
            {
                fall = false;
                if(onHitBottom != null)
                {
                    onHitBottom();
                }
            }
        }
    }

    public void DisplayFloorHideHole()
    {
        GetReferences();
        temple.ShowFloor();
        hole.Hide();
    }
}
