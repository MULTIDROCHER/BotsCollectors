using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ResourceCounter))]
public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private Flag _flagTemplate;
    [SerializeField] private ResourceCounter _resourceCounter;

    protected Camera Camera;
    protected RaycastHit Hit;

    private Flag _flag;
    private int _basePrice = 5;

    public Flag Flag => _flag;

    public UnityAction StartBuildBase;

    private void Awake()
    {
        Camera = Camera.main;
    }

    private void OnMouseDown()
    {
        if (GetHit(out Hit).TryGetComponent(out Base currentBase))
        {
            if (_flag == null)
                CreateFlag(Hit.point);
            else
                MoveFlag(Hit.point);
        }
    }

    protected Collider GetHit(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
            return hit.collider;
        else
            return null;
    }

    private void OnFlagPlaced(Flag flag)
    {
        if (_resourceCounter.TryBuy(_basePrice))
        {
            GetComponent<Base>().isFlag = true;
            StartBuildBase?.Invoke();
        }
    }

    private void CreateFlag(Vector3 position)
    {
        _flag = Instantiate(_flagTemplate);
        MoveFlag(position);
    }

    private void MoveFlag(Vector3 position)
    {
        _flag.transform.position = MakeOffset(position);
        _flag.Moving?.Invoke();
        _flag.FlagPlaced += OnFlagPlaced;
    }

    protected Vector3 MakeOffset(Vector3 position)
    {
        float offset = .5f;
        return new Vector3(position.x, position.y - offset, position.z);
    }
}