using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectAnimator : ObjectActionHandler
{
    public Animator animator;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetTrigger("Rotation");
        }
    }
}
