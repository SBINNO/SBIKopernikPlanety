using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CutsceneEventLinker : CutsceneActionBase
{
    [SerializeField] private MonoBehaviour linkedAction;
    protected override void HandleCutsceneStart()
    {
        if (shouldWorkOnCutsceneStart)
        ExecuteLinkedAction();
    }
    protected override void HandleCutsceneEnd()
    {
        if (shouldWorkOnCutsceneEnd)
        ExecuteLinkedAction();
    }
    private void ExecuteLinkedAction()
    {
        if (linkedAction != null && linkedAction is ICutsceneAction cutsceneAction)
        {
            cutsceneAction.ExecuteAction();
        }
        CleanSubscriptions();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        CleanSubscriptions();
    }

}
