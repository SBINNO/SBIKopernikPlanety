using UnityEngine;
using UnityEngine.UI;


public class PlayerCursor : MonoBehaviour
{
    public Image crosshairImage;

    bool isCursorLocked = false;
    void Start()
    {
#if UNITY_EDITOR
        ToggleCursorLock();
#endif
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorLock();
        }
    }
    private void ToggleCursorLock()
    {
        isCursorLocked = !isCursorLocked;

        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
