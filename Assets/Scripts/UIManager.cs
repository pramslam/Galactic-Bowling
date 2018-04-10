using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public void DisableBoolAnimator(Animator animator)
    {
        animator.SetBool("IsDisplayed", false);
    }

    public void EnableBoolAnimator(Animator animator)
    {
        animator.SetBool("IsDisplayed", true);
    }

    public void ToggleDebug(Animator animator)
    {
        if (animator.GetBool("IsDisplayed") == true)
        {
            DisableBoolAnimator(animator);
        }
        else
        {
             EnableBoolAnimator(animator);
        }
    }
}
