using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private Base _baseTemplate;

    private Flag _flag;

    private void Awake()
    {
        TryGetComponent(out _flag);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit) 
        && unit.Flag == _flag)
            BuildNewBase();
    }

    private void BuildNewBase()
    {
        var newBase = Instantiate(_baseTemplate, transform.position, Quaternion.identity);
        _flag.GetBase(newBase);
        Destroy(gameObject);
    }
}