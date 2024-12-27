using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine;
    private SpriteRenderer _spriteRenderer;
    private MovementController _movementController;
    private AnimationController _animationController;
    private AttackController _attackController;
    public PlayerStats _playerStats;
    public UIManager _uIManager;

    [SerializeField]
    GameObject _bow;

    public bool isGround;
    public bool isRope = false;
    public bool isHitDmg = false;
    public bool canDash = false;
    private bool isHurtDot;
    public bool isHurt = false;
    private float _dotStartTime;

    public bool isAttackingWithArrow;
    public bool isAttackingNormal;
    public int combo = 1;

    private float lastHitTime;
    [SerializeField]
    private float hitCooldown = 2f;

    private void Start()
    {
        // Khởi tạo StateMachine
        stateMachine = new StateMachine();

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _movementController = GetComponent<MovementController>();
        _animationController = GetComponent<AnimationController>();
        _attackController = GetComponent<AttackController>();
        _playerStats = GetComponent<PlayerStats>();
        _uIManager = FindObjectOfType<UIManager>(); // Tìm UI Manager trong scene
        stateMachine.ChangeState(new IdleState(this));
    }

    void OnEnable()
    {
        //NightBorneController.OnPlayerHitDame += TakeDMG;
        //Incendiary.OnPlayerHitDame += TakeDMG;
        Trap.OnPlayerHitDame += TakeDMG;
        BoarEnemyAttackState.OnPlayerHitDame += TakeDMG;
        //Fire.OnPlayerHitDame += TakeDMG;
        //Napalm.OnPlayerHitDame += TakeDmgDot;
    }

    void OnDisable()
    {
        //NightBorneController.OnPlayerHitDame -= TakeDMG;
        //Incendiary.OnPlayerHitDame -= TakeDMG;
        Trap.OnPlayerHitDame -= TakeDMG;
        BoarEnemyAttackState.OnPlayerHitDame -= TakeDMG;
        //Fire.OnPlayerHitDame -= TakeDMG;
        //Napalm.OnPlayerHitDame -= TakeDmgDot;
    }

    public void TakeDMG(int damage)
    {
        if (Time.time - lastHitTime >= hitCooldown)
        {
            _playerStats.Hp -= damage;
            stateMachine.ChangeState(new DeathState(this, _uIManager));
            isHurt = true;
            Vector2 knockbackDirection = (transform.position.x > 0) ? Vector2.left : Vector2.right;
            GetComponent<Rigidbody2D>().AddForce(knockbackDirection * 5f, ForceMode2D.Impulse);
            lastHitTime = Time.time;
        }
    }
    void TakeDmgDot(int dmgDot)
    {
        if (Time.time - lastHitTime >= hitCooldown)
        {
            isHurtDot = true;
            isHurt = true;
            _dotStartTime = Time.time;
            StartCoroutine(DoT(dmgDot, 1));
            lastHitTime = Time.time;
        }
    }

    private IEnumerator DoT(int damage, float time)
    {
        while (isHurtDot)
        {
            if (Time.time - _dotStartTime >= time)
            {
                _playerStats.Hp -= damage;
                stateMachine.ChangeState(new DeathState(this, _uIManager));
                _dotStartTime = Time.time;
                break;
            }
            yield return null;
        }
    }

    public void HurtNoActive()
    {
        isHurt = false;
        isHurtDot = false;
    }

    void Update()
    {
        if (stateMachine != null)
        {
            stateMachine.Update();
        }
        checkGround();
        canDash = _movementController.checkDash(canDash);
    }

    private void FixedUpdate()
    {
        if (stateMachine != null)
        {
            stateMachine.FixedUpdate();
        }
    }


    public void Move(float direction)
    {
        _movementController.Move(direction);
    }

    public void Jump()
    {
        _movementController.Jump();
    }

    public void Dash()
    {
        _movementController.Dash();
    }

    public void CheckDash()
    {
        _movementController.checkDash(canDash);
    }

    public float GetHorizontalVelocity()
    {
        return _movementController.GetHorizontalVelocity();
    }

    public float GetVerticalVelocity()
    {
        return _movementController.GetVerticalVelocity();
    }

    public void PlayAnimation(string animationName, string parameter = "", float value = 0.0f)
    {
        _animationController.SetAnimation(animationName, parameter, value);
    }

    public float GetAnimationLength(string animationName)
    {
        return _animationController.getLengthAnimation(animationName);
    }

    public void AttakingWithArrowStart()
    {
        isAttackingWithArrow = true;
    }

    public void AttakingWithArrowEnd()
    {
        isAttackingWithArrow = false;
    }

    public void ContinueAttack()
    {
        combo++;
    }

    public void StartAttackEventAnimation()
    {
        isAttackingNormal = true;
    }

    public void EndAttackEventAnimation()
    {
        isAttackingNormal = false;
        combo = 1;
    }

    public bool GetIsAttackingWithArrow()
    {
        return _attackController.GetIsAttackingWithArrow(isAttackingWithArrow);
    }

    public bool GetIsAttackingNormal()
    {
        return _attackController.GetIsAttackingNormal(isAttackingWithArrow);
    }

    public void TakeDamage(float value)
    {
        _playerStats.Hp -= Mathf.FloorToInt(value);
    }

    public void flipBody()
    {
        Vector3 currentLocalScale = GetComponent<Rigidbody2D>().transform.localScale;
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > Mathf.Epsilon)
        {
            currentLocalScale.x = -Mathf.Abs(currentLocalScale.x);
        }
        else if (horizontalInput < -Mathf.Epsilon)
        {
            currentLocalScale.x = Mathf.Abs(currentLocalScale.x);
        }

        GetComponent<Rigidbody2D>().transform.localScale = currentLocalScale;

    }
    public void checkGround()
    {
        int playerLayer = 1 << LayerMask.NameToLayer("Player");
        playerLayer = ~playerLayer;
        float rayLength = transform.localScale.y / 2 + 0.7f;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + Vector3.right / 6 * transform.localScale.x, Vector2.down, rayLength, playerLayer);

        Debug.DrawRay(transform.position + Vector3.right / 6 * transform.localScale.x, Vector2.down * rayLength, Color.green);

        foreach (RaycastHit2D hit in hits)
        {
            Vector2 directionRaycast = hit.normal;

            float angle = Vector2.Angle(directionRaycast, Vector2.down);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    isGround = true;
                }
                else
                {
                    isGround = false;
                }
            }
            else
            {
                isGround = false;
            }

            PhysicsMaterial2D physicsMaterial2D = GetComponent<Rigidbody2D>().sharedMaterial;

            if (angle != 180.0f)
            {
                isRope = true;
                if (Input.GetAxis("Horizontal") != 0f || Input.GetKeyDown(KeyCode.W))
                {
                    physicsMaterial2D.friction = 1f;
                    GetComponent<Rigidbody2D>().sharedMaterial = physicsMaterial2D;
                }
                else if (Input.GetAxis("Horizontal") == Mathf.Epsilon)
                {
                    physicsMaterial2D.friction = 10f;
                    GetComponent<Rigidbody2D>().sharedMaterial = physicsMaterial2D;
                }
            }
            else
            {
                isRope = false;
                physicsMaterial2D.friction = 1f;
                GetComponent<Rigidbody2D>().sharedMaterial = physicsMaterial2D;
            }
        }

    }

    public void hitDmg()
    {
        isHitDmg = false;
    }
}