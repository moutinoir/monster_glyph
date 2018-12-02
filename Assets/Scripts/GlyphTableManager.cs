﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphTableManager : MonoBehaviour
{
    GlyphTable glyphTable;

    public void GetGlyphTable()
    {
        if(glyphTable == null)
        {
            glyphTable = FindObjectOfType<GlyphTable>();
        }
    }

    public bool HasRightCombination()
    {
        GetGlyphTable();
        return glyphTable.HasRightCombination();
    }

    public bool HasMadeTooManyMistakes()
    {
        GetGlyphTable();
        return glyphTable.HasMadeTooManyMistakes();
    }

    public void OnTilePush(TableGlyph tile)
    {
        GetGlyphTable();
        glyphTable.PushButton(tile.number);
    }

    public void ResetGlyphTable()
    {
        GetGlyphTable();
        glyphTable.OnRestartLevel();
    }
}
