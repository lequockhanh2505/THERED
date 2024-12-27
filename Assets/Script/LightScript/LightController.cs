using System;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private int maxReflections = 10;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float offsetDistance = 0.2f;
    [SerializeField] private float radiusCheck = 2f;
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] Animator animator;

    private LineRenderer lineRenderer;

    private bool lastButtonState = false;
    

    public static event Action<bool> buttonTurnOn;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is missing on this GameObject!");
        }
    }

    private void Update()
    {
        ChangeRotation();

        animator.SetBool("TurnOn", lastButtonState);

        if (lineRenderer == null)
            return;

        Vector2 currentPosition = transform.position;
        List<Vector3> points = new List<Vector3> { currentPosition };

        lineRenderer.SetPosition(0, transform.position);
        Vector2 direction = transform.up;
        int reflections = 0;
        bool isButtonActive = false;

        while (reflections < maxReflections)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, maxDistance, raycastLayerMask);

            if (hit.collider != null)
            {
                Vector2 adjustedPoint = hit.point - direction.normalized * Mathf.Min(offsetDistance, hit.distance);

                points.Add(adjustedPoint);

                if (hit.collider.CompareTag("Button"))
                {
                    isButtonActive = true;
                }

                if (hit.collider.CompareTag("Receiver"))
                {
                    Vector2 normal = hit.normal;
                    direction = Vector2.Reflect(direction, normal);
                    currentPosition = adjustedPoint;
                    reflections++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                points.Add(currentPosition + direction * maxDistance);
                break;
            }
        }

        if (lastButtonState != isButtonActive)
        {
            buttonTurnOn?.Invoke(isButtonActive);
            lastButtonState = isButtonActive;
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    private void ChangeRotation()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radiusCheck);

        foreach (var i in hits)
        {
            if (i.CompareTag("Player"))
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    float angle = rotationSpeed * Time.deltaTime;
                    transform.localRotation *= Quaternion.Euler(0, 0, angle);
                }
                else if (Input.GetKey(KeyCode.C))
                {
                    float angle = rotationSpeed * Time.deltaTime;
                    transform.localRotation *= Quaternion.Euler(0, 0, -angle);
                }
            }
        }
    }
}
