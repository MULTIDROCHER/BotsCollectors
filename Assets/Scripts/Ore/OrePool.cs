using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OrePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;

    private List<Ore> _pool = new List<Ore>();

    protected void Initialize(Ore ore)
    {
        for (int i = 0; i < _capacity; i++)
        {
            var spawned = Instantiate(ore, _container);
            spawned.gameObject.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetOre(out Ore ore)
    {
        ore = _pool.FirstOrDefault(oreObject => oreObject.gameObject.activeSelf == false);

        return ore != null;
    }
}