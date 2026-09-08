using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_JumpAttackState : PlayerState
{
    private bool touchedGround;
    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animBoolname) : base(player, stateMachine, animBoolname)
    {

    }

    public override void Enter()
    {
        base.Enter();

        touchedGround = false;

        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDer, player.jumpAttackVelocity.y);
    }
    public override void update()
    {
        base.update();

        if (player.groundDetected && touchedGround == false)
        {
            touchedGround = true;
            anim.SetTrigger("jumpAttackTigger");
            player.SetVelocity(0, rb.velocity.y);
        }

        if(triggerCalled && player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
