using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void Notify(EventType eventType, GameObject target, float damage, Vector3 explosionPosition);
}

public enum EventType
{
    PlayerHit,
    MonsterHit,
    TrapTriggered,
    ItemPicked
}

public class EventObserver : IObserver
{
    public void Notify(EventType eventType, GameObject target, float damage, Vector3 explosionPosition)
    {
        switch (eventType)
        {
            case EventType.PlayerHit:
                target.GetComponent<Player>().TakeDamage(damage);
                break;
        }
    }
}