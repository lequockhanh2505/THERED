using UnityEngine;
using UnityEngine.UI;

public class BoarEnemyController : MonoBehaviour
{
    public StateMachine2 stateMachine;
    public AnimationController animationController;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip idleClip;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip hitClip;

    [Header("References")]
    public Transform player;
    public Rigidbody2D rb;

    [Header("Stats")]
    public int health = 30;
    public int maxHealth = 30;
    public float walkSpeed = 2f;
    public float chargeSpeed = 10f;
    public float detectionRange = 8f;
    public float attackRange = 2f;

    [Header("Hit State")]
    public float hitDuration = 0.5f;

    [Header("Environment Checks")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float groundCheckDistance = 2f;
    [SerializeField] private float wallCheckDistance = 2f;


    [Header("UI Elements")]
    [SerializeField] private GameObject healthBarPrefab;
    private Slider healthBar;

    private Vector3 originalScale;
    private GameObject healthBarInstance;

    // Sự kiện public để các đối tượng khác có thể lắng nghe
    public delegate void OnDamageTakenDelegate(int remainingHealth);
    public event OnDamageTakenDelegate OnDamageTaken;

    public BoarEnemyData data;

    void Start()
    {
        stateMachine = new StateMachine2();
        rb = GetComponent<Rigidbody2D>();
        if (animationController  == null)
        {
            animationController = GetComponent<AnimationController>();
        }

        originalScale = transform.localScale;
        originalScale.x = Mathf.Abs(originalScale.x);

        data = new BoarEnemyData
        {
            Player = player,
            ChargeSpeed = chargeSpeed,
            AttackRange = attackRange,
            DetectionRange = detectionRange
        };

        stateMachine.ChangeState(new BoarEnemyIdleState(this));

        InitializeHealthBar();
    }

    void Update()
    {
        stateMachine.Update();
        UpdateHealthBarPosition();
    }

    private void InitializeHealthBar()
    {
        if (healthBarPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
            healthBar = healthBarInstance.GetComponentInChildren<Slider>();
            healthBarInstance.transform.SetParent(GameObject.Find("HealthBar_Enemy").transform, false);
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }
    }

    private void UpdateHealthBarPosition()
    {
        if (healthBarInstance != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
            healthBarInstance.transform.position = screenPosition;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (audioManager != null)
        {
            audioManager.PlaySound(clip);
        }
    }

    public void Move(Vector2 velocity)
    {
        rb.velocity = velocity;

        if (velocity.x != 0)
        {
            float normalizedDirection = Mathf.Sign(velocity.x);
            if (Mathf.Sign(transform.localScale.x) != normalizedDirection)
                Flip(normalizedDirection);
        }
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    public void Flip(float direction)
    {
        transform.localScale = new Vector3(
            originalScale.x *  Mathf.Sign(direction),
            originalScale.y,
            originalScale.z
        );
    }

    public bool IsObstacleAhead()
    {
        return !IsGroundAhead() || IsWall();
    }

    private bool IsGroundAhead()
    {
        float direction = Mathf.Sign(transform.localScale.x);
        Vector2 origin = new Vector2(transform.position.x + direction * 0.5f, transform.position.y - 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    private bool IsWall()
    {
        float direction = Mathf.Sign(transform.localScale.x);
        Vector2 origin = new Vector2(transform.position.x + direction * 0.5f, transform.position.y - 0.3f);
        RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(direction, 0), wallCheckDistance, wallLayer);
        Debug.DrawRay(origin, new Vector2(direction, 0), Color.cyan, 3f);
        return hit.collider != null;
    }


    public bool IsPlayerInRange(float range)
    {
        return Vector2.Distance(transform.position, player.position) <= range;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (healthBar != null)
        {
            healthBar.value = health;
        }

        OnDamageTaken?.Invoke(health);

        stateMachine.ChangeState(new BoarEnemyHitState(this));

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance);
        }
        gameObject.SetActive(false);
    }
}
