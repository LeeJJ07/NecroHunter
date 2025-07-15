using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvestState : PlayerState
{
    public PlayerHarvestState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (movementAmount.magnitude != 0.0f)
            stateMachine.ChangeState(player.MoveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
