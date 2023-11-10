using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Flag : MonoBehaviour
{
    [SerializeField] private Base _baseTemplate;

    private bool _isMoving = true;
    private Collider _groundCollider;

    //public Base NewBase { get; private set; }

    public UnityAction<Flag> FlagPlaced;

    private void Awake()
    {
        //_groundCollider = FindObjectOfType<Ground>().GetComponent<Collider>();
    }

    private void Update()
    {
        if (_isMoving)
            Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit) && unit.Flag == this)
        {
            Debug.Log("unit is here" + unit.Flag.name);
            BuildNewBase();
        }
    }

    private void Move()
    {
        float speed = 15;
        _isMoving = true;
        RaycastHit hit;


        //f (GetHit(out hit) != null && hit.collider == _groundCollider)
            //transform.DOMove(hit.point, speed * Time.deltaTime);

        //if (Input.GetMouseButtonDown(1))
            //StopMoving(hit.point);
    }

    private void StopMoving(Vector3 position)
    {
        _isMoving = false;
       //transform.position = MakeOffset(position);
        FlagPlaced?.Invoke(this);
    }

    public void BuildNewBase()
    {
        //NewBase = Instantiate(_baseTemplate, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}