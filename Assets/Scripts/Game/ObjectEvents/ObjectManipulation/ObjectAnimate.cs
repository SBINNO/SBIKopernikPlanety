using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimate : MonoBehaviour, ICutsceneAction
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animationTrigger;
    public void ExecuteAction()
    {
        animator.SetTrigger(animationTrigger);
    }
}
