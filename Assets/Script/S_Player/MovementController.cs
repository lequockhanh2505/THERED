using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerStats _playerStats;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerStats>();
    }

    public void Move(float direction)
    {
        Vector2 velocity = new Vector2(direction * _playerStats.Speed, _rb.velocity.y);
        _rb.velocity = velocity;
    }

    public void Jump()
    {
        _rb.AddForce(Vector2.up * _playerStats.ForceJump, ForceMode2D.Impulse);
    }

    public void Dash()
    {
        _rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * _playerStats.DashForce, _rb.velocity.y);
    }

    public bool checkDash(bool canDash)
    {
        int playerLayer = 1 << LayerMask.NameToLayer("Player");
        playerLayer = ~playerLayer;
        float rayLength = transform.localScale.y / 2 + 0.2f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.left / 2 * Mathf.Sign(transform.localScale.x), Vector2.down, rayLength, playerLayer);
        Debug.DrawRay(transform.position + Vector3.left / 2 * Mathf.Sign(transform.localScale.x), Vector2.down * rayLength, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Ground"))
            {
                return canDash = true;
            }
            else
            {
                return canDash = false;
            }
        }
        else
        {
            return canDash = false;
        }
    }

    public float GetHorizontalVelocity() => _rb.velocity.x;
    public float GetVerticalVelocity() => _rb.velocity.y;
}
