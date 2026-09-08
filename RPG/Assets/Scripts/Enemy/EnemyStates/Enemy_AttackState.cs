using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackState : EnemyState
{
    public Enemy_AttackState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {

    }
    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocity(0, rb.velocity.y);
    }

    public override void update()
    {
        base.update();


        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
