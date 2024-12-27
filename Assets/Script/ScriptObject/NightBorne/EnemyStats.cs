using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField]
    string nameEnemy;

    [SerializeField]
    [Range(1, 100)]
    float maxSpeed;

    [SerializeField]
    [Range(1, 100)]
    float maxHp;

    [SerializeField]
    [Range(1, 100)]
    int attack;
    
    [SerializeField]
    [Range(1, 100)]
    int defense;

    public string NameEnemy { get => nameEnemy; }
    public float MaxSpeed { get => maxSpeed; }
    public float MaxHp { get => maxHp; }
    public int Attack { get => attack; }
    public int Defense { get => defense; }
}
