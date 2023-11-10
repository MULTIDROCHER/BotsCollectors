using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class OpenBaseState : State
{
    private Unit _unit;
    private Base _base;
    private Flag _flag;

    public UnityAction<Base> BaseOpened;

    public OpenBaseState(Unit unit, Flag flag)
    {
        _unit = unit;
        _flag = flag;
    }

    public override void Enter()
    {
        GoToFlag().OnComplete(() =>
        {
            GetBase();
        });
    }

    private Tween GoToFlag()
    {
        float distance = Vector3.Distance(_unit.transform.position, _flag.transform.position);
        float duration = distance / _unit.Speed;

        return _unit.transform.DOMove(_flag.transform.position, duration, false);
    }

    private void GetBase()
    {
        //_base = _flag.Base;
        _unit.transform.parent = _base.transform;
        BaseOpened?.Invoke(_base);
    }
}