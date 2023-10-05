using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVisibility : MonoBehaviour, ICutsceneAction
{
    [SerializeField] private GameObject objectToToggle;
    [SerializeField] bool shouldHideObject;
    [SerializeField] bool shouldShowObject;
    public void ChangeObjectVisibility()
    {
        if (shouldHideObject)
        {
            HideObject();
        } else if (shouldShowObject)
        {
            ShowObject();
        }
    }

    public void ExecuteAction()
    {
        ChangeObjectVisibility();
    }

    private void HideObject()
    {
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(false);
        }
    }

    private void ShowObject()
    {
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(true);
        }
    }
}
