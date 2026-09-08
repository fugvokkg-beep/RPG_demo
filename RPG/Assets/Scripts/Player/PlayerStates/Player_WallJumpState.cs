public class Player_WallJumpState : PlayerState
{
    public Player_WallJumpState(Player player, StateMachine stateMachine, string animBoolname) : base(player, stateMachine, animBoolname)
    {

    }


    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.wallJumpForce.x * -player.facingDer, player.wallJumpForce.y);
    }

    public override void update()
    {
        base.update();

        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
        if (player.wallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }

}
