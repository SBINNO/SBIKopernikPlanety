using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimationCutscene : CutsceneController
{
    protected override void Start()
    {
        base.Start();    
    }
    public override void StartCutscene(Vector3 currentCameraPosition, Quaternion currentCameraRotation)
    {
        base.StartCutscene(InitialCameraPosition, InitialCameraRotation);
        //if (isPartOfSequence)
        //UpdateCameraTransform();
        StartCoroutine(AnimationCoroutine());
    }
    private IEnumerator AnimationCoroutine()
    {
        Animation cameraAnimation = cutsceneCamera.GetComponent<Animation>();

        float animationLength = cameraAnimation.clip.length;
        float totalDuration = animationLength + cutsceneDuration;

        SwitchToCutsceneCamera(cutsceneCamera);

        if (cameraAnimation.clip != null && cameraAnimation != null)
        {
            PlayAnimation(cameraAnimation);
            yield return new WaitForSeconds(cutsceneDuration);
            if (isPartOfSequence)
            UpdateCameraTransform();
        }
        else
        {
            Debug.LogWarning("Cutscene animation or Animation component is null. Coroutine stopped.");
            yield break;
        }
        EndCutscene();
        if(!isPartOfSequence)
        {
            SwitchToMainCamera();
        } else
        {
            DisableCutsceneCamera();
        }

    }


    private void PlayAnimation(Animation cameraAnimation)
    {
        cameraAnimation.Play();       
    }
    private void UpdateCameraTransform()
    {
        Vector3 targetCameraPosition = cutsceneCamera.transform.position;
        Quaternion targetCameraRotation = cutsceneCamera.transform.rotation;
        cutsceneSequencer.SetCurrentCameraTransform(targetCameraPosition, targetCameraRotation);
        mainCamera.transform.position = targetCameraPosition;
        mainCamera.transform.rotation = targetCameraRotation;
    }
}
