using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MiningManager))]
[RequireComponent(typeof(UnitManager))]
public class Base : MonoBehaviour
{
    private MiningManager _miningMng;
    private UnitManager _unitMng;
    private WaitForSeconds _waitForSeconds;
    private float _delay = .2f;

    private void Awake()
    {
        TryGetComponent(out _miningMng);
        TryGetComponent(out _unitMng);

        _waitForSeconds = new WaitForSeconds(_delay);
    }

    private void Start()
    {
        StartCoroutine(SendUnitToMining());
    }

    public void OnOreDelivered(Ore ore) => _miningMng.ResetOre(ore);

    private IEnumerator SendUnitToMining()
    {
        while (true)
        {
            yield return _waitForSeconds;

            if (_miningMng.TryGetOre(out Ore target)
            && _unitMng.TryGetUnit(out Unit unit))
            {
                _miningMng.OnOreTaken(target);
                unit.Mine(target);
            }
        }
    }

    public void SendUnitToOpenBase(Flag flag)
    {
        if (_unitMng.TryGetUnit(out Unit unit))
            unit.OpenBase(flag);
    }
}