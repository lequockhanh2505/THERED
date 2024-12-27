using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IdleState: IState
{
    Player player;

    public IdleState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        player.PlayAnimation("Idle");
    }

    public void OnExit()
    {
    }
    public void OnUpdate()
    {

        if (Input.GetAxis("Horizontal") != 0.0f)
        {
            player.stateMachine.ChangeState(new RunningState(player));
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            player.stateMachine.ChangeState(new JumpingState(player));
        }

        if (!player.isGround && player.GetVerticalVelocity() <= 0)
        {
            player.stateMachine.ChangeState(new FallingState(player));
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            player.stateMachine.ChangeState(new AttakingWithArrow(player));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            player.stateMachine.ChangeState(new AtkNor1State(player));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            player.stateMachine.ChangeState(new SitState(player));
        }

        if (Input.GetKeyDown(KeyCode.K) && player.canDash)
        {
            player.stateMachine.ChangeState(new DashState(player));
        }

        if (player.isHurt)
        {
            player.stateMachine.ChangeState(new DeathState(player, player._uIManager));
        }
    }

    public void OnFixedUpdate()
    {
        player.flipBody();
    }
}

public class RunningState : IState
{
    Player player;

    public RunningState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        //Debug.Log("Running State Enter");
        player.PlayAnimation("Run");
    }

    public void OnExit()
    {
        //Debug.Log("Running State Exit");
    }

    public void OnFixedUpdate()
    {
        player.Move(Input.GetAxis("Horizontal"));

        player.flipBody();
    }

    public void OnUpdate()
    {

        if (Mathf.Abs(Input.GetAxis("Horizontal")) == 0.0f)
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            player.stateMachine.ChangeState(new JumpingState(player));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            player.stateMachine.ChangeState(new SitState(player));
        }

        if (!player.isGround && player.GetVerticalVelocity() <= 0)
        {
            player.stateMachine.ChangeState(new FallingState(player));
        }

        if (player.isHurt)
        {
            player.stateMachine.ChangeState(new DeathState(player, player._uIManager));
        }
    }
}

public class FallingState: IState
{
    Player player;

    public FallingState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        player.PlayAnimation("Fall");
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
        player.Move(Input.GetAxis("Horizontal"));
    }

    public void OnUpdate()
    {
        player.flipBody();

        //Debug.Log(velocityY = player.GetVerticalVelocity());
        if (player.isGround)
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }
    }
}

public class JumpingState : IState
{
    Player player;

    private float startFallTime;
    private float startTimeJump;
    private bool isFalling;

    public JumpingState(Player player) {

        this.player = player;
    }

    public void OnEnter()
    {
        isFalling = false;
        startTimeJump = Time.time;
        if (player.isRope)
        {
            player.PlayAnimation("JumpAndFall");
            player.Jump();
        }
        else
        {
            player.PlayAnimation("ChargeJump");
        }
    }

    public void OnExit()
    {
        //Debug.Log("Jumping Exit");
    }

    public void OnFixedUpdate()
    {

        player.flipBody();
    }

    public void OnUpdate()
    {
        player.PlayAnimation("JumpAndFall", "VelocityY", player.GetVerticalVelocity());
        player.Move(Input.GetAxis("Horizontal"));

        if (player.isGround)
        {
            if (!isFalling)
            {
                startFallTime = Time.time;
                isFalling = true;
                //player.SetAnimation("FallInGround");
            }
            if (Time.time - startFallTime >= 0.15f)
            {
                //Kiểm tra điều kiện Input trước khi chuyển sang IdleState hoặc RunningState
                if (Input.GetAxis("Horizontal") != 0.0f)
                {
                    player.stateMachine.ChangeState(new RunningState(player));
                }
                else
                {
                    player.stateMachine.ChangeState(new IdleState(player));
                }
            }
        }
    }
}


public class AttakingWithArrow : IState
{

    public Player player;
    private bool isAttackingWithArrow = false;
    private float startState;
    //private bool isAttackingNormal;

    public AttakingWithArrow(Player player) { this.player = player; }
    public void OnEnter()
    {
        startState = Time.time;
        isAttackingWithArrow = true;
        player.PlayAnimation("AtkByArrow");
    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void OnUpdate()
    {

        if (Time.time - startState >= player.GetAnimationLength("AtkByArrow"))
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon)
        {
            isAttackingWithArrow = false;
            player.stateMachine.ChangeState(new RunningState(player));
        }
    }
}

public class AtkNor1State : IState
{
    Player player;
    float startTime;

    public AtkNor1State(Player player) { this.player = player; }

    public void OnEnter()
    {
        player.PlayAnimation("AtkNor" + player.combo);
        //player.MoveStraight(-2f);
        startTime = Time.time;

        //Debug.Log("AtkNor" + player.combo);
    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {
        player.Move(Input.GetAxis("Horizontal"));
    }

    public void OnUpdate()
    {
        if (Time.time - startTime > player.GetAnimationLength("AtkNor" + player.combo) * 0.65 && Input.GetKeyDown(KeyCode.L))
        {
            player.ContinueAttack();
        }

        if (player.combo == 2)
        {
            player.stateMachine.ChangeState(new AtkNor2State(player));
        }

        if (!player.isAttackingNormal)
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }
    }
}

public class AtkNor2State : IState
{
    Player player;

    float startTime;

    public AtkNor2State(Player player) { this.player = player; }

    public void OnEnter()
    {
        player.PlayAnimation("AtkNor" + player.combo);
        startTime = Time.time;
    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {
        player.Move(Input.GetAxis("Horizontal"));
    }

    public void OnUpdate()
    {
        if (Time.time - startTime > player.GetAnimationLength("AtkNor" + player.combo) * 0.65 && Input.GetKeyDown(KeyCode.L))
        {
            player.ContinueAttack();
        }

        if (player.combo == 3)
        {
            player.stateMachine.ChangeState(new AtkNor3State(player));
        }

        if (!player.isAttackingNormal)
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }
    }
}

public class AtkNor3State : IState
{
    Player player;
    float startTime;
    public AtkNor3State(Player player) { this.player = player; }

    public void OnEnter()
    {
        player.PlayAnimation("AtkNor" + player.combo);
        startTime = Time.time;
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
        player.Move(Input.GetAxis("Horizontal"));
    }

    public void OnUpdate()
    {
        if (Time.time - startTime > player.GetAnimationLength("AtkNor" + player.combo) * 0.65 && Input.GetKeyDown(KeyCode.L))
        {
            player.ContinueAttack();
        }

        if (player.combo == 4)
        {
            player.stateMachine.ChangeState(new AtkNor4State(player));
        }

        if (!player.isAttackingNormal)
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }
    }
}

public class AtkNor4State : IState
{
    Player player;
    float startTime;
    public AtkNor4State(Player player) { this.player = player; }

    public void OnEnter()
    {
        player.PlayAnimation("AtkNor" + player.combo);
        startTime = Time.time;
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
        player.Move(Input.GetAxis("Horizontal"));
    }

    public void OnUpdate()
    {
        if (Time.time - startTime > player.GetAnimationLength("AtkNor" + player.combo) * 0.65 && Input.GetKeyDown(KeyCode.L))
        {
            player.ContinueAttack();
        }

        if (player.combo == 5)
        {
            player.stateMachine.ChangeState(new AtkNor5State(player));
        }

        if (!player.isAttackingNormal)
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }
    }
}

public class AtkNor5State : IState
{
    Player player;
    float startTime;
    public AtkNor5State(Player player) { this.player = player; }

    public void OnEnter()
    {
        player.PlayAnimation("AtkNor" + player.combo);
        startTime = Time.time;
        //player.MoveStraight(-2f);

    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
        player.Move(Input.GetAxis("Horizontal"));
    }

    public void OnUpdate()
    {
        if (Time.time - startTime > player.GetAnimationLength("AtkNor" + player.combo) * 0.65 && Input.GetKeyDown(KeyCode.L))
        {
            player.ContinueAttack();
        }

        if (player.combo == 6)
        {
            player.stateMachine.ChangeState(new AtkNor6State(player));
        }

        if (!player.isAttackingNormal)
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }
    }
}

public class AtkNor6State : IState
{
    Player player;
    public AtkNor6State(Player player) { this.player = player; }

    public void OnEnter()
    {
        player.PlayAnimation("AtkNor" + player.combo);
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
        player.Move(Input.GetAxis("Horizontal"));
    }

    public void OnUpdate()
    {
        if (!player.isAttackingNormal)
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }
    }
}

public class SitState : IState
{
    public Player player;

    public SitState(Player player) { this.player = player; }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void OnUpdate()
    {
        if (Input.GetKey(KeyCode.S))
        {
            player.PlayAnimation("Sit");
        }
        else
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }
    }
}

public class DeathState : IState
{
    private Player _player;
    private bool _isDeath;
    private UIManager _uiManager; // Thêm biến tham chiếu UI Manager
    public DeathState(Player player, UIManager uiManager) // Thêm tham số UI Manager vào constructor
    {
        _player = player;
        _uiManager = uiManager; // Gán giá trị từ tham số
    }

    public void OnEnter()
    {
        if (_player._playerStats.Hp <= 0)
        {
            _isDeath = true;
            _player.PlayAnimation("PlayerDeath");
            _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _player.enabled = false;
            _uiManager.LoadContinuteScene(); // Gọi hàm chuyển sang scene continue
        }
        else
        {
            _isDeath = false;
            _player.PlayAnimation("HitDmg");
        }
    }

    public void OnExit()
    {
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        if (_isDeath == false)
        {
            if (!_player.isHurt)
            {
                _player.stateMachine.ChangeState(new IdleState(_player));
            }
        }
    }
}

public class DashState : IState
{
    public Player player;

    public DashState(Player player) { this.player = player; }

    public void OnEnter()
    {
        player.PlayAnimation("Dash");
        player.Dash();
    }

    public void OnExit()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void OnUpdate()
    {
        if (!player.canDash)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        if(player.GetHorizontalVelocity() == 0f)
        {
            player.stateMachine.ChangeState(new IdleState(player));
        }
    }
}
