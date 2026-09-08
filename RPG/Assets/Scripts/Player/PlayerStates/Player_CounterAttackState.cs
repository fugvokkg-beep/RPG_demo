public class Player_CounterAttackState : PlayerState
{

    private Player_Combat combat;
    private bool counterSomebody;
    public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        combat = player.GetComponent<Player_Combat>();
    }

    public override void Enter()
    {
        base.Enter();

        counterSomebody = combat.CounterAttackPerforned();
        stateTimer = combat.GetcounterRecoveryDuration();

        anim.SetBool("counterAttackPerformed", counterSomebody);
    }

    public override void update()
    {
        base.update();

        player.SetVelocity(0, rb.velocity.y);

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
            anim.SetBool("counterAttackPerformed", false);
            anim.SetBool("counterAttack", false);
        }

        if (stateTimer < 0 && counterSomebody == false)
            stateMachine.ChangeState(player.idleState);
    }
}
