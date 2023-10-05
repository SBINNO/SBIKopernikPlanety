using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSimpleCutsceneController : ObjectActionHandler
{
    [SerializeField]
    private CutsceneController cutsceneController;
    public override void OnPointerClick(PointerEventData eventData)
    {

        if (cutsceneController != null)
        {
            Debug.Log("Normal Cutscene goes");
            cutsceneController.Initialize();
            return;
        }
    }
}
