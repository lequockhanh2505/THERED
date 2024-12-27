using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField] GameObject arrow;
    Rigidbody2D rb;
    [SerializeField]
    private float _speed = 10.0f;

    [SerializeField]
    GameObject target;

    [SerializeField]
    GameObject bow;

    [SerializeField]
    GameObject player;

    private bool isTarget = false;

    static public float angleToTarget;

    Vector2 directionToTarget;


    Vector3 position;

    private Vector3 launchPosition;
    float gravity = 9.81f;

    public Vector2 landingPosition;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        rb = arrow.GetComponent<Rigidbody2D>();
        //lineRenderer = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Instantiate(arrow, transform.position, transform.rotation);
        }

        if (!isTarget)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                isTarget = true;
                target.SetActive(isTarget);
                bow.SetActive(isTarget);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                isTarget = false;
                target.SetActive(isTarget);
                bow.SetActive(isTarget);

            }
        }

        if (isTarget && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting();
        }

        //Vector3 mousePos = Input.mousePosition;
        //{
        //    Debug.Log("x: " + mousePos.x);
        //    Debug.Log("y: " + mousePos.y);
        //}

        // Lấy vị trí chuột trong không gian thế giới
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = bow.transform.position.z; // Đảm bảo cùng mặt phẳng Z với bow
        target.transform.position = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        // Tính vector chỉ hướng từ bow đến vị trí chuột
        Vector2 directionToTarget = new Vector2(mouseWorldPos.x, mouseWorldPos.y) - new Vector2(bow.transform.position.x, bow.transform.position.y);
        // Tính toán góc giữa vector hiện tại của bow và vector chỉ hướng
        angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        if(player.transform.localScale.x < 0f)
        {
            bow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToTarget + 180));
        }
        else
        {
            bow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToTarget));
        }
    }

    private void Shooting()
    {
        Instantiate(arrow, transform.position, bow.transform.rotation);
    }
}