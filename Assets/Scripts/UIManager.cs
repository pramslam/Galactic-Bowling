using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public void DisableBoolAnimator(Animator animator)
    {
        animator.SetBool("DebugIsDisplayed", false);
    }

    public void EnableBoolAnimator(Animator animator)
    {
        animator.SetBool("DebugIsDisplayed", true);
    }

    public void ToggleDebugMenu(Animator animator)
    {
        if (animator.GetBool("DebugIsDisplayed") == true)
        {
            DisableBoolAnimator(animator);
        }
        else
        {
             EnableBoolAnimator(animator);
        }
    }
}
