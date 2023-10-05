using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStopper : MonoBehaviour, ICutsceneAction
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float fadeOutTime = 3.0f;

    public void ExecuteAction()
    {
        if (audioSource != null)
            StartCoroutine(FadeOutAndStop());
    }
    private IEnumerator FadeOutAndStop()
    {
        float startVolume = audioSource.volume;
        float timer = 0.0f;

        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0.0f, timer / fadeOutTime);
            yield return null;
        }

        audioSource.Stop();
    }
}
