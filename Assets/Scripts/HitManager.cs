using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public static HitManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<HitManager>();
            }
            return instance;
        }
    }
    private static HitManager instance;

    public delegate void OnHit();
    public OnHit onHit;

    public delegate void OnTilePush(TableGlyph tile);
    public OnTilePush onTilePush;

    public void OnObstacleHit()
    {
        if(onHit != null)
        {
            onHit();
        }
    }

    public void OnPushTile(TableGlyph tile)
    {
        if(onTilePush != null)
        {
            onTilePush(tile);
        }
    }
}
