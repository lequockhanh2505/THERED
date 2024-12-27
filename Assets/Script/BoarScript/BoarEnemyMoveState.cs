// Trạng thái Move
using UnityEngine;

public class BoarEnemyMoveState : IState
{
    private BoarEnemyController _boar;
    private float randomDistance;
    private float currentDistanceTraveled;
    private Vector2 startPosition;

    public BoarEnemyMoveState(BoarEnemyController boar)
    {
        _boar = boar;
    }

    public void OnEnter()
    {
        _boar.animationController.SetAnimation("Move");
        randomDistance = Random.Range(50f, 100f);
        currentDistanceTraveled = 0f;
        //_boar.PlaySound(_boar.idleClip);
        startPosition = _boar.transform.position;
    }

    public void OnUpdate()
    {
        if (HandleTransitions()) return;

        currentDistanceTraveled = Vector2.Distance(startPosition, _boar.transform.position);

        if (currentDistanceTraveled >= randomDistance)
        {
            FlipAndReset();
            return;
        }

        _boar.Move(new Vector2(_boar.walkSpeed * Mathf.Sign(_boar.transform.localScale.x), _boar.rb.velocity.y));
    }

    public void OnExit()
    {
        _boar.StopMoving();
    }

    private bool HandleTransitions()
    {
        if (_boar.IsPlayerInRange(_boar.detectionRange))
        {
            _boar.stateMachine.ChangeState(new BoarEnemyAttackState(_boar));
            return true;
        }

        if (_boar.IsObstacleAhead())
        {
            FlipAndReset();
            return true;
        }

        return false;
    }

    private void FlipAndReset()
    {
        _boar.Flip(-_boar.transform.localScale.x);
        randomDistance = Random.Range(50f, 100f);
        currentDistanceTraveled = 0f;
        startPosition = _boar.transform.position;
    }

    public void OnFixedUpdate() { }
}