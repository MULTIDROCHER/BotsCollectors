using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class MiningState : State
{
    private Unit _unit;
    private Ore _ore;

    public UnityAction<Ore> OreMined;

    public MiningState(Unit unit, Ore ore)
    {
        _unit = unit;
        _ore = ore;
    }

    public override void Enter()
    {
        GoToOre().OnComplete(() =>
        {
            TakeOre();
        });
    }

    private Tween GoToOre()
    {
        float distance = Vector3.Distance(_unit.transform.position, _ore.transform.position);
        float duration = distance / _unit.Speed;

        return _unit.transform.DOMove(_ore.transform.position, duration, false);
    }

    private void TakeOre()
    {
        float offsetY = .5f;
        Vector3 takenOrePosition = new Vector3
        (_unit.transform.position.x, _unit.transform.position.y + offsetY, _unit.transform.position.z);

        _ore.transform.SetParent(_unit.transform);
        _ore.transform.position = takenOrePosition;
        OreMined?.Invoke(_ore);
    }
}