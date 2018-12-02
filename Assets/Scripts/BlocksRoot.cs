using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksRoot : MonoBehaviour
{

    public GridParams theBlocksRoot;

    public SequencesLD availableSequences;

    // public GridBlock cubePrefab;
    public List<GameObject> sequences;

    float length;

    [Header("Reference Points")]
    public Transform startPositionAndDirection;
    public Transform endPosition;

    void InitLength()
    {
        length = Vector3.Distance(startPositionAndDirection.position, endPosition.position);
    }

    void OnEnable()
    {
        InitLength();
        if (availableSequences != null)
        {
            availableSequences.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// percentage 0 : the blocks group is at its start position (same as temple)
    /// percentage 1 : the blocks group moved all its length along its direction (the end of the group matches the end of the temple)
    /// </summary>
    /// <param name="percentage">from 0 to 1</param>
    public Vector3 ComputeBlocksPosition(float percentage)
    {
        return startPositionAndDirection.position + length * percentage * -startPositionAndDirection.forward;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void DestroyAllBlocks()
    {
        for (int i = level.Count-1; i >= 0 ; i--)
        {
            DestroyImmediate(level[i]);
        }
        level = new List<GameObject>();
    }

    List<GameObject> level;

    void AddCubeSequence(CubeSequence seq, float offset)
    {
        level = new List<GameObject>();
        foreach (GridBlock blockTemplate in seq.allBlocks)
        {
            Vector3 localPosInLD = blockTemplate.transform.localPosition;
            GridBlock copy = Instantiate(blockTemplate, this.theBlocksRoot.transform);
            copy.transform.localPosition = localPosInLD + Vector3.forward * offset;
            level.Add(copy.gameObject);
        }
    }

    public void SpawnLevel(List<Texture> theGlyphs, int showEachGlyphNTimes)
    {

        List<GlyphTimelinePlaceholder> allGlyphPlaceholders = new List<GlyphTimelinePlaceholder>();

        bool canAddMoreSequences = true;
        float totalSequencesLength = 0;
        float remainingAvailableLength = length;
        while (totalSequencesLength < length && canAddMoreSequences)
        {
            // add a sequence if possible
            List<CubeSequence> candidates = new List<CubeSequence>();
            foreach (CubeSequence seq in availableSequences.allSequences)
            {
                if (seq.sequenceLength <= remainingAvailableLength)
                {
                    candidates.Add(seq);
                }
            }
            int seqIndex = Random.Range(0, candidates.Count - 1);
            CubeSequence chosenSequence = candidates[seqIndex];
            AddCubeSequence(chosenSequence, totalSequencesLength);
            allGlyphPlaceholders.AddRange(chosenSequence.allSequencePlaceholders);
            totalSequencesLength += chosenSequence.sequenceLength;
            remainingAvailableLength -= chosenSequence.sequenceLength;

            // check if we can loop again
            bool atLeastOneSequenceCanFit = false;
            foreach (CubeSequence seq in availableSequences.allSequences)
            {
                if (seq.sequenceLength <= remainingAvailableLength)
                {
                    atLeastOneSequenceCanFit = true;
                }
            }
            canAddMoreSequences = atLeastOneSequenceCanFit;
        }

        // spawn glyphs on placeholders
        foreach (GlyphTimelinePlaceholder potentialGlyph in allGlyphPlaceholders)
        {
            // FOR NOW, randomizes the glyph and displays on each placeholder
            // TODO: randomize the number of appearances
            potentialGlyph.myQuad.material.mainTexture = theGlyphs[Random.Range(0, theGlyphs.Count - 1)];
        }

    }

}
