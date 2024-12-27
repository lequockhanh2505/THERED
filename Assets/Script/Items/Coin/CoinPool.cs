using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public static CoinPool Instance;  // Singleton instance

    public GameObject prefabsCoin;
    public int poolsize = 10;
    public Queue<GameObject> coinPool;

    private void Awake()
    {
        // Thiết lập instance cho singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // Đảm bảo chỉ có một instance
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        coinPool = new Queue<GameObject>();

        for (int i = 0; i < poolsize; i++) {
            GameObject coin = Instantiate(prefabsCoin, transform);
            coin.SetActive(false);
            coinPool.Enqueue(coin);
        }

    }

    public GameObject GetCoin() {

        if (coinPool.Count > 0) 
        {
            GameObject coin = coinPool.Dequeue();
            coin.SetActive(true);
            return coin;
        }
        else
        {
            GameObject coin = Instantiate(prefabsCoin, transform);
            return coin;
        }
    }

    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
        coinPool.Enqueue(coin);
    }
}
