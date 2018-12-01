using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector director;
    float timeScale = 1;

    public void StartTimelineAndActivate()
    {
        director.gameObject.SetActive(true);
        director.Play();
        director.time = 0;
    }

    public void SlowTimeScale()
    {
        timeScale = 0.2f;
    }

    public void NormalTimeScale()
    {
        timeScale = 1;
    }

    public void Update()
    {
        director.time += Time.deltaTime * timeScale;
        director.Evaluate();
    }

    public void StopTimelineAndDeactivate()
    {
        director.Stop();
        director.gameObject.SetActive(false);
    }
}
