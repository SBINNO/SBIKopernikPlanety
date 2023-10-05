using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimatorToggler : MonoBehaviour, ICutsceneAction
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool shouldEnableAnimator;
    [SerializeField] private bool shouldDisableAnimator;
    public void ExecuteAction()
    {
        if (animator != null && shouldEnableAnimator) 
        {
            animator.enabled = true;
        }
        if (animator != null && shouldDisableAnimator)
        {
            animator.enabled = false;
        }
    }
}
