using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> UIToHideList = new List<GameObject>();
    [SerializeField] private GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isPaused)
            {
                Resume();
            } else if (!isPaused)
            {
                Pause();
            }
        }
    }
    private void HideUIElements()
    {
        foreach (var UI in UIToHideList)
        {
            UI.SetActive(false);
        }
    }
    private void RevealUIElements()
    {
        foreach (var UI in UIToHideList)
        {
            UI.SetActive(true);
        }
    }

    public void OpenPauseMenuAndroid()
    {
        if (isPaused)
        {
            Resume();
        }
        else if (!isPaused)
        {
            Pause();
        }
    }

    public void Resume()
    {
        RevealUIElements();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Pause()
    {
        HideUIElements();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    //public void Options()
    //{

    //}
    public void ExitMainMenu()
    {
        Time.timeScale = 1f;

        if (Application.isEditor || Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            SceneManager.LoadScene(SceneNames.MainMenuAndroid);
        } else
        {
            SceneManager.LoadScene(SceneNames.MainMenu);
        }
    }
}
