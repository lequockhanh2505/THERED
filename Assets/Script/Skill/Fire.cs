using System;
using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    Transform startPos;

    [SerializeField]
    Transform nightBorne;

    [SerializeField]
    float _speed = 10.0f;

    [SerializeField]
    float scaleFactor = 1.2f; // Mức độ tăng kích thước

    int dmg = 1;

    public float startTime;

    public static event Action<int> OnPlayerHitDame;

    bool isThrow = false;
    Vector3 initialScale; // Kích thước ban đầu

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    private void OnEnable()
    {
        isThrow = false;
        startTime = Time.time;
        transform.localScale = initialScale * scaleFactor;

    }

    private void Update()
    {
        if (!isThrow)
        {
            transform.position = startPos.position;
            GetComponent<Rigidbody2D>().velocity = Vector2.right * _speed * Mathf.Sign(nightBorne.localScale.x);
            GetComponent<Animator>().Play("Fire");
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * nightBorne.localScale.x, transform.localScale.y);
            isThrow = true;
        }
        if (Time.time - startTime > 5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Fire takedame");
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                StartCoroutine(waitAnimation(0.5f));
                OnPlayerHitDame?.Invoke(dmg);
            }
        }
    }

    IEnumerator waitAnimation(float delay)
    {
        GetComponent<Animator>().Play("Fire2");
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}