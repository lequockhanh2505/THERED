using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public event Action<int> OnPlayerHealthChanged;
    public event Action<int> OnPlayerCoinChanged;

    public void PlayerHealthChanged(int newHealth)
    {
        OnPlayerHealthChanged?.Invoke(newHealth);
    }

    public void PlayerCoinChanged(int newCoin)
    {
        OnPlayerCoinChanged?.Invoke(newCoin);
    }
}
