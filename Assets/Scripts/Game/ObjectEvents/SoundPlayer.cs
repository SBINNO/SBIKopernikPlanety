using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour, ICutsceneAction
{
    [SerializeField]
    private AudioSource audioSource;

    private bool actionExecuted = false;

    public void ExecuteAction()
    {
        PlaySound();
        actionExecuted = true;
    }

    public void PlaySound()
    {
        if (audioSource != null)
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering Collider has a specific tag or meets any other criteria
        // before triggering the action
        if (other.CompareTag("Player")) // Change "Player" to the tag you want
        {
            if (!actionExecuted)
            ExecuteAction();
        }
    }
}
