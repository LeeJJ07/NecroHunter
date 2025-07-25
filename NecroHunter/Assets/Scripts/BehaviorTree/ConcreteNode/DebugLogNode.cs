using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogNode : ActionNode
{
    public string message;
    protected override void OnStart()
    {
        Debug.Log($"OnStart{message}");
    }

    protected override void OnStop()
    {
        Debug.Log($"OnStop{message}");
    }

    protected override EState OnUpdate()
    {
        Debug.Log($"OnUpdate{message}");

        Debug.Log($"Blackboard: {blackboard.moveToPosition}");
        blackboard.moveToPosition.x += Time.deltaTime;
        return EState.Success;
    }
}
