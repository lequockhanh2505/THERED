using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int hp = 10;
    [SerializeField]
    private int coin = 100;
    [SerializeField]
    [Range(1, 50)]
    private float speed = 7f;
    [SerializeField]
    [Range(1, 50)]
    private float forceJump = 15f;
    [SerializeField]
    private float dashForce = -15f;

    public int Hp
    {
        get { return hp; }
        set
        {
            int newHp = Mathf.Max(0, value);
            if (hp != newHp)
            {
                hp = newHp;
                EventManager.Instance.PlayerHealthChanged(hp);
            }
        }
    }

    public int Coin
    {
        get { return coin; }
        set
        {

            coin = value;
            EventManager.Instance.PlayerCoinChanged(coin);
        }
    }

    public float Speed { get => speed; set => speed = value; }
    public float ForceJump { get => forceJump; set => forceJump = value; }
    public float DashForce { get => dashForce; set => dashForce = value; }
}
