using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject pointA;
    [SerializeField]
    GameObject pointB;

    [SerializeField]
    [Range(0, 100)]
    int numberOfPoints;

    [SerializeField]
    float maxDistance;

    int preNumberOfPoints;

    [SerializeField]
    GameObject prefabs;

    private bool pointsCreated = false;

    [SerializeField]
    LineRenderer lineRenderer;

    [SerializeField]
    List<Vector2> points = new List<Vector2>();

    [SerializeField]
    List<GameObject> spawnedPrefabs = new List<GameObject>();

    [SerializeField]
    List<Vector2> positionPrefabs = new List<Vector2>();

    [SerializeField]
    Player player;

    private void Start()
    {
        maxDistance = Vector2.Distance(pointA.transform.position, pointB.transform.position);
    }

    private void Update()
    {
        calculatorInterpolate();
        drawRope();
    }

    private void drawRope()
    {
        lineRenderer.positionCount = spawnedPrefabs.Count;
        for (int i = 0; i < spawnedPrefabs.Count; i++)
        {
            lineRenderer.SetPosition(i, spawnedPrefabs[i].transform.position);
        }
    }

    private void calculatorInterpolate()
    {
        points.Clear();
        for (int i = 0; i <= numberOfPoints; i++)
        {
            float t = (float)i / numberOfPoints;
            Vector2 interpolatedPoint = Vector2.Lerp(pointA.transform.position, pointB.transform.position, t);
            points.Add(interpolatedPoint);
        }

        if (spawnedPrefabs.Count < points.Count)
        {
            foreach (var point in points)
            {
                GameObject instance = Instantiate(prefabs, point, Quaternion.identity);
                spawnedPrefabs.Add(instance);
            }

            //Kết nối SpringJoint2D giữa các đối tượng đã sinh ra
            for (int i = 1; i < spawnedPrefabs.Count; i++) // Bắt đầu từ 1 vì điểm đầu không kết nối với đối tượng trước
            {
                SpringJoint2D joint = spawnedPrefabs[i].AddComponent<SpringJoint2D>();
                if (i == 1)
                {
                    // Liên kết điểm thứ 2 với điểm đầu tiên (pointA)
                    joint.connectedBody = pointA.GetComponent<Rigidbody2D>();
                }
                else
                {
                    // Liên kết các điểm tiếp theo với điểm trước nó
                    joint.connectedBody = spawnedPrefabs[i - 1].GetComponent<Rigidbody2D>();
                    //spawnedPrefabs[i - 1].GetComponent<Rigidbody2D>().gravityScale = 0f;
                }

                joint.autoConfigureDistance = false;
                joint.distance = (maxDistance) / (numberOfPoints - 1); // Điều chỉnh khoảng cách giữa các đối tượng
                joint.dampingRatio = 1f; // Giảm chấn
                joint.frequency = 3f; // Tần số dao động của lò xo
                joint.enableCollision = true;
            }
        }

        if (points.Count == numberOfPoints)
        {
            for (int i = 0; i <= numberOfPoints; i++)
            {
                float t = (float)i / numberOfPoints;
                Vector2 interpolatedPoint = Vector2.Lerp(pointA.transform.position, pointB.transform.position, t);
                points[i] = interpolatedPoint;
            }
        }

        //if (Vector2.Distance(points[0], points[points.Count -1]) > numberOfPoints * 0.8f){
        //    // Thực hiện hành động giới hạn
        //    foreach (var prefab in spawnedPrefabs)
        //    {
        //        Vector2 direction = (prefab.GetComponent<SpringJoint2D>().connectedBody.position - (Vector2)transform.position).normalized;
        //        transform.position = (Vector2)prefab.GetComponent<SpringJoint2D>().connectedBody.position - direction * numberOfPoints * 0.8f;
        //    }
        //}
        //// Điều chỉnh độ cứng và giảm dao động của dây
        //springJoint.frequency = 3.0f;
        //springJoint.dampingRatio = 0.5f;
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    preNumberOfPoints = numberOfPoints;
    //    GeneratePointsAndPrefabs(); // Sinh ra các điểm ban đầu
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    if (preNumberOfPoints != numberOfPoints) // Kiểm tra nếu số lượng điểm đã thay đổi
    //    {
    //        preNumberOfPoints = numberOfPoints;
    //        UpdatePointsAndPrefabs();
    //        //Debug.Log(spawnedPrefabs.Count);
    //        //drawRope();
    //    }
    //}

    //void GeneratePointsAndPrefabs()
    //{
    //    // Làm sạch danh sách điểm và các đối tượng đã sinh ra
    //    ClearPointsAndPrefabs();

    //    // Tạo các điểm dựa trên số lượng `numberOfPoints`
    //    for (int i = 0; i <= numberOfPoints; i++)
    //    {
    //        float t = (float)i / numberOfPoints;
    //        Vector2 interpolatedPoint = Vector2.Lerp(pointA.transform.position, pointB.transform.position, t);
    //        points.Add(interpolatedPoint);
    //    }

    //    // Sinh các đối tượng prefabs tại các điểm
    //    foreach (var point in points)
    //    {
    //        GameObject instance = Instantiate(prefabs, point, Quaternion.identity);
    //        spawnedPrefabs.Add(instance); // Lưu tham chiếu tới đối tượng đã sinh
    //    }

    //    // Kết nối SpringJoint2D giữa các đối tượng đã sinh ra
    //    for (int i = 1; i < spawnedPrefabs.Count; i++) // Bắt đầu từ 1 vì điểm đầu không kết nối với đối tượng trước
    //    {
    //        SpringJoint2D joint = spawnedPrefabs[i].AddComponent<SpringJoint2D>();

    //        if (i == 1)
    //        {
    //            // Liên kết điểm thứ 2 với điểm đầu tiên (pointA)
    //            joint.connectedBody = pointA.GetComponent<Rigidbody2D>();
    //        }
    //        else
    //        {
    //            // Liên kết các điểm tiếp theo với điểm trước nó
    //            joint.connectedBody = spawnedPrefabs[i - 1].GetComponent<Rigidbody2D>();
    //        }

    //        joint.autoConfigureDistance = false;
    //        joint.distance = 0.2f; // Điều chỉnh khoảng cách giữa các đối tượng
    //        joint.dampingRatio = 0.5f; // Giảm chấn
    //        joint.frequency = 1f; // Tần số dao động của lò xo
    //    }

    //    // Kết nối điểm cuối (pointB) với đối tượng cuối cùng trong danh sách
    //    SpringJoint2D lastJoint = pointB.AddComponent<SpringJoint2D>();
    //    lastJoint.connectedBody = spawnedPrefabs[spawnedPrefabs.Count - 1].GetComponent<Rigidbody2D>();
    //    lastJoint.autoConfigureDistance = false;
    //    lastJoint.distance = 0.00001f;
    //    lastJoint.dampingRatio = 0.5f;
    //    lastJoint.frequency = 0.5f;

    //    pointsCreated = true; // Đánh dấu đã sinh ra các điểm
    //}


    //void UpdatePointsAndPrefabs()
    //{
    //    UpdatePoint();

    //    if (!pointsCreated) return;

    //    // Cập nhật vị trí các đối tượng đã sinh ra dựa trên số lượng điểm mới
    //    if (spawnedPrefabs.Count == numberOfPoints + 1)
    //    {
    //        points.Clear();
    //        for (int i = 0; i <= numberOfPoints; i++)
    //        {
    //            float t = (float)i / numberOfPoints;
    //            Vector2 interpolatedPoint = Vector2.Lerp(pointA.transform.position, pointB.transform.position, t);
    //            points.Add(interpolatedPoint);
    //        }

    //        // Di chuyển các đối tượng tới vị trí mới
    //        for (int i = 0; i <= numberOfPoints; i++)
    //        {
    //            spawnedPrefabs[i].transform.position = points[i];
    //        }
    //    }
    //    else
    //    {
    //        // Nếu số lượng điểm thay đổi, sinh lại toàn bộ
    //        GeneratePointsAndPrefabs();
    //    }
    //}

    //void ClearPointsAndPrefabs()
    //{
    //    // Xóa tất cả các đối tượng đã sinh ra
    //    foreach (var obj in spawnedPrefabs)
    //    {
    //        Destroy(obj);
    //    }

    //    spawnedPrefabs.Clear();
    //    points.Clear(); // Làm sạch danh sách điểm
    //}

    //private void drawRope()
    //{
    //    lineRenderer.positionCount = spawnedPrefabs.Count;

    //    for (int i = 0; i < spawnedPrefabs.Count; i++)
    //    {
    //        lineRenderer.SetPosition(i, spawnedPrefabs[i].transform.position);
    //    }
    //}

    //private void UpdatePoint()
    //{
    //    for (int i = 0; i < spawnedPrefabs.Count; i++)
    //    {
    //        positionPrefabs[i] = spawnedPrefabs[i].transform.position;
    //    }
    //}
}
