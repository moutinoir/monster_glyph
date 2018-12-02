﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlyphTable : MonoBehaviour {

    public BlocksRoot currentBlocksRoot;

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

    int numberOfAllowedMistakes;

    public bool HasMadeTooManyMistakes()
    {
        bool atLeastOneIsWrong = false;
        for (int i = 0; i < pushedButtons.Length; i++)
        {
            bool isPressed = pushedButtons[i];
            if (isPressed && !combination.Contains(i))
            {
                atLeastOneIsWrong = true;
            }
        }
        return atLeastOneIsWrong;
    }

    // the combination to press is expressed in BUTTON INDEX on the table
    // at the beginning of each game we randomize which texture goes on which button
    // but the combination 0,1,2,3 for example means you have to press the 4 buttons on top of the table, no matter what textures are on it
    // the game is smart and knows which textures should be displayed in the timeline depending on what says the table
    // the indices of this table are the buttons numbers (physical layout) and the values are the texture indices (which symbol)
    List<int> buttonToTextureMapping;

    void InitTextureMapping()
    {
        buttonToTextureMapping = new List<int>();
        for (int i = 0; i < buttons.Count; i++)
        {
            buttonToTextureMapping.Add(i);
        }
    }

    void RandomizeTexturesIndex()
    {
        InitTextureMapping();
        for (int i = 0; i < buttonToTextureMapping.Count; i++)
        {
            int temp = buttonToTextureMapping[i];
            int randomIndex = Random.Range(0, buttonToTextureMapping.Count);
            buttonToTextureMapping[i] = buttonToTextureMapping[randomIndex];
            buttonToTextureMapping[randomIndex] = temp;
        }
        SetTextures();
    }

    void SetTextures()
    {
        if (buttons != null)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                TableGlyph button = buttons[i];
                button.textureIndex = buttonToTextureMapping[i];
                button.UpdateTexture();
            }
        }
    }

    public List<Texture> GetTexturesFromExpectedCombination()
    {
        List<Texture> toReturn = new List<Texture>();
        for (int i = 0; i < combination.Count; i++)
        {
            toReturn.Add(glyphButtonPrefab.textures[buttonToTextureMapping[combination.ElementAt(i)]]);
        }
        return toReturn;
    }

    bool[] pushedButtons;

    public int numberOfButtons = 16;
    
    public int textureGridCols = 4;
    public int textureGridRows = 4;

    public RuntimeAnimatorController buttonPush;
    public string buttonPushAnimationName;

    public GlyphTableModel model;

    public TableGlyph glyphButtonPrefab;

    public Vector3 glyphButtonScale = new Vector3(0.4f,0.3f,1);

    public Vector3 tableOffsetFromFinalPosition;
    
    TempleRoot temple;

    public List<TableGlyph> buttons;

    bool placed = false;

    bool glyphsSpawnedIntimeline;

    public int showEachGlyphNTimes = 2;

    [HeaderAttribute("Debug")]
    public bool restart;
    public int pushnb;
    public bool push;

    void ResetAnimationsAndButtonsStates()
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            //GameObject button = model.transform.GetChild(0).gameObject;
            model.transform.GetChild(i).gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
            pushedButtons[i] = false;
        }
    }

    void SpawnAnimations()
    {

        buttons = new List<TableGlyph>();

        if (model != null)
        {
            // take first N children
            // this is specific to the FBX internal structure, update code if it changes

            //for (int i = 0; i < model.transform.childCount; i++)
            for (int i = 0; i < numberOfButtons; i++)
            {
                GameObject button = model.transform.GetChild(0).gameObject;
                buttons.Add(Instantiate<TableGlyph>(glyphButtonPrefab, button.transform));
                buttons[i].transform.localScale = glyphButtonScale;
                buttons[i].number = i;
                GameObject animatedParent = new GameObject("button " + i + " anim root");
                animatedParent.transform.parent = model.transform;
                animatedParent.transform.localPosition = new Vector3(0, 0, 0.1f);
                animatedParent.transform.localEulerAngles = Vector3.zero;
                animatedParent.transform.localScale = Vector3.one;
                button.transform.parent = animatedParent.transform;
                animatedParent.AddComponent<Animator>();
                //button.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Assets/Resources/" + buttonPushAnimationName + ".controller") as RuntimeAnimatorController;
            }

        }
    }

    public void PushButton(int i)
    {
        if (model != null)
        {
            if (i >= 0 && i < numberOfButtons)
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

    void DeleteAllGlyphsIntimeline()
    {
        if (currentBlocksRoot != null)
        {
            currentBlocksRoot.DestroyAllBlocks();
        }
        glyphsSpawnedIntimeline = false;
    }

    void SpawnGlyphsInTimeline()
    {
        List<Texture> theGlyphs = GetTexturesFromExpectedCombination();
        if (currentBlocksRoot != null)
        {
            currentBlocksRoot.SpawnLevel(theGlyphs, showEachGlyphNTimes);
        }
        glyphsSpawnedIntimeline = true;
    }

    public void OnRestartLevel()
    {
        DeleteAllGlyphsIntimeline();
        ResetAnimationsAndButtonsStates();
        RandomizeCombination();
        RandomizeTexturesIndex();
        SpawnGlyphsInTimeline();
    }

    void Start () {

        SpawnAnimations();

        pushedButtons = new bool[16];

        RandomizeCombination();

        RandomizeTexturesIndex();

        //InitTextureMapping();
        //SetTextures();

    }
		
	void Update () {

        if (currentBlocksRoot == null)
        {
            currentBlocksRoot = FindObjectOfType<BlocksRoot>();
        }

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
                if (HasMadeTooManyMistakes())
                {
                    Debug.Log("YOU LOOSE");
                }
            }
        }

        if (restart)
        {
            restart = false;
            OnRestartLevel();
        }

    }
}
