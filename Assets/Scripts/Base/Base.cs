using UnityEngine;

[RequireComponent(typeof(MiningManager))]
[RequireComponent(typeof(UnitManager))]
public class Base : MonoBehaviour
{
    private MiningManager _miningMng;
    private UnitManager _unitMng;

    private void Awake()
    {
        TryGetComponent(out _miningMng);
        TryGetComponent(out _unitMng);
    }

    private void Update()
    {
        SendUnitToMining();
    }

    private void SendUnitToMining()
    {
        if (_miningMng.TryGetOre(out Ore target)
        && _unitMng.TryGetUnit(out Unit unit))
        {
            Debug.Log("yes");
            unit.Mine(target);
            _miningMng.OnOreTaken(target);
        }
    }

    public void OnOreDelivered(Ore ore) => _miningMng.ResetOre(ore);
}