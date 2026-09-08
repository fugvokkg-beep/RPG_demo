using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private float attackVelocityTimer;

    private const int FirstComboIndex = 1;//开始连击索引
    private int attackDir;
    private int comboIndex = 1;
    private int comboLimit = 3;
    private bool comboAttackQueued;

    private float lastTimeAttacted;
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolname) : base(player, stateMachine, animBoolname)
    {
        if (comboLimit != player.attackVelocity.Length)
        {
            Debug.LogWarning("根据攻击速度调整了攻击数组限制");
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();

        comboAttackQueued = false;
        ResetComboIndexIfNeeded();

        //根据输入定义攻击方向
        attackDir = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDer;
        //if(player.moveInput.x != 0)
        //{
        //    attackDir = (int)player.moveInput.x;
        //}
        //else
        //{
        //    attackDir = player.facingDer;
        //}


        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }


    public override void update()
    {
        base.update();

        HandleAttackVelocity();

        if (input.Player.Attack.WasPressedThisFrame())
        {
            QueueNextAttack();
        }

        if (triggerCalled)
        {
            HandleStateExit();
        }
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex++;
        lastTimeAttacted = Time.time;
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        stateMachine.ChangeState(player.idleState);
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
        {
            comboAttackQueued = true;
        }
    }

    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
        {
            player.SetVelocity(0, rb.velocity.y);
        }
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];


        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
    }
    private void ResetComboIndexIfNeeded()
    {

        if (Time.time > lastTimeAttacted + player.comboResetTime)
        {
            comboIndex = FirstComboIndex;
        }
        if (comboIndex > comboLimit || Time.time > lastTimeAttacted + player.comboResetTime)
        {
            comboIndex = FirstComboIndex;
        }
    }
}
