// Trạng thái Idle
using UnityEngine;

public class BoarEnemyIdleState : IState
{
    private BoarEnemyController _boar;
    private float idleDuration;
    private float idleStartTime;

    public BoarEnemyIdleState(BoarEnemyController boar)
    {
        _boar = boar;
    }

    public void OnEnter()
    {
        _boar.StopMoving();
        _boar.animationController.SetAnimation("Idle");
        //_boar.PlaySound(_boar.idleClip);
        idleDuration = Random.Range(2f, 3f);
        idleStartTime = Time.time;
    }

    public void OnUpdate()
    {
        if (Time.time - idleStartTime >= idleDuration)
        {
            _boar.stateMachine.ChangeState(new BoarEnemyMoveState(_boar));
            return;
        }

        if (_boar.IsPlayerInRange(_boar.detectionRange))
        {
            _boar.stateMachine.ChangeState(new BoarEnemyAttackState(_boar));
        }
    }

    public void OnExit() { }

    public void OnFixedUpdate() { }
}