using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MiningManager : MonoBehaviour
{
    private OreContainer _oreContainer;
    private List<Ore> _oresToMine = new List<Ore>();
    private List<Ore> _takenOres = new List<Ore>();

    private void Start()
    {
        _oreContainer = FindObjectOfType<OreContainer>();
        GetOrePool(_oreContainer);
    }

    public void ResetOre(Ore ore)
    {
        _takenOres.Remove(ore);
        ore.transform.SetParent(_oreContainer.transform);
        ore.gameObject.SetActive(false);
    }

    public bool TryGetOre(out Ore ore)
    {
        var activeOres = _oresToMine.Where(o => o.gameObject.activeSelf).ToArray();
        ore = null;

        if (activeOres.Length > 0)
            ore = activeOres[Random.Range(0, activeOres.Length)];

        return ore != null && _takenOres.Contains(ore) == false;
    }

    public void OnOreTaken(Ore ore) => _takenOres.Add(ore);

    private void GetOrePool(OreContainer container)
    {
        foreach (Transform ore in container.transform)
            _oresToMine.Add(ore.GetComponent<Ore>());
    }
}