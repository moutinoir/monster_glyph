﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlyphTable : MonoBehaviour {

    public int combinationLength = 4;

    public HashSet<int> combination;

    void RandomizeCombination()
    {
        combination = new HashSet<int>();
        if (combinationLength < 0 || combinationLength > numberOfButtons)
        {
            return;
        }
        while (combination.Count < combinationLength)
        {
            int glyphCandidate = Random.Range(0, 15);
            if (!combination.Contains(glyphCandidate))
            {
                combination.Add(glyphCandidate);
            }
        }

        string combinationDebug = "";
        foreach (int expectedGlyphIndex in combination)
        {
            combinationDebug += expectedGlyphIndex + " ";
        }
        Debug.Log("New combination: " + combinationDebug);

    }

    public bool HasRightCombination()
    {
        bool noButtonsAreMissing = true;
        foreach (int expectedGlyphIndex in combination)
        {
            if (!pushedButtons[expectedGlyphIndex])
            {
                noButtonsAreMissing = false;
            }
        }
        return noButtonsAreMissing;
    }

    public bool HasMadeTooManyMistakes()
    {
        // TODO
        return false;
    }

    public int pushnb;
    public bool push;

    bool[] pushedButtons;

    public int numberOfButtons = 16;
    
    public int textureGridCols = 4;
    public int textureGridRows = 4;

    public RuntimeAnimatorController buttonPush;
    public string buttonPushAnimationName;

    public GlyphTableModel model;

    public Vector3 tableOffsetFromFinalPosition;
    
    TempleRoot temple;

    bool placed = false;

    void SpawnAnimations()
    {
        if (model != null)
        {
            // take first N children
            // this is specific to the FBX internal structure, update code if it changes

            //for (int i = 0; i < model.transform.childCount; i++)
            for (int i = 0; i < numberOfButtons; i++)
            {
                GameObject button = model.transform.GetChild(i).gameObject;
                button.AddComponent<Animator>();
                //button.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Assets/Resources/" + buttonPushAnimationName + ".controller") as RuntimeAnimatorController;
            }

        }
    }

    void PushButton(int i)
    {
        if (model != null)
        {
            if (i > 0 && i < numberOfButtons)
            {
                if (pushedButtons[i])
                {
                    // already pushed: TODO play sound
                } else
                {
                    // ok push it
                    model.transform.GetChild(i).gameObject.GetComponent<Animator>().runtimeAnimatorController = buttonPush;
                    pushedButtons[i] = true;
                }
            }
        }

    }

	void Start () {

        SpawnAnimations();

        pushedButtons = new bool[16];

        RandomizeCombination();

    }
		
	void Update () {

        if (!placed)
        {
            if (temple == null)
            {
                temple = FindObjectOfType<TempleRoot>();
            }

            if (temple != null)
            {
                this.transform.position = temple.endPosition.position + tableOffsetFromFinalPosition;
                placed = true;
            }

        }

        // DEBUG
        if (push)
        {
            PushButton(pushnb);
            push = false;
            if (Application.isEditor)
            {
                if (HasRightCombination())
                {
                    Debug.Log("YOU WIN");
                }
            }
        }

    }
}
