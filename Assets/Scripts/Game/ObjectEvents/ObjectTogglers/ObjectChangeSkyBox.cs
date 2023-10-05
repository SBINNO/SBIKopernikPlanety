using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChangeSkyBox : MonoBehaviour, ICutsceneAction
{
    [SerializeField] private Camera cameraToChange;
    public void ExecuteAction()
    {
        ChangeSkyBox();
    }
    

    private void ChangeSkyBox()
    {
        if (cameraToChange != null)
        {
            cameraToChange.clearFlags = CameraClearFlags.Skybox;
        }
    }
}
