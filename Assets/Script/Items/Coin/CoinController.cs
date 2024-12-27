using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public CoinStats coinStats;

    private SpriteRenderer coinIcon;
    private string coinName;
    private int coinPoints;

    // Start is called before the first frame update
    void Start()
    {
        coinIcon = GetComponent<SpriteRenderer>();
        if (coinStats != null)
        {
            coinIcon.sprite = coinStats.sprite;
            //coinName.text = coinStats.name;
            coinPoints = coinStats.point;
        }
        else
        {
            Debug.LogError("CoinStats chưa được gán trong Inspector.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.Coin += coinPoints;
            }
            CoinPool.Instance.ReturnCoin(this.gameObject);
        }
    }
}
