using UnityEngine;

[RequireComponent(typeof(ResourceCounter))]
public class FlagSpawner : InHitSpawner
{
    [SerializeField] private Flag _flagTemplate;

    private ResourceCounter _counter;
    private Base _currentBase;

    public Flag Flag { get; private set; }

    private void Awake()
    {
        TryGetComponent(out _counter);
        TryGetComponent(out _currentBase);
    }

    private void Update()
    {
        if (Flag != null)
            OnFlagSpawned(Flag);
    }

    private void OnMouseDown()
    {
        if (GetHit(out RaycastHit hit))
            if (Flag != null)
                Flag.Replase(hit.point);
            else
                SpawnFlag(hit.point);
    }

    private void SpawnFlag(Vector3 position)
    {
        Flag = Instantiate(_flagTemplate, position, Quaternion.identity);

        //Flag.FlagPlaced += OnFlagSpawned;
    }

    private void OnFlagSpawned(Flag flag)
    {
        if (_counter.TryBuy(5))
            _currentBase.SendUnitToOpenBase(flag);
    }
}