using Unity.VisualScripting;
using UnityEngine;

public class CutsceneActionBase : MonoBehaviour
{
    [SerializeField] private CutsceneController cutsceneController;
    [SerializeField] protected bool shouldWorkOnCutsceneStart;
    [SerializeField] protected bool shouldWorkOnCutsceneEnd;

    protected virtual void Start()
    {
        if (cutsceneController != null)
        {
            cutsceneController.OnCutsceneStart += HandleCutsceneStart;
            cutsceneController.OnCutsceneEnd += HandleCutsceneEnd;
        }
        else
        {
            Debug.LogWarning("Cutscenecontroller is null!");
        }

    }
    protected virtual void OnDestroy()
    {
        cutsceneController.OnCutsceneStart -= HandleCutsceneStart;
        cutsceneController.OnCutsceneEnd -= HandleCutsceneEnd;
    }
    protected virtual void HandleCutsceneStart()
    {
        // Implementation specific to the child class
    }

    protected virtual void HandleCutsceneEnd()
    {
        // Implementation specific to the child class
    }
    protected void HandleEventStarting()
    {
        cutsceneController.OnCutsceneStart -= HandleCutsceneStart;
    }
    protected void HandleEventEnding()
    {
        cutsceneController.OnCutsceneEnd -= HandleCutsceneEnd;
    }
    protected void CleanSubscriptions()
    {
        cutsceneController.OnCutsceneStart -= HandleCutsceneStart;
        cutsceneController.OnCutsceneEnd -= HandleCutsceneEnd;
    }
}
