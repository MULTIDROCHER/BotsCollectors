using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Flag : InHitSpawner
{
    private bool _isMoving = true;
    private Base _newBase;

    public UnityAction<Flag> FlagPlaced;

    public Base Base => _newBase;

    private void Update()
    {
        if (_isMoving)
            Move();
    }

    public void GetBase(Base spawned) => _newBase = spawned; 

    public void Replase(Vector3 position)
    {
        transform.position = position;
        _isMoving = true;
    }

    private void Move()
    {
        float speed = 15;
        _isMoving = true;

        if (GetHit(out RaycastHit hit) != null
        && hit.collider.TryGetComponent(out Ground ground))
            transform.DOMove(hit.point, speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(1))
            StopMoving(hit.point);
    }

    private void StopMoving(Vector3 position)
    {
        _isMoving = false;

        FlagPlaced?.Invoke(this);
    }
}