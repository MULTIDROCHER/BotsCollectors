using UnityEngine;

[RequireComponent(typeof(MiningManager))]
[RequireComponent(typeof(UnitManager))]
public class Base : MonoBehaviour
{
    private BaseBuilder _builder;
    private MiningManager _miningMng;
    private UnitManager _unitMng;
    public bool isFlag = false;

    private void Start()
    {
        TryGetComponent(out _builder);
        TryGetComponent(out _miningMng);
        TryGetComponent(out _unitMng);
    }

    private void Update()
    {
        if (!isFlag)
            SendUnitToMining();
        else
            SendUnitToNewBase();
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

    private void SendUnitToNewBase()
    {
        if (_unitMng.TryGetUnit(out Unit unit))
        {
            unit.OpenBase(_builder.Flag);
            isFlag = false;
        }
    }
}