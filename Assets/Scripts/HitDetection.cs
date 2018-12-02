using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    int damagePlayerLayer;
    int glyphTableTileLayer;

    private void Start()
    {
        damagePlayerLayer = LayerMask.NameToLayer("DamagePlayer");
        glyphTableTileLayer = LayerMask.NameToLayer("GlyphTableTileLayer");
    }

    void OnTriggerEnter(Collider objectHit)
    {
        if(HitManager.Instance != null)
        {
            if(objectHit.gameObject.layer == damagePlayerLayer)
            {
                HitManager.Instance.OnObstacleHit();
            }
            else if (objectHit.gameObject.layer == glyphTableTileLayer)
            {
                TableGlyph tile = objectHit.gameObject.GetComponentInParent<TableGlyph>();
                HitManager.Instance.OnPushTile(tile);
            }

        }
        Debug.Log(objectHit);
    }
}
