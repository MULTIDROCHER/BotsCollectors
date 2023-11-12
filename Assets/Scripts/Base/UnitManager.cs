using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(ResourceCounter))]
public class UnitManager : InHitSpawner
{
    [SerializeField] private Unit _template;

    private List<Unit> _units = new List<Unit>();
    private ResourceCounter _counter;
    private int _unitPrice = 3;

    private void Awake()
    {
        TryGetComponent(out _counter);
        _units.AddRange(GetComponentsInChildren<Unit>());
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1)
        && GetHit(out RaycastHit hit) == GetComponent<Collider>()
        && _counter.TryBuy(_unitPrice))
            SpawnUnit();
    }

    public bool TryGetUnit(out Unit unit)
    {
        unit = _units.FirstOrDefault(u => u.IsFree);
        return unit != null;
    }

    public void RemoveUnit(Unit unit, Base newBase)
    {
        if (_units.Contains(unit))
        {
            _units.Remove(unit);
            newBase.TryGetComponent(out UnitManager manager);
            manager.AddUnit(unit);
        }
    }

    private void SpawnUnit()
    {
        float scaleY = 3;

        var spawned = Instantiate(_template, transform.position, Quaternion.identity, transform);
        spawned.transform.localScale = new Vector3(spawned.transform.localScale.x, scaleY, spawned.transform.localScale.z);
        AddUnit(spawned);
    }

    private void AddUnit(Unit unit)
    {
        _units.Add(unit);
    }
}