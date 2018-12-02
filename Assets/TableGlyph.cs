using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableGlyph : MonoBehaviour {

    public MeshRenderer quad;

    public List<Texture> textures;

    [Header("Editor debug only:")]
    public int number;

    public void SetTextureNumber(int n)
    {
        quad.material.mainTexture = textures[n];
    }

    void Start()
    {
        if (Application.isEditor)
        {
            SetTextureNumber(number);
        }
    }

}
