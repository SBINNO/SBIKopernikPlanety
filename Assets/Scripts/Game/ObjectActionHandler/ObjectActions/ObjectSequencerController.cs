using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSequencerController : ObjectActionHandler
{
    [SerializeField]
    private CutsceneSequencer cutsceneSequencer;
    public override void OnPointerClick(PointerEventData eventData)
    {

        if (cutsceneSequencer != null)
        {
            Debug.Log("Normal Cutscene goes");
            cutsceneSequencer.StartNextCutscene();
            return;
        }
    }
}
