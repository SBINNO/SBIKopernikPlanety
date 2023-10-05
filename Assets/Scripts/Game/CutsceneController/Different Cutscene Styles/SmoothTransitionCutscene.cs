using UnityEngine;
using System.Collections;

public class SmoothTransitionCutscene : CutsceneController
{
    [SerializeField]
    private float transitionDuration = 1f;

    protected override void Start()
    {
        base.Start();
    }

    public override void StartCutscene(Vector3 currentCameraPosition, Quaternion currentCameraRotation)
    {
        base.StartCutscene(InitialCameraPosition, InitialCameraRotation);
        mainCamera.enabled = true;
        StartCoroutine(SmoothTransition());
    }

    private IEnumerator SmoothTransition()
    {
        Vector3 targetCameraPosition = cutsceneCamera.transform.position;
        Quaternion targetCameraRotation = cutsceneCamera.transform.rotation;

        float stepSize = 1f / transitionDuration;
        float t = 0f;

        // Perform the smooth transition
        while (t < 1f)
        {
            // Interpolate the camera position and rotation
            UpdateCameraTransform(targetCameraPosition, targetCameraRotation, t);
            t += stepSize * Time.deltaTime;
            yield return null;
        }

        // Wait for the specified duration
        yield return new WaitForSeconds(cutsceneDuration);
        if (isPartOfSequence)
        cutsceneSequencer.SetCurrentCameraTransform(targetCameraPosition, targetCameraRotation);
        EndCutscene();
        if (!isPartOfSequence)
        SwitchToMainCamera();
    }
    private void UpdateCameraTransform(Vector3 targetCameraPosition, Quaternion targetCameraRotation, float t)
    {
        mainCamera.transform.position = Vector3.Lerp(InitialCameraPosition, targetCameraPosition, t);
        mainCamera.transform.rotation = Quaternion.Slerp(InitialCameraRotation, targetCameraRotation, t);
    }
}