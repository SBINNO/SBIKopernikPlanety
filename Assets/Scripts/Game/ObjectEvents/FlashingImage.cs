using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]

public class FlashingImage : MonoBehaviour
{
    Image image = null;
    private Coroutine currentFlashRoutine = null;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void StartFlash(float durationInSeconds, float maxAlpha, Color newColor)
    {
        image.color = newColor;

        // ensure maxAlpha isn't above 1
        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);

        if (currentFlashRoutine != null)
            StopCoroutine(currentFlashRoutine);
        currentFlashRoutine = StartCoroutine(Flash(durationInSeconds, maxAlpha));
    }

    IEnumerator Flash(float  durationInSeconds, float maxAlpha)
    {
        float flashInDuration = durationInSeconds / 2;
        for (float t = 0; t <= flashInDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = image.color;
            colorThisFrame.a = Mathf.Lerp(0, maxAlpha, t / flashInDuration);
            image.color = colorThisFrame;
            yield return null;
        }

        float flashOutDuration = durationInSeconds / 2;
        for (float t = 0; t <= flashOutDuration; t += Time.deltaTime)
        {
            Color colorThisFrame = image.color;
            colorThisFrame.a = Mathf.Lerp(maxAlpha, 0, t / flashOutDuration);
            image.color = colorThisFrame;
            yield return null;
        }

        image.color = new Color32(0, 0, 0, 0);
    }
}
