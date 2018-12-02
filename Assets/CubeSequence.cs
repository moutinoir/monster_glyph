using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubeSequence : MonoBehaviour {

    public float sequenceLength;

    void RecalcLength()
    {
        sequenceLength = 0;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GridBlock isItABlock = transform.GetChild(i).GetComponent<GridBlock>();
            if (isItABlock != null)
            {
                if (isItABlock.transform.localPosition.z > sequenceLength)
                {
                    sequenceLength = isItABlock.transform.localPosition.z;
                }
            }
        }
    }

	void Start () {
		
	}
	
	void Update () {

        if (Application.isEditor)
        {
            RecalcLength();
        }

	}
}
