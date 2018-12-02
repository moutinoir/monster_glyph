using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningTrackManager : MonoBehaviour
{
    [Header("Timeline")]
    public float timelinePercentage = 0;

    TempleRoot currentTemple;
    Player player;
    BlocksRoot currentBlocks;

    public delegate void OnReachEnd();
    public OnReachEnd onReachEnd;

    public void Reset()
    {
        timelinePercentage = 0;
        SetPlayerAndBlocksPosition();
    }

    private void Update()
    {
        SetPlayerAndBlocksPosition();
    }

    public void SetPlayerAndBlocksPosition()
    {
        if(currentBlocks != null && currentTemple != null && player != null)
        {
            player.SetPosition(currentTemple.ComputePlayerPosition(timelinePercentage));
            currentBlocks.SetPosition(currentBlocks.ComputeBlocksPosition(timelinePercentage));
        }

        if(timelinePercentage > 0.99f && onReachEnd != null)
        {
            onReachEnd();
        }

        if (currentBlocks == null)
        {
            currentBlocks = FindObjectOfType<BlocksRoot>();
        }

        if (currentTemple == null)
        {
            currentTemple = FindObjectOfType<TempleRoot>();
        }

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }
}
