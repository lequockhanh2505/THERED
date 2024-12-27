using System;
using System.Collections;
using UnityEngine;

public class Napalm : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
    Transform startPos;
    [SerializeField]
    Transform endPos;

    [SerializeField]
    float scaleFactor = 1.2f; // Mức độ tăng kích thước

    readonly int angleDeg = 45;

    Vector2 directionToPlayer;

    float initialVelocityX;
    float initialVelocityY;

    private bool hasThrown = false;

    public static event Action<int> OnPlayerHitDame;
    public int dmg = 1;
    Vector3 initialScale; // Kích thước ban đầu
    Vector3 startScale;

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    void OnEnable()
    {
        hasThrown = false;
        transform.localScale = initialScale * scaleFactor;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        PhysicsMaterial2D physicsMaterial2D = new PhysicsMaterial2D();
        physicsMaterial2D.friction = 2f;
        rb.sharedMaterial = physicsMaterial2D;
    }

    private void Update()
    {
        if (!hasThrown)
        {
            Throw();
            hasThrown = true;
        }
    }

    public void Throw()
    {
        transform.position = startPos.position;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.Play("SkillNapalm");
        directionToPlayer = (endPos.position - startPos.position).normalized;
        float totalDistance = Vector2.Distance(startPos.position, endPos.position);
        float verticalDistance = Vector2.Distance(new Vector2(startPos.position.x, endPos.position.y), startPos.position);
        float v0 = Mathf.Sqrt((Mathf.Abs(totalDistance - verticalDistance) * 9.81f) / (Mathf.Sin(2 * angleDeg * Mathf.Deg2Rad) * Mathf.Sin(2 * angleDeg * Mathf.Deg2Rad)));
        initialVelocityX = v0 * Mathf.Cos(angleDeg * Mathf.Deg2Rad);
        initialVelocityY = v0 * Mathf.Sin(angleDeg * Mathf.Deg2Rad);

        rb.velocity = new Vector2(initialVelocityX * directionToPlayer.x, initialVelocityY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            GetComponent<BoxCollider2D>().isTrigger = true;
            StartCoroutine(WaitToNextState(0.5f));
        }

        if (collision.CompareTag("Player"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            OnPlayerHitDame?.Invoke(dmg);
        }
    }


    private IEnumerator WaitToNextState(float waitTime)
    {
        animator.Play("SkillNapalm3");
        yield return new WaitForSeconds(waitTime);
        animator.Play("SkillNapalm4");
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}