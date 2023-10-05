using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineCutscene : CutsceneController
{
    [SerializeField]
    private PlayableDirector playableDirector;
    [SerializeField]
    private TimelineAsset cutsceneTimeline;

    public override void StartCutscene(Vector3 currentCameraPosition, Quaternion currentCameraRotation)
    {
        base.StartCutscene(InitialCameraPosition, InitialCameraRotation);
        SwitchToCutsceneCamera(cutsceneCamera);
        PlayTimeline();
        StartCoroutine(WaitForTimeline(cutsceneCamera));
    }

    private System.Collections.IEnumerator WaitForTimeline(Camera cutsceneCamera)
    {
        while (playableDirector.state == PlayState.Playing)
        {
            yield return null;
        }
        EndCutscene();
        cutsceneCamera.enabled = false;
    }
    private void PlayTimeline()
    {
        playableDirector.playableAsset = cutsceneTimeline;
        playableDirector.Play();
    }

    public override void EndCutscene()
    {
        base.EndCutscene();
        SwitchToMainCamera();
    }
}
