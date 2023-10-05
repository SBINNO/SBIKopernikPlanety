using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSequencer : MonoBehaviour
{
    [SerializeField]
    private List<CutsceneController> cutsceneControllers = new List<CutsceneController>();
    private int currentIndex = 0;
    private GameObject playerCharacter;
    private Camera initialPlayerCamera;

    private Vector3 currentCameraPosition;
    private Quaternion currentCameraRotation;

    public Vector3 GetCurrentCameraPosition()
    {
        return currentCameraPosition;
    }
    public Quaternion GetCurrentCameraRotation()
    {
        return currentCameraRotation;
    }

    private void Awake()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        InitializeCameraTransforms();
        
    }
    public void StartNextCutscene()
    {
        if (currentIndex >= cutsceneControllers.Count)
        {
            EndSequence();
            return;
        }
        CutsceneController currentCutscene = cutsceneControllers[currentIndex];
        currentCutscene.StartCutscene(currentCameraPosition, currentCameraRotation);
        currentIndex++;

    }
    private void EndSequence()
    {
        Debug.Log("Sequencer: End of cutscene sequence.");
        if (cutsceneControllers.Count > 0)
        {
            CutsceneController lastCutscene = cutsceneControllers[cutsceneControllers.Count - 1];
            lastCutscene.ResetMainCameraTransforms();
            lastCutscene.SwitchToMainCamera();
            lastCutscene.EnablePlayerControls();
        }
        ResetSequence();
    }
    private void ResetSequence()
    {
        currentIndex = 0;
    }
    public void SetCurrentCameraTransform(Vector3 position, Quaternion rotation)
    {
        currentCameraPosition = position;
        currentCameraRotation = rotation;
    }
    private void InitializeCameraTransforms()
    {
        initialPlayerCamera = playerCharacter.GetComponentInChildren<Camera>();
        currentCameraPosition = initialPlayerCamera.transform.position;
        currentCameraRotation = initialPlayerCamera.transform.rotation;
    }

}
