using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private Vector3 pointStart; // Điểm đầu
    private Vector3 pointEnd; // Điểm cuối

    LineRenderer lineRenderer;

    Arrow Arrow;

    float maxDistance;

    [SerializeField]
    [Range(0, 100)]
    int numberOfPoints;

    public GameObject perfabs;

    

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Arrow = GetComponentInParent<Arrow>();

        pointStart = Arrow.currentPointArrow;
        pointEnd = Arrow.finalPosition;

        //currentPoint = Arrow.currentPointArrow;
        //prePoint = currentPoint;
    }

    // Update is called once per frame
    void Update()


    //if (prePoint != currentPoint)
    //{
    //    SpringJoint2D joint = currentPoint.AddCompoment<SpringJoint2D>();
    //    joint.connectedBody = prePoint.GetCompoment(Rigidbody2D);
    //    joint.autoConfigureDistance = false;
    //    joint.distance = 0.2f;
    //    joint.dampingRatio = 0.5f;
    //    joint.frequency = 1f;
    //}
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = (float)i / (numberOfPoints - 1); // Tỷ lệ nội suy giữa 0 và 1
            Vector3 position = Vector3.Lerp(pointStart, pointEnd, t); // Nội suy tuyến tính
            //Instantiate(perfabs, position, Quaternion.identity);
            //Debug.Log("Point: " + i + ": " + position);
        }

        //currentPoint = Arrow.currentPointArrow;
        //Debug.Log(Arrow.stopDraw);
        //if (Vector2.Distance(currentPoint, Arrow.initialPosition) > 0.1f && !Arrow.stopDraw)
        //{
        //    lineRenderer.positionCount++;
        //    lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPoint);
        //}
    }
}
