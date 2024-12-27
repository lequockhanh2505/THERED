using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    private int currentCoin;
    public TMP_Text amountCoin;
    private int maxHP;

    public Slider healthBar;

    private void Start()
    {
        maxHP = FindObjectOfType<PlayerStats>().Hp;
        currentCoin = FindObjectOfType<PlayerStats>().Coin;

        healthBar.maxValue = maxHP;
        healthBar.value = maxHP;

        amountCoin.text = currentCoin.ToString();
    }

    private void OnEnable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnPlayerHealthChanged += UpdateHealthBar;
            EventManager.Instance.OnPlayerCoinChanged += UpdateCoinUI;
        }
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnPlayerHealthChanged -= UpdateHealthBar;
            EventManager.Instance.OnPlayerCoinChanged -= UpdateCoinUI;

        }
    }

    private void UpdateHealthBar(int newHealth)
    {
        healthBar.value = newHealth;
    }

    private void UpdateCoinUI(int newCoin)
    {
        amountCoin.text = newCoin.ToString();
    }
}
