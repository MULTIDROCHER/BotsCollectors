using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(ResourceCounter))]
public class UnitManager : MonoBehaviour
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

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(1) && _counter.TryBuy(_unitPrice))
            SpawnUnit();
    }

    public bool TryGetUnit(out Unit unit)
    {
        unit = _units.FirstOrDefault(u => u.IsFree);
        return unit != null;
    }

    private void SpawnUnit()
    {
        /* float spawnPositionY = 0.4f;
        Vector3 spawnPoin = new Vector3(transform.position.x, spawnPositionY, transform.position.z); */

        var spawned = Instantiate(_template, transform.position, Quaternion.identity);
        spawned.transform.SetParent(this.transform);
        _units.Add(spawned);
    }
}