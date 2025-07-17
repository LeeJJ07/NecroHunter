using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHaulingState : PlayerState
{
    public PlayerHaulingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        Vector3 scaledMovement = player.StatHandler.GetStat(EStatType.MOVE_SPEED) * Time.deltaTime * new Vector3(
            movementAmount.x,
            0.0f,
            movementAmount.y
        );

        player.Agent.Move(scaledMovement);
        player.transform.LookAt(player.transform.position + scaledMovement, Vector3.up);

        if (movementAmount.magnitude == 0.0f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
