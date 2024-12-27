using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Incendiary : MonoBehaviour
{
    [SerializeField]
    Transform startPos;

    [SerializeField]
    Transform nighBorne;

    [SerializeField]
    float scaleFactor = 1f;

    [SerializeField]
    int dmg = 1;

    bool activeSkill = false;
    Vector3 initialScale;

    public static event Action<int> OnPlayerHitDame;

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    private void OnEnable()
    {
        activeSkill = false;
        transform.localScale = initialScale * scaleFactor;
    }

    private void Update()
    {
        if (!activeSkill)
        {
            transform.position = startPos.position;
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * nighBorne.localScale.x, transform.localScale.y);
            GetComponent<Animator>().Play("Incendiary");
            activeSkill = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                OnPlayerHitDame?.Invoke(dmg);
            }
        }
    }


    public void SetActiveOff()
    {
        gameObject.SetActive(false);
    }
}