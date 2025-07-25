using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : ScriptableObject
{
    public enum EState
    {
        Running,
        Failure,
        Success
    }

    [HideInInspector] public EState state = EState.Running;
    [HideInInspector] public bool started = false;
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;
    [HideInInspector] public Blackboard blackboard;
    //[HideInInspector] public AiAgent agent;
    [TextArea] public string description;

    public EState Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }

        state = OnUpdate();

        if(state == EState.Failure || state == EState.Success)
        {
            OnStop();
            started = false;
        }

        return state;
    }
    public virtual Node Clone()
    {
        return Instantiate(this);
    }
    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract EState OnUpdate();

}
