using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinStats", menuName = "Stats/CoinStats")]
public class CoinStats : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public int point;
}
