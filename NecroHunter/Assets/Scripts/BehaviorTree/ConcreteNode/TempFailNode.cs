using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFailNode : ActionNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
        
    }

    protected override EState OnUpdate()
    {
        return EState.Failure;
    }
}
