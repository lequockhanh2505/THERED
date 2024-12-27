using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    private float _arrowSpeed = 10.0f;
    float launchAngle;
    readonly private float gravity = -9.81f;


    // Vị trí ban đầu
    static public Vector3 initialPosition;

    public float initialVelocityX = 0f;  // Vận tốc ban đầu theo phương X
    public float initialVelocityY = 0f;  // Vận tốc ban đầu theo phương Y

    public bool isGround = false;

    public Vector3 finalPosition;

    public Vector2 currentPointArrow;

    public bool stopDraw = false;

    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();

        launchAngle = Shoot.angleToTarget;
        float initialVelocityX = _arrowSpeed * Mathf.Cos(launchAngle * Mathf.Deg2Rad);
        float initialVelocityY = _arrowSpeed * Mathf.Sin(launchAngle * Mathf.Deg2Rad);

        rb.velocity = new Vector2(initialVelocityX, initialVelocityY);

        float totalFlightTime = (2 * initialVelocityY) / -gravity;

        float distanceX = initialVelocityX * totalFlightTime;
        float finalYPosition = initialPosition.y + initialVelocityY * totalFlightTime + 0.5f * gravity * Mathf.Pow(totalFlightTime, 2);
        finalPosition = new Vector3(initialPosition.x + distanceX, finalYPosition, initialPosition.z);
    }
    // Update is called once per frame
    void Update()
    {
        //rb.velocity.x* rb.velocity.x > 0.01f && rb.velocity.y * rb.velocity.y > 0.01f // tích vô hướng = độ lớn của vector
        if (rb.velocity.sqrMagnitude > 0.01f)
        {
            currentPointArrow = transform.position;
            // Tính toán góc của mũi tên dựa trên vận tốc
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

            // Xoay mũi tên theo góc vừa tính được
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        // Hủy mũi tên khi đã chạm đất
        if (isGround)
        {
            StartCoroutine(WaitAndDesotroyArrow(1f));
        }
    }

    private IEnumerator WaitAndDesotroyArrow(float waitTime)
    {
        while (isGround)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator WaitAndStopArrow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        rb.bodyType = RigidbodyType2D.Static;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            StartCoroutine(WaitAndStopArrow(0.04f));
        }
        else
        {
            isGround = false;
        }

        if (collision.gameObject.CompareTag("Wall-Top"))
        {
            stopDraw = true;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
