using UnityEngine;

public class GlassPlatform : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] SpriteMask mask;
    [SerializeField] bool canChange = false;

    // Đảm bảo BoxCollider2D ở object cha và CircleCollider2D ở object con có isTrigger bật
    private void Update()
    {
        if (!canChange) return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            float directionRotation = 0f;
            if (Input.GetKey(KeyCode.UpArrow))
                directionRotation += 1f;
            if (Input.GetKey(KeyCode.DownArrow))
                directionRotation -= 1f;

            if (directionRotation != 0f)
            {
                transform.Rotate(0, 0, directionRotation * rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is BoxCollider2D)
        {
            Debug.Log("BoxCollider triggered on Parent");
        }

        if (other is CircleCollider2D)
        {
            Debug.Log("CircleCollider triggered on Child");
        }

        if (other.CompareTag("Player"))
        {
            canChange = true;
            mask.enabled = true;
        }
        else
        {
            canChange = false;
            mask.enabled = false;
        }
    }
}
