using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Trạng thái Attack
public class BoarEnemyAttackState : IState
{
    private BoarEnemyController _boar;
    private float chargeStartTime;
    private float chargeDuration = 2f;
    private Vector2 targetPosition;
    public static event Action<int> OnPlayerHitDame;
    public int dmg = 1;

    public BoarEnemyAttackState(BoarEnemyController boar)
    {
        _boar = boar;
    }

    public void OnEnter()
    {
        _boar.animationController.SetAnimation("Attack");
        chargeStartTime = Time.time;
        _boar.StopMoving();

        //_boar.PlaySound(_boar.attackClip);
        targetPosition = _boar.player.position;
    }

    public void OnUpdate()
    {
        float elapsedTime = Time.time - chargeStartTime;

        if (elapsedTime >= chargeDuration)
        {
            _boar.stateMachine.ChangeState(new BoarEnemyIdleState(_boar));
            return;
        }

        if (elapsedTime >= 0.5f) // Chuẩn bị xong và bắt đầu lao tới
        {
            Vector2 chargeVelocity = new Vector2(
                _boar.chargeSpeed * Mathf.Sign(targetPosition.x - _boar.transform.position.x),
                0
            );
            _boar.Move(chargeVelocity);

            // Nếu chạm người chơi, gây sát thương và chuyển về trạng thái chờ
            if (_boar.IsPlayerInRange(_boar.attackRange))
            {
                OnPlayerHitDame?.Invoke(dmg);
                _boar.stateMachine.ChangeState(new BoarEnemyIdleState(_boar));
                return;
            }
        }

        // Nếu người chơi thoát khỏi tầm nhìn, chuyển về trạng thái chờ
        if (!_boar.IsPlayerInRange(_boar.detectionRange))
        {
            Debug.Log("Player out of range. Preparing for next attack.");
            _boar.stateMachine.ChangeState(new BoarEnemyIdleState(_boar));
        }
    }

    public void OnExit()
    {
        _boar.StopMoving();
    }

    public void OnFixedUpdate() { }
}