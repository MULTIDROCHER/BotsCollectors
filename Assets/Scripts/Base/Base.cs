using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MiningManager))]
[RequireComponent(typeof(UnitManager))]
[RequireComponent(typeof(FlagSpawner))]
public class Base : MonoBehaviour
{
    private MiningManager _miningMng;
    private UnitManager _unitMng;
    private FlagSpawner _flagSpawner;
    private WaitForSeconds _waitForSeconds;
    private float _delay = .2f;

    private void Awake()
    {
        TryGetComponent(out _miningMng);
        TryGetComponent(out _unitMng);
        TryGetComponent(out _flagSpawner);

        _waitForSeconds = new WaitForSeconds(_delay);
    }

    private void OnEnable()
    {
        _flagSpawner.AbleToBuildBase += SendUnitToOpenBase;
    }

    private void OnDisable()
    {
        _flagSpawner.AbleToBuildBase -= SendUnitToOpenBase;
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

    private void SendUnitToOpenBase(Flag flag)
    {
        StartCoroutine(SearchUnit(flag));
    }

    private IEnumerator SearchUnit(Flag flag)
    {
        Unit unit = null;

        while (unit == null)
        {
            if (_unitMng.TryGetUnit(out unit))
            {
                unit.OpenBase(flag);
                yield break;
            }

            yield return null;
        }

        StopCoroutine(SearchUnit(flag));
    }
}