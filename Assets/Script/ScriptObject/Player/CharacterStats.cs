using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public float maxHP = 100f;
    public float speed = 5f;
    public float mana = 50f;
}
