using System;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    Animator animator;
    [SerializeField] private float radiusCheck = 2f;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private GameObject[] items;

    public event Action<GameObject[]> OnChestOpened;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckOpen();
    }

    private void CheckOpen()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isOpen && IsPlayerNearby())
        {
            animator.SetTrigger("Open");
            Open();
        }
    }

    private bool IsPlayerNearby()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radiusCheck);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public void Open()
    {
        if (!isOpen)
        {
            isOpen = true;
            OnChestOpened?.Invoke(items);
        }
    }
}
