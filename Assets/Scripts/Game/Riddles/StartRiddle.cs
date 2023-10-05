using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRiddle : MonoBehaviour, ICutsceneAction
{
    [SerializeField] SolarRiddle solarRiddle;
    [SerializeField] CutsceneSequencer winSequencer;
    private bool hasInitializedWinCutscene = false;
    private bool isUsingSequencer = false;
    private void Awake()
    {
        if (winSequencer != null)
        {
            isUsingSequencer = true;
        }
    }
    private void Start()
    {
        if (solarRiddle != null)
        {
            solarRiddle.OnRiddleSolved += HandleRiddleSolved;
        }
    }
    private void OnDestroy()
    {
        if (solarRiddle != null)
        {
            solarRiddle.OnRiddleSolved -= HandleRiddleSolved;
        }
    }
    private void HandleRiddleSolved()
    {
        if (isUsingSequencer && !hasInitializedWinCutscene)
        {
            winSequencer.StartNextCutscene();
            hasInitializedWinCutscene = true;

            Destroy(this);
        }
    }
    public void ExecuteAction()
    {
        if (solarRiddle != null)
        {
            solarRiddle.StartRiddle();
        }
    }
}
