using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSoundPlayer : ObjectActionHandler
{
    public AudioSource soundSource;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (soundSource != null)
        {
            soundSource.Play();
        }
    }
}
