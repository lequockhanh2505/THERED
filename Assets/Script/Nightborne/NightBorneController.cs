//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NightBorneController : MonoBehaviour
//{
//    public AnimationController2 animationController;
//    public StateMachine2 stateMachine2;
//    public Rigidbody2D rb;

//    [SerializeField] private Vector2 direction;

//    public bool canSeePlayer = false;
//    public bool isAttacking = false;

//    public GameObject prefabsNapalm;
//    public GameObject prefabsFire;
//    public GameObject prefabsIncendiary;

//    public EnemyStats data;

//    public Transform player;

//    public float detectionRange = 100f;
//    public float attackRange = 2f;


//    public Cooldown CDMeleeAttack;
//    public Cooldown CDFire;
//    public Cooldown CDNapalm;
//    public Cooldown CDIncendiary;
//    public Cooldown CDAfterUseKill;

//    [SerializeField] private GameObject _meleeAttackCollider;
//    [SerializeField] private int _meleeDamage = 1;
//     public static event Action<int> OnPlayerHitDame;

//    [SerializeField] private int _currentHealth;
//    public event Action<int> OnDamageTaken;


//    void Start()
//    {
//        animationController = GetComponent<AnimationController2>();
//        rb = GetComponent<Rigidbody2D>();
//        _currentHealth = data.maxHealth;

//        stateMachine2 = new StateMachine2();
//        SetDefaultCooldownForSkill();
//        stateMachine2.ChangeState(new NightBorneIdleState(this));
//         _meleeAttackCollider.SetActive(false);
//    }

//    void Update()
//    {
//        stateMachine2.Update();
//        UpdateCooldownTime();
//         if (player != null)
//        {
//            CacaulatorDirection();
//        }
//    }
//    private void UpdateCooldownTime()
//    {
//        CDMeleeAttack.UpdateCooldown();
//        CDIncendiary.UpdateCooldown();
//        CDNapalm.UpdateCooldown();
//        CDFire.UpdateCooldown();
//        CDAfterUseKill.UpdateCooldown();
//    }

//    public void CacaulatorDirection()
//    {
//        direction = player.position - transform.position;
//        direction.Normalize();
//    }

//    private void SetDefaultCooldownForSkill()
//    {
//        CDMeleeAttack = new Cooldown(2f);
//        CDFire = new Cooldown(5f);
//        CDNapalm = new Cooldown(10000000000f);
//        CDIncendiary = new Cooldown(10f);
//        CDAfterUseKill = new Cooldown(3f);
//    }

//    public bool IsPlayerInRange(float range)
//    {
//         if (player != null)
//        {
//            return Vector2.Distance(transform.position, player.position) <= range;
//        }
//         return false;
//    }

//    public bool CanUseSkill(Cooldown cooldown)
//    {
//        return !cooldown.isCoolingDown && cooldown.currentTime == 0.0f;
//    }

//    public void StopMoving()
//    {
//        rb.velocity = Vector3.zero;
//    }

//    public void Move(Vector2 velocity)
//    {
//        rb.velocity = velocity;
//    }

//    public void SetAnimation(string animatiom)
//    {
//        animationController.SetAnimation(animatiom);
//    }

//    public void Flip()
//    {
//        Vector3 currentLocalScale;

//        if (direction.x > 0)
//        {
//            currentLocalScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
//        }
//        else if (direction.x < 0)
//        {
//            currentLocalScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
//        }
//        else
//        {
//            currentLocalScale = transform.localScale;
//        }

//        transform.localScale = currentLocalScale;
//    }

//    public void CheckPositionPlayer()
//    {
//        int layerPlayer = LayerMask.GetMask("Player");
//        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRange, layerPlayer);

//         if (hit != null)
//        {
//            player = hit.transform;
//            canSeePlayer = true;
//        }
//         else
//         {
//            canSeePlayer = false;
//         }

//         if (player != null)
//         {
//            if (Mathf.Sign(player.position.x - transform.position.x) != Mathf.Sign(transform.localScale.x))
//            {
//                Flip();
//            }
//         }
//    }

//    public void SetAttackingDone() //eventAnimation
//    {
//        isAttacking = false;
//    }

//    public void SetActivePrefabsNapalm() //eventAnimation
//    {
//        prefabsNapalm.SetActive(true);
//    }

//    public void SetActivePrefabsFire() //eventAnimation
//    {
//        prefabsFire.SetActive(true);
//    }

//    public void SetActivePrefabsIncendiary() //eventAnimation
//    {
//        prefabsIncendiary.SetActive(true);
//    }
//     public void EnableMeleeAttackCollider()
//        {
//            _meleeAttackCollider.SetActive(true);
//             isAttacking = true;
//        }
//    public void DisableMeleeAttackCollider()
//        {
//            _meleeAttackCollider.SetActive(false);
//             isAttacking = false;
//        }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (_meleeAttackCollider.activeInHierarchy)
//        {
//          if (collision.CompareTag("Player"))
//            {
//               OnPlayerHitDame?.Invoke(_meleeDamage);
//             }
//       }
//    }
//}