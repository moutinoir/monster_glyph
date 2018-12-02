using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [Header("References")]
    public InputManager inputManager;
    public TimelineController timelineController;
    public HitManager hitManager;
    public FallInHoleManager fallInHoleManager;
    public RunningTrackManager runningTrackManager;
    public AudioSource audioSource;
    public GlyphTableManager glyphTableManager;

    public bool invincible;

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
        FallInHole = 2,
        GameOverBottom = 3,
        GlyphTable = 4,
        Congrats = 5,
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
                new SGameState() {onEnter =  OnFallInHoleEnter, onUpdate = OnFallInHoleUpdate, onExit = OnFallInHoleExit},
                new SGameState() {onEnter =  OnGameOverBottomEnter, onUpdate = OnGameOverBottomUpdate, onExit = OnGameOverBottomExit},
        };
    }
    
    void OnPreparationEnter()
    {
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        fallInHoleManager.DisplayFloorHideHole();
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
        audioSource.Play();
        runningTrackManager.gameObject.SetActive(true);
        timelineController.StartTimelineAndActivate();
        hitManager.onHit += OnInTimelineObstacleHit;
        runningTrackManager.onReachEnd += OnInTimelineReachEnd;
        Debug.Log("[GameLoop] : Enter In Timeline");
    }

    void OnInTimelineObstacleHit()
    {
        if (invincible)
            return;

        runningTrackManager.gameObject.SetActive(false);
        nextState = EGameState.FallInHole;
        hitManager.onHit -= OnInTimelineObstacleHit;
        runningTrackManager.onReachEnd -= OnInTimelineReachEnd;
    }

    void OnInTimelineReachEnd()
    {
        nextState = EGameState.GlyphTable;

        runningTrackManager.onReachEnd -= OnInTimelineReachEnd;
        hitManager.onHit -= OnInTimelineObstacleHit;
    }

    void OnInTimelineUpdate()
    {

    }

    void OnInTimelineExit()
    {
        timelineController.StopTimelineAndDeactivate();
        Debug.Log("[GameLoop] : Exit In Timeline");
    }

    void OnFallInHoleEnter()
    {
        Debug.Log("[GameLoop] : Enter Fall In Hole");
        fallInHoleManager.MoveHoleDisableFloor();
        fallInHoleManager.onHitBottom += OnFallInHoleHitBottom;
    }

    void OnFallInHoleHitBottom()
    {
        nextState = EGameState.GameOverBottom;
        fallInHoleManager.onHitBottom -= OnFallInHoleHitBottom;
    }

    void OnFallInHoleUpdate()
    {
        fallInHoleManager.FallUpdate();
    }

    void OnFallInHoleExit()
    {
        Debug.Log("[GameLoop] : Exit Fall In Hole");
    }

    void OnGameOverBottomEnter()
    {
        Debug.Log("[GameLoop] : Enter Game Over Bottom");
        inputManager.onTrigger += OnGameOverBottomTrigger;
    }

    void OnGameOverBottomTrigger()
    {
        runningTrackManager.Reset();
        runningTrackManager.gameObject.SetActive(true);
        nextState = EGameState.Preparation;
        inputManager.onTrigger -= OnGameOverBottomTrigger;
    }

    void OnGameOverBottomUpdate()
    {

    }

    void OnGameOverBottomExit()
    {
        Debug.Log("[GameLoop] : Exit Game Over Bottom");
    }

    void OnGlyphTableEnter()
    {
        Debug.Log("[GameLoop] : Enter Glyph Table");
    }

    void OnGlyphTableUpdate()
    {
        if(glyphTableManager.HasRightCombination())
        {
            nextState = EGameState.Congrats;
            Debug.Log("Congrats");
        }

        if (glyphTableManager.HasMadeTooManyMistakes())
        {
            nextState = EGameState.GameOverBottom;
        }
    }

    void OnGlyphTableExit()
    {
        Debug.Log("[GameLoop] : Exit Glyph Table");
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
