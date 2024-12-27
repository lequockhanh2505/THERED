using UnityEngine;

public class BoarEnemyHitState : IState
{
    private BoarEnemyController _boar;
    private float hitStartTime;

    public BoarEnemyHitState(BoarEnemyController boar)
    {
        _boar = boar;
    }

    public void OnEnter()
    {
        _boar.animationController.SetAnimation("Hit");
        hitStartTime = Time.time;
        _boar.StopMoving();
    }

    public void OnUpdate()
    {
        if (Time.time - hitStartTime >= _boar.hitDuration)
        {
            _boar.stateMachine.ChangeState(new BoarEnemyIdleState(_boar));
        }
    }

    public void OnExit() { }

    public void OnFixedUpdate() { }
}
