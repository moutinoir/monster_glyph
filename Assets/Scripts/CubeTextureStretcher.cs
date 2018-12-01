using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubeTextureStretcher : MonoBehaviour {

    public GridBlock myParentObject;

    void ModifyUV()
    {
        if (myParentObject != null)
        {
            GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(myParentObject.transform.localScale.z, 1));
        }

    }

    void OnEnable()
    {
        ModifyUV();
    }

    void Update()
    {

        if (Application.isEditor)
        {
            ModifyUV();
        }

    }

}
