using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    public Enemy_DeadState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        anim.enabled = false;

        rb.gravityScale = 15;
        enemy.GetComponent<Collider2D>().enabled = false;

        stateMachine.SwitchOffStateMachine();
    }
}
