using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController2 : MonoBehaviour
{
    public Animator animator;
    string currentAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetAnimation(string newAnimation)
    {
        if (newAnimation != null && newAnimation != currentAnimation)
        {
            currentAnimation = newAnimation;
            animator.Play(currentAnimation);
        }
    }

    public bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName);
    }

    public float getLengthAnimationIsRunning(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle"))
        {
            return stateInfo.length;
        }
        return 0.0f;
    }

    public float getAnimationLengthInController(string animationName)
    {
        if (animator == null || animator.runtimeAnimatorController == null)
        {
            //Debug.LogError("Animator or RuntimeAnimatorController is null.");
            return 0.0f;
        }

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            //Debug.Log("Found clip: " + clip.name);
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        //Debug.LogWarning("Animation clip not found: " + animationName);
        return 0.0f;
    }
}
