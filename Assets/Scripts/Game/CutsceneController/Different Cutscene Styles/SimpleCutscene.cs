using System.Collections;
using UnityEngine;

public class SimpleCutscene : CutsceneController
{
    protected override void Start()
    {
        base.Start();
    }
    public override void StartCutscene(Vector3 currentCameraPosition, Quaternion currentCameraRotation)
    {
        base.StartCutscene(InitialCameraPosition, InitialCameraRotation);
        if (isPartOfSequence)
        UpdateCameraTransform();
        StartCoroutine(CameraTransitionCoroutine());
    }
    private IEnumerator CameraTransitionCoroutine()
    {
        SwitchToCutsceneCamera(cutsceneCamera);
        yield return new WaitForSeconds(cutsceneDuration);
        EndCutscene();
        if (!isPartOfSequence)
        {
            SwitchToMainCamera();
        }
        else
        {
            DisableCutsceneCamera();
        }
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
