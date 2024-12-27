using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private string _currentAnimation;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimation(string animationName, string parameter = "", float value = 0.0f)
    {
        if (_animator == null)
        {
            Debug.LogError("Animator is not assigned or missing.");
            return;
        }

        if (_currentAnimation != animationName && _currentAnimation != null)
        {
            if (parameter != "" && value != 0.0f)
            {
                _animator.Play(animationName);
                _animator.SetFloat(parameter, value);
            }
            else if (parameter == "" && value == 0.0f)
            {
                _animator.Play(animationName);
            }
        }

        if (parameter != "" && value != 0.0f)
        {
            _currentAnimation = animationName;
            _animator.Play(_currentAnimation);
            _animator.SetFloat(parameter, value);
        }
        else if (parameter == "" && value == 0.0f)
        {
            _currentAnimation = animationName;
            _animator.Play(_currentAnimation);
        }
    }

    public float getLengthAnimation(string animationName)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            return _animator.GetCurrentAnimatorStateInfo(0).length;
        }
        return 0.0f;
    }
}
