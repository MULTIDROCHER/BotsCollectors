using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private bool _isFree = true;
    private UnitStateMachine _stateMachine;

    public bool IsFree => _isFree;

    private void Start()
    {
        _stateMachine = new UnitStateMachine();
        _stateMachine.Initialize(new IdleState());
    }

    public void GoToOre(Ore ore)
    {
        _isFree = false;
        TakeOreState goToOre = new TakeOreState(this, ore.transform.position);
        
        _stateMachine.ChangeState(goToOre);
    }

    public void GoToBase(){
        _isFree = true;
    }
}
