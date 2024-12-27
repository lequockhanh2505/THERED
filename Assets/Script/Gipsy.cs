using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gipsy : MonoBehaviour
{
    Animator animator;
    public GameObject objF;
    private bool showF;
    public static event Action<bool> OnArea;
    private void Start()
    {
        animator = GetComponent<Animator>();
        objF.SetActive(false); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.Play("talk");
        set(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.Play("coin");
        set(false);
    }

    private void set(bool state)
    {
        objF.SetActive(state);
        OnArea?.Invoke(state);
    }

    public void SetAnimIdle()
    {
        animator.Play("idle");
    }
}
