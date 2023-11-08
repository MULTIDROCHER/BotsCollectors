using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class DeliverOreState : State
{
    private Unit _unit;
    private Ore _ore;
    private Base _base;

    public UnityAction OreDelivered;

    public DeliverOreState(Unit unit, Ore ore, Base unitBase)
    {
        _unit = unit;
        _ore = ore;
        _base = unitBase;
    }

    public override void Enter()
    {
        GoToBase().OnComplete(() =>
        {
            GiveOre();
        });
    }

    private Tween GoToBase()
    {
        float distance = Vector3.Distance(_unit.transform.position, _base.transform.position);
        float duration = distance / _unit.Speed;

        return _unit.transform.DOMove(BasePosition(), duration, false);
    }

    private void GiveOre()
    {
        _base.TryGetComponent(out ResourceCounter counter);
        counter.AddResource();
        OreDelivered?.Invoke();
    }

    private Vector3 BasePosition()
    {
        float rangeSpread = 1;
        float randomDistance = Random.Range(-rangeSpread, rangeSpread);

        return new Vector3(_base.transform.position.x + randomDistance,
                           _unit.transform.position.y,
                           _base.transform.position.z + randomDistance);
    }
}