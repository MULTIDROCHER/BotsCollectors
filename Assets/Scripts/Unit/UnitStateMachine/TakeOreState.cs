using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TakeOreState : State
{
    private float _duration = 3;
    private Unit _unit;
    private Vector3 _orePosition;

    public TakeOreState(Unit unit, Vector3 orePosition)
    {
        _unit = unit;
        _orePosition = orePosition;
    }

    public override void Enter()
    {
        Debug.Log("go to ore");
        _unit.transform.DOMove(_orePosition, _duration, false);
    }

    public override void Exit()
    {
        Debug.Log("ore is taken");
    }
}
