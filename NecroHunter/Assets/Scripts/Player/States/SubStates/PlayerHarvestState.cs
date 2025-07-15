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
        SetHarvestState(true);
    }

    public override void Exit()
    {
        base.Exit();
        SetHarvestState(false);
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

    private void SetHarvestState(bool active)
    {
        if (player.HarvestableTarget == null)
            return;

        int resourceType = (int)player.HarvestableTarget.ResourceData.resourceType;
        player.SetToolActive(resourceType, active);

        player.Anim.SetBool(playerData.harvestAnimNames[resourceType], active);
    }

}
