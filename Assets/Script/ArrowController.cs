using System.Collections;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemyController = collision.GetComponent<BoarEnemyController>();
            if (enemyController != null)
            {
                enemyController.OnDamageTaken += HandleEnemyDamage;
                enemyController.TakeDamage(10);
            }

            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }

    private void HandleEnemyDamage(int remainingHealth)
    {
        Debug.Log(remainingHealth);
    }
}
