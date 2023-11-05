using UnityEngine;

[RequireComponent(typeof(Unit))]
public abstract class State
{
    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }
}