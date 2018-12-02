using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableGlyph : MonoBehaviour {

    public MeshRenderer quad;

    public List<Texture> textures;

    [Header("Button number (physical layout)")]
    public int number;

    [Header("Texture index (which symbol)")]
    public int textureIndex;

    void SetTexture(int n)
    {
        quad.material.mainTexture = textures[n];
    }

    public void UpdateTexture()
    {
        SetTexture(textureIndex);
    }

    void Start()
    {
        //if (Application.isEditor)
        //{
        //    SetTextureNumber(number);
        //}
    }

}
