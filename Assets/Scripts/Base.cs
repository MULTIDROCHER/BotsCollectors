using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Base : UnitManager
{
    [SerializeField] private Transform _oreContainer;
    public bool isFlag = false;

    private List<Ore> _toMine = new List<Ore>();
    private List<Ore> _taken = new List<Ore>();
    private BaseBuilder _builder;

    private void Start()
    {
        GetOrePool(_oreContainer);
        _builder = GetComponent<BaseBuilder>();
        //_builder.StartBuildBase += SendUnitToNewBase;
    }

    private void Update()
    {
        if (!isFlag)
            SendUnitToMining();
        else
            SendUnitToNewBase();
    }

    public void ResetOre(Ore ore)
    {
        _taken.Remove(ore);
        ore.transform.SetParent(_oreContainer);
        ore.gameObject.SetActive(false);
    }

    private void SendUnitToMining()
    {
        if (TryGetOre(out Ore target) && _taken.Contains(target) == false)
        {
            if (TryGetUnit(out Unit unit))
            {
                _taken.Add(target);
                unit.Mine(target);
            }
        }
    }

    private void SendUnitToNewBase()
    {
        Debug.Log("open base1");
        if (TryGetUnit(out Unit unit)){
            unit.OpenBase(_builder.Flag);
            isFlag = false;
        }
    }

    private bool TryGetOre(out Ore ore)
    {
        var activeOres = _toMine.Where(o => o.gameObject.activeSelf).ToArray();
        ore = null;

        if (activeOres.Length > 0)
            ore = activeOres[Random.Range(0, activeOres.Length)];

        return ore != null;
    }

    private void GetOrePool(Transform container)
    {
        foreach (Transform ore in container)
            _toMine.Add(ore.GetComponent<Ore>());
    }
}