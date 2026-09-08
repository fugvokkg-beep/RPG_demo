public class Player_AiredState : PlayerState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string animBoolname) : base(player, stateMachine, animBoolname)
    {

    }

    public override void update()
    {
        base.update();

        if (player.moveInput.x != 0)
        {
            player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.inAirMoveMultiplier), rb.velocity.y);
        }
        else if (player.moveInput.x == 0)
        {
            player.SetVelocity(0, rb.velocity.y);
        }

        if (input.Player.Attack.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.jumpAttackState);
        }
    }
}
