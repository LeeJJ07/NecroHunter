using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;
    private string animBoolName;

    protected Vector2 movementAmount;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        DoCheck();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;

        Debug.Log(animBoolName);
    }
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {
        movementAmount = player.InputHandler.MovementInput;
    }
    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }

    public virtual void DoCheck()
    {

    }
}
