using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [Header("References")]
    public InputManager inputManager;
    public TimelineController timelineController;

    delegate void StateAction();

    struct SGameState
    {
        public StateAction onEnter;
        public StateAction onUpdate;
        public StateAction onExit;
    }

    public enum EGameState
    {
        None = 100,
        Preparation = 0,
        InTimeline = 1,
    }

    SGameState[] states;
    public EGameState currentState = EGameState.None;
    public EGameState nextState = EGameState.Preparation;

    private void Start()
    {
        states = new SGameState[]
            {
                new SGameState() {onEnter =  OnPreparationEnter, onUpdate = OnPreparationUpdate, onExit = OnPreparationExit},
                new SGameState() {onEnter =  OnInTimelineEnter, onUpdate = OnInTimelineUpdate, onExit = OnInTimelineExit},
        };
    }
    
    void OnPreparationEnter()
    {
        inputManager.onTrigger += OnPreparationTrigger;
        Debug.Log("[GameLoop] : Enter Preparation");
    }

    void OnPreparationTrigger()
    {
        nextState = EGameState.InTimeline;
        inputManager.onTrigger -= OnPreparationTrigger;
    }

    void OnPreparationUpdate()
    {

    }

    void OnPreparationExit()
    {
        Debug.Log("[GameLoop] : Exit Preparation");
    }

    void OnInTimelineEnter()
    {
        timelineController.StartTimelineAndActivate();
        inputManager.onTrigger += OnInTimelineTrigger;
        Debug.Log("[GameLoop] : Enter In Timeline");
    }

    void OnInTimelineTrigger()
    {
        nextState = EGameState.Preparation;
        inputManager.onTrigger -= OnInTimelineTrigger;
    }

    void OnInTimelineUpdate()
    {

    }

    void OnInTimelineExit()
    {
        timelineController.StopTimelineAndDeactivate();
        Debug.Log("[GameLoop] : Exit In Timeline");
    }

    private void Update()
    {
        if(nextState != currentState)
        {
            if((int)currentState < states.Length && (int)currentState > -1 && states[(int)currentState].onExit != null)
                states[(int)currentState].onExit();
            currentState = nextState;
            if((int)currentState < states.Length && (int)currentState > -1 && states[(int)currentState].onEnter != null)
                states[(int)currentState].onEnter();
        }

        if ((int)currentState < states.Length && (int)currentState > -1 && states[(int)currentState].onUpdate != null)
            states[(int)currentState].onUpdate();
    }
}
