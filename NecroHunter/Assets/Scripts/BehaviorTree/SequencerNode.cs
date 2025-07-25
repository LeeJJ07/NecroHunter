using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : CompositeNode
{
    private int current;

    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {
    }

    protected override EState OnUpdate()
    {
        var child = children[current];
        switch (child.Update())
        {
            case EState.Running:
                return EState.Running;
            case EState.Failure:
                return EState.Failure;
            case EState.Success:
                current++;
                break;
        }
        return current == children.Count ? EState.Success : EState.Running;
    }
}
