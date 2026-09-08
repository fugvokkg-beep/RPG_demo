using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected PlayerInputSet input;
   


    public PlayerState(Player player,StateMachine stateMachine, string animBoolName) : base(stateMachine,animBoolName)
    {
        this.player = player;

        anim = this.player.anim;
        rb = this.player.rb;
        input = this.player.input;
    }



    public override void update()
    {
        base.update();


        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }
    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();

        anim.SetFloat("yVelocity", rb.velocity.y);
    }
    private bool CanDash()
    {
        if (player.wallDetected)
        {
            return false;
        }

        if (stateMachine.currentState == player.dashState)
        {
            return false;
        }
        return true;
    }
}
