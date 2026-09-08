using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StunnedState : EnemyState
{

    private Enemy_VFX vfx;
    public Enemy_StunnedState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        vfx = enemy.GetComponent<Enemy_VFX>();
    }

    public override void Enter()
    {
        base.Enter();

        vfx.EnableAttackAlert(false);
        enemy.EnableCounterwindow(false);

        stateTimer = enemy.stunnedDuration;
        rb.velocity = new Vector2(enemy.stunnedVelocity.x * -enemy.facingDer, enemy.stunnedVelocity.y);
    }

    public override void update()
    {
        base.update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.battleState);
    }
}
