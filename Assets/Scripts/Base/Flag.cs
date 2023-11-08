using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Flag : BaseBuilder
{
    [SerializeField] private Base _baseTemplate;

    private Base _newBase;
    private bool _isMoving = true;
    private Collider _groundCollider;

    public Base Base => _newBase;

    public UnityAction Moving;
    public UnityAction<Flag> FlagPlaced;

    private void Awake()
    {
        _groundCollider = FindObjectOfType<Ground>().GetComponent<Collider>();
    }

    private void Update()
    {
        if (_isMoving)
            Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit) && unit.Flag == this){
            Debug.Log("unit is here");
            BuildNewBase();
        }
    }

    private void Move()
    {
        float speed = 15;
        _isMoving = true;
        RaycastHit hit;


        if (GetHit(out hit) != null && hit.collider == _groundCollider)
            transform.DOMove(hit.point, speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(1))
            StopMoving(hit.point);
    }

    private void StopMoving(Vector3 position)
    {
        _isMoving = false;
        transform.position = MakeOffset(position);
        FlagPlaced?.Invoke(this);
    }

    private void BuildNewBase()
    {
        var newBase = Instantiate(_baseTemplate, transform.position, Quaternion.identity);
        _newBase = newBase;
        Destroy(gameObject);
    }
}