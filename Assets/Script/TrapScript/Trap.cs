using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public List<GameObject> listPos = new List<GameObject>();
    [SerializeField] GameObject targetPos;
    [SerializeField] int indexPos = 0;
    [SerializeField] bool isReverse = false;
    [SerializeField] bool isEndPoint = false;
    [SerializeField] [Range (0.0f, 10f)] float speed = 5f;

    public static event Action<int> OnPlayerHitDame;
    public int dmg = 1;


    private void Start()
    {
        if (listPos == null)
        {
            Debug.Log("List position is null");
            listPos = new List<GameObject>();
        }

        if (listPos.Count == 0)
        {
            Debug.Log("List position is empty");
            return;
        }

        if (targetPos == null)
        {
            Debug.Log("Target Position start is null");
            targetPos = listPos[indexPos];
        }

        transform.position = listPos[indexPos].gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Player"))
        {
            OnPlayerHitDame?.Invoke(dmg);
        }
    }

    void Update()
    {
        if (targetPos == null)
        {
            if (!isReverse)
            {
                if (indexPos < listPos.Count)
                {
                    indexPos = (indexPos + 1) % listPos.Count;
                }
            }

            if (isReverse)
            {
                if (!isEndPoint && indexPos < listPos.Count - 1)
                {
                    indexPos++;
                    if (indexPos == listPos.Count - 1) isEndPoint = true;
                }
                else if (isEndPoint && indexPos > 0)
                {
                    indexPos--;
                    if (indexPos == 0) isEndPoint = false;
                }
            }

            targetPos = listPos[indexPos];
        }
    }
    private void FixedUpdate()
    {
        if (targetPos != null && Vector2.Distance(transform.position, targetPos.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos.transform.position, speed * Time.deltaTime);
        }

        else
        {
            transform.Translate(Vector2.zero * Time.deltaTime);
            targetPos = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < listPos.Count - 1; i++)
        {
            GameObject current = listPos[i];
            GameObject next = listPos[i + 1];
            Gizmos.DrawLine(current.transform.position, next.transform.position);

            if (!isReverse) {
                Gizmos.DrawLine(listPos[listPos.Count - 1].transform.position, listPos[0].transform.position);
            }
        }
    }
}
