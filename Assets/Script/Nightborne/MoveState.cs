//using UnityEngine;

//public class NightBorneIdleState : IState
//{
//    readonly NightBorneController _nightBorne;
//    float _startTime;
//    public NightBorneIdleState(NightBorneController nightBorne)
//    {
//        _nightBorne = nightBorne;
//    }
//    public void OnEnter()
//    {
//        _nightBorne.SetAnimation("Idle");
//        _startTime = Time.time;
//        _nightBorne.StopMoving();
//    }

//    public void OnExit()
//    {

//    }

//    public void OnFixedUpdate()
//    {

//    }

//    public void OnUpdate()
//    {
//        _nightBorne.CheckPositionPlayer();

//        if (_nightBorne.canSeePlayer)
//        {
//            if (!_nightBorne.IsPlayerInRange(_nightBorne.attackRange) && Time.time - _startTime > .5f)
//            {
//                _nightBorne.stateMachine2.ChangeState(new NightBorneMoveState(_nightBorne));
//            }
//            else if (_nightBorne.IsPlayerInRange(_nightBorne.attackRange) && Time.time - _startTime > .3f && _nightBorne.CanUseSkill(_nightBorne.CDMeleeAttack))
//            {
//                _nightBorne.stateMachine2.ChangeState(new NightBorneMeleeAttackState(_nightBorne));
//            }
//            else if (!_nightBorne.IsPlayerInRange(5f) && _nightBorne.CanUseSkill(_nightBorne.CDAfterUseKill) && _nightBorne.IsPlayerInRange(6f) && _nightBorne.CanUseSkill(_nightBorne.CDNapalm))
//            {
//                _nightBorne.stateMachine2.ChangeState(new NightBorneNapalmSkillState(_nightBorne));
//            }
//            else if (!_nightBorne.IsPlayerInRange(4f) && _nightBorne.CanUseSkill(_nightBorne.CDAfterUseKill) && _nightBorne.IsPlayerInRange(5f) && _nightBorne.CanUseSkill(_nightBorne.CDFire))
//            {
//                _nightBorne.stateMachine2.ChangeState(new NightBorneFireSkillState(_nightBorne));
//            }
//            else if (!_nightBorne.IsPlayerInRange(2f) && _nightBorne.CanUseSkill(_nightBorne.CDAfterUseKill) && _nightBorne.IsPlayerInRange(3f) && _nightBorne.CanUseSkill(_nightBorne.CDIncendiary))
//            {
//                _nightBorne.stateMachine2.ChangeState(new NightBorneIncendiarySkillState(_nightBorne));
//            }
//        }
//        else
//        {
//            _nightBorne.CacaulatorDirection();
//            _nightBorne.Flip();
//        }
//    }
//}

//public class NightBorneMoveState : IState
//{
//    readonly NightBorneController _nightBorne;
//    float _startTime;

//    public NightBorneMoveState(NightBorneController nightBorne)
//    {
//        _nightBorne = nightBorne;
//    }
//    public void OnEnter()
//    {
//        _nightBorne.SetAnimation("Move");
//        _startTime = Time.time;
//    }
//    public void OnUpdate()
//    {
//        _nightBorne.Move(new Vector2(_nightBorne.data.MaxSpeed * Mathf.Sign(_nightBorne.transform.localScale.x), _nightBorne.rb.velocity.y));
//        _nightBorne.CheckPositionPlayer();

//        if (!_nightBorne.canSeePlayer || _nightBorne.IsPlayerInRange(_nightBorne.attackRange) || Time.time - _startTime > 2f)
//        {
//            _nightBorne.stateMachine2.ChangeState(new NightBorneIdleState(_nightBorne));
//        }
//    }

//    public void OnFixedUpdate()
//    {
//    }

//    public void OnExit()
//    {
//    }
//}

//public class NightBorneMeleeAttackState : IState
//{
//    readonly NightBorneController _nightBorne;

//    public NightBorneMeleeAttackState(NightBorneController nightBorne)
//    {
//        _nightBorne = nightBorne;
//    }

//    public void OnEnter()
//    {
//        _nightBorne.SetAnimation("AttackBasic");
//        _nightBorne.StopMoving();
//        _nightBorne.EnableMeleeAttackCollider();
//        _nightBorne.CDMeleeAttack.StartCooldown();
//    }

//    public void OnUpdate()
//    {
//        if (!_nightBorne.isAttacking)
//        {
//            _nightBorne.stateMachine2.ChangeState(new NightBorneIdleState(_nightBorne));
//        }
//    }

//    public void OnFixedUpdate()
//    {
//    }

//    public void OnExit()
//    {
//    }
//}

//public class NightBorneFireSkillState : IState
//{
//    readonly NightBorneController _nightBorne;

//    public NightBorneFireSkillState(NightBorneController nightBorne)
//    {
//        _nightBorne = nightBorne;
//    }
//    public void OnEnter()
//    {
//        _nightBorne.SetAnimation("SkillFire");
//        _nightBorne.StopMoving();
//        _nightBorne.CDFire.StartCooldown();
//        _nightBorne.isAttacking = true;
//        _nightBorne.CDAfterUseKill.StartCooldown();

//    }

//    public void OnUpdate()
//    {
//        if (!_nightBorne.isAttacking)
//        {
//            _nightBorne.DisableMeleeAttackCollider();

//            _nightBorne.stateMachine2.ChangeState(new NightBorneIdleState(_nightBorne));
//        }
//    }

//    public void OnFixedUpdate()
//    {
//    }

//    public void OnExit()
//    {
//    }
//}


//public class NightBorneHurtState : IState
//{
//    readonly NightBorneController _nightBorne;

//    public NightBorneHurtState(NightBorneController nightBorne)
//    {
//        _nightBorne = nightBorne;
//    }

//    public void OnEnter()
//    {
//        _nightBorne.SetAnimation("Hurt");
//    }

//    public void OnUpdate()
//    {
//    }

//    public void OnFixedUpdate()
//    {
//    }

//    public void OnExit()
//    {
//    }
//}

//public class NightBorneDeathState : IState
//{
//    readonly NightBorneController _nightBorne;

//    public NightBorneDeathState(NightBorneController nightBorne)
//    {
//        _nightBorne = nightBorne;
//    }

//    public void OnEnter()
//    {
//        _nightBorne.SetAnimation("Death");
//    }

//    public void OnUpdate()
//    {
//    }

//    public void OnFixedUpdate()
//    {
//    }

//    public void OnExit()
//    {
//    }
//}

//public class NightBorneNapalmSkillState : IState
//{
//    readonly NightBorneController _nightBorne;

//    public NightBorneNapalmSkillState(NightBorneController nightBorne)
//    {
//        _nightBorne = nightBorne;
//    }

//    public void OnEnter()
//    {
//        _nightBorne.SetAnimation("SkillNapalm");
//        _nightBorne.StopMoving();
//        _nightBorne.CDNapalm.StartCooldown();
//        _nightBorne.isAttacking = true;
//        _nightBorne.CDAfterUseKill.StartCooldown();
//    }
//    public void OnUpdate()
//    {
//        if (!_nightBorne.isAttacking)
//        {
//            _nightBorne.stateMachine2.ChangeState(new NightBorneIdleState(_nightBorne));
//        }
//    }

//    public void OnFixedUpdate()
//    {
//    }

//    public void OnExit()
//    {
//    }
//}

//public class NightBorneIncendiarySkillState : IState
//{
//    readonly NightBorneController _nightBorne;

//    public NightBorneIncendiarySkillState(NightBorneController nightBorne)
//    {
//        _nightBorne = nightBorne;
//    }

//    public void OnEnter()
//    {
//        _nightBorne.SetAnimation("SkillIncendiary");
//        _nightBorne.StopMoving();
//        _nightBorne.CDIncendiary.StartCooldown();
//        _nightBorne.isAttacking = true;
//        _nightBorne.CDAfterUseKill.StartCooldown();
//    }
//    public void OnUpdate()
//    {
//        if (!_nightBorne.isAttacking)
//        {
//            _nightBorne.stateMachine2.ChangeState(new NightBorneIdleState(_nightBorne));
//        }
//    }

//    public void OnFixedUpdate()
//    {
//    }

//    public void OnExit()
//    {
//    }
//}
