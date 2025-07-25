using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
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
                current++;
                break;
            case EState.Success:
                return EState.Success;
        }
        return current == children.Count ? EState.Failure : EState.Running;
    }
}
