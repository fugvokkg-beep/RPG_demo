using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    private float lastTimeWasInButtle;

    //private float retreatTimer;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        UpdateBattleTimer();

        player ??= enemy.GetplayerReferense();

        //?? retreatTimer?
        //if (ShouldRetreat())
        //{
        //    rb.velocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
        //    enemy.HandleFilp(DirectionToPlayer());
        //}
    }

    public override void update()
    {
        base.update();

        if (enemy.PlayerDetected())
            UpdateBattleTimer();

        if (BattleTimeIsOver())
            stateMachine.ChangeState(enemy.idleState);

        if (WithinAttackRange() && enemy.PlayerDetected())
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.velocity.y);
        }
    }
    private void UpdateBattleTimer() => lastTimeWasInButtle = Time.time;
    private bool BattleTimeIsOver() => Time.time > lastTimeWasInButtle + enemy.battleTimeDuration;
    //private bool ShouldRetreat() => DistanseToPlayer() < enemy.minRetreatDistance;

    private bool WithinAttackRange()
    {
        return DistanseToPlayer() < enemy.attackDistanse;
    }

    private float DistanseToPlayer()
    {
        if (player == null)
        {
            return float.MaxValue;
        }

        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    private float DirectionToPlayer()
    {
        if (player == null)
            return 0;



        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
