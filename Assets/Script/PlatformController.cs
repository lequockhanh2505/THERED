using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] float distance = 10f; 
    [SerializeField] float speed = 0.1f;   

    private Vector2 startPoint;   
    private Vector2 targetPosition;

    [SerializeField] bool isMoving = false;
    private bool isAtTarget = false;

    private void OnEnable()
    {
        LightController.buttonTurnOn += Moving;
    }

    private void OnDisable()
    {
        LightController.buttonTurnOn -= Moving; 
    }

    private void Moving(bool state)
    {
        isMoving = state; 
    }

    void Start()
    {
        startPoint = transform.position;
        targetPosition = (Vector2)transform.position + Vector2.up * distance;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (!isAtTarget)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed);

                if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
                {
                    isAtTarget = true;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, startPoint, speed);
                if (Vector2.Distance(transform.position, startPoint) < 0.01f)
                {
                    isAtTarget = false;
                }
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPoint, speed);

            if (Vector2.Distance(transform.position, startPoint) < 0.01f)
            {
                isAtTarget = false;
            }
        }
    }
}
