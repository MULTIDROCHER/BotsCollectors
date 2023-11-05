using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(ResourceCounter))]
public class UnitManager : MonoBehaviour
{
    [SerializeField] protected List<Unit> _units = new List<Unit>();
    [SerializeField] private Unit _template;

    private ResourceCounter _counter;
    private int _unitPrice = 3;

    private void Awake()
    {
        TryGetComponent(out _counter);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
            if (_counter.TryBuy(_unitPrice))
                SpawnUnit();
    }

    protected bool TryGetUnit(out Unit unit)
    {
        unit = _units.FirstOrDefault(u => u.IsFree);
        return unit != null;
    }

    private void SpawnUnit()
    {
        float spawnPositionY = 0.4f;
        Vector3 spawnPoin = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

        var spawned = Instantiate(_template, spawnPoin, Quaternion.identity);
        spawned.transform.SetParent(this.transform);
        _units.Add(spawned);
    }
}