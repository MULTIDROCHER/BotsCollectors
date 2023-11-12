using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ResourceCounter))]
public class FlagSpawner : InHitSpawner
{
    [SerializeField] private Flag _flagTemplate;

    private ResourceCounter _counter;
    private int _basePrice = 5;

    public UnityAction<Flag> AbleToBuildBase;

    public Flag Flag { get; private set; }

    private void Awake()
    {
        TryGetComponent(out _counter);
    }

    private void OnMouseDown()
    {
        if (GetHit(out RaycastHit hit))
            if (Flag != null)
                Flag.Replace(hit.point);
            else
                SpawnFlag(hit.point);
    }

    private void SpawnFlag(Vector3 position)
    {
        Flag = Instantiate(_flagTemplate, position, Quaternion.identity);

        Flag.FlagPlaced += OnFlagPlaced;
    }

    private void OnFlagPlaced(Flag flag)
    {
        StartCoroutine(WaitForEnoughBalance(flag));
    }

    private IEnumerator WaitForEnoughBalance(Flag flag)
    {
        while (_counter.Count < _basePrice)
            yield return null;

        BuildBase(flag);
    }

    private void BuildBase(Flag flag)
    {
        if (_counter.TryBuy(_basePrice))
            AbleToBuildBase?.Invoke(flag);

        StopCoroutine(WaitForEnoughBalance(flag));
    }
}