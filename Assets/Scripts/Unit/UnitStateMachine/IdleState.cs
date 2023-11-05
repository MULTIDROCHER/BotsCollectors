using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        Debug.Log("im free and waiting for order");
    }
}