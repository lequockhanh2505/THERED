using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public Vector2 incidentRay = new Vector2(2f, 1f);
    public Vector2 reflectedRay;
    public Vector2 normal;
    [SerializeField] GameObject lightObj;
    LineRenderer lineRenderer;
    public List<GameObject> glass;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    void Update()
    {
        if (lightObj != null)
        {
            lineRenderer.SetPosition(0, lightObj.transform.position);
            lineRenderer.SetPosition(1, transform.position);
            lineRenderer.SetPosition(2, reflectedRay);

            incidentRay = lightObj.transform.position;
        }


        normal = transform.position + transform.up;
        reflectedRay = -(incidentRay - 2 * Vector2.Dot(normal, incidentRay) * normal);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, reflectedRay);

        // Check if the ray hits something and if the hit object is not the object performing the raycast
        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            if (hit.collider.CompareTag("Receiver"))
            {
                incidentRay = transform.position;
                reflectedRay = -(incidentRay - 2 * Vector2.Dot(normal, incidentRay) * normal);

                lineRenderer.SetPosition(0, incidentRay);
                lineRenderer.SetPosition(1, hit.point);
                lineRenderer.SetPosition(2, reflectedRay);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, normal); // Vẽ pháp tuyến

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + incidentRay.normalized); // Vẽ tia sáng

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + reflectedRay.normalized); // Vẽ tia phản xạ
    }
}
