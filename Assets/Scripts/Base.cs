using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Base : MonoBehaviour
{
    [SerializeField] private Unit[] _units;
    [SerializeField] private Transform _oreContainer;

    private List<Ore> _ores;

    private void Start()
    {
        _ores = new List<Ore>(_oreContainer.GetComponentsInChildren<Ore>());
    }

    private void Update()
    {
        TrySendUnit();
    }

    private void TrySendUnit()
    {
        Debug.Log("try send unit");
        if (GetOre(out Ore target))
        {
            Debug.Log("1 succsess");
            var unit = _units.FirstOrDefault(unit => unit.IsFree);

            if (unit != null)
            {
                Debug.Log("2 succsess");
                unit.GoToOre(target);
            }
        }
    }

    private bool GetOre(out Ore ore)
    {
        var activeOres = _ores.Where(ore => ore.gameObject.activeSelf).ToArray();
        ore = null;

        if (activeOres.Length > 0)
        {
            ore = activeOres[Random.Range(0, activeOres.Length)];
            Debug.Log(ore.transform.position);
        }

        return ore != null;
    }
}