using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class CubeSequence : MonoBehaviour {

    public List<GlyphTimelinePlaceholder> allSequencePlaceholders;
    public List<GridBlock> allBlocks;

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

    void PollPlaceholders()
    {
        allSequencePlaceholders = GetComponentsInChildren<GlyphTimelinePlaceholder>().ToList<GlyphTimelinePlaceholder>();
    }

    void PollBlocks()
    {
        allBlocks = GetComponentsInChildren<GridBlock>().ToList<GridBlock>();
    }

    void Start () {
		
	}
	
	void Update () {

        if (Application.isEditor)
        {
            RecalcLength();
            PollPlaceholders();
            PollBlocks();
        }

	}
}
