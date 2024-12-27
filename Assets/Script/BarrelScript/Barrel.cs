using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    Animator _animator;
    Rigidbody2D _rb;
    public Collider2D[] colliders;
    public GameObject player;

    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
        Debug.Log("Observer added.");
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
        Debug.Log("Observer removed.");
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        EventObserver observer = new EventObserver();
        AddObserver(observer);
    }

    void Update()
    {
        Vector3 explosionPos = transform.position;
        colliders = Physics2D.OverlapCircleAll(explosionPos, radius);

        if (colliders.Length == 0)
        {
            Debug.LogWarning("Không tìm thấy vật thể nào trong bán kính vụ nổ.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            _animator.Play("BarrelHit");
            StartCoroutine(WaitHitDestroy(3f));
        }
    }

    float radius = 1f;
    float forceExplosive = 100f;

    IEnumerator WaitHitDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _animator.Play("BarrelDestroy");
        yield return new WaitForSeconds(.3f);

        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direction = hit.ClosestPoint(transform.position) - GetComponent<Collider2D>().ClosestPoint(transform.position);
                float distance = direction.magnitude; // Khoảng cách giữa vật thể và vụ nổ

                direction.Normalize();
                float force = forceExplosive * (1 - distance / radius);

                if (hit.CompareTag("Player"))
                {
                    if (player != null && observers.Count > 0)
                    {
                        foreach (IObserver observer in observers)
                        {
                            observer.Notify(EventType.PlayerHit, player, 3, transform.position);
                            Debug.Log("Thông báo gửi đến observer.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Player hoặc observers chưa được khởi tạo.");
                    }
                }

                rb.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }

        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
