using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class SequencesLD : MonoBehaviour {

    public List<CubeSequence> allSequences;
    
    void Start () {
		
	}

    void PollSequences()
    {
        allSequences = GetComponentsInChildren<CubeSequence>().ToList<CubeSequence>();
    }

    void Update () {
        if (Application.isEditor)
        {
            PollSequences();
        }
    }
}
