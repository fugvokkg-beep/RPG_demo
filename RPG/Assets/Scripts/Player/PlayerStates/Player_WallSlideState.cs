public class Player_WallSlideState : PlayerState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolname) : base(player, stateMachine, animBoolname)
    {

    }

    public override void update()
    {
        base.update();


        HandleWallSlide();

        if (input.Player.Jump.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.wallJumpState);
        }
        if (player.wallDetected == false)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
            if (player.facingDer != player.moveInput.x)
            {
                player.Filp();
            }
        }

    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0 && player.groundDetected == false)
        {
            player.SetVelocity(player.moveInput.x, rb.velocity.y);
        }
        else
        {
            player.SetVelocity(player.moveInput.x, rb.velocity.y * player.wallSlideSlowMultiplier);
        }
    }
}
