using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();

       if(IsHarvestableObjectNearby())
            stateMachine.ChangeState(player.HarvestState);
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

        if (player.TargetMonster != null)
            player.transform.LookAt(player.TargetMonster.transform.position, Vector3.up);
    }

    private bool IsHarvestableObjectNearby()
    {
        Vector3 harvestObjPos = player.FindHarvestableObject();

        if (harvestObjPos == Vector3.zero)
            return false;

        player.transform.LookAt(harvestObjPos, Vector3.up);
        return true;
    }
}
