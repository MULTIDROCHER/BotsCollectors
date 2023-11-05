using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private Base _baseTemplate;
    [SerializeField] private Flag _flagTemplate;
    [SerializeField] private Text _resourceText;

    protected Ground Ground;
    protected Vector3 MousePosition;

    private TextContainer _textContainer;
    private Flag _flagInstance;
    private Camera _camera;

    private void Awake()
    {
        Ground = FindObjectOfType<Ground>();
        _textContainer = FindObjectOfType<TextContainer>();
        _camera = Camera.main;
    }

    private void Update()
    {
        MousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        MousePosition.z = _camera.transform.position.y;
    }

    private void OnMouseDown()
    {
        if (_flagInstance != null)
        {
            Debug.LogError("this base already has flag");
            return;
        }

        SetFlag();
    }

    private void SetFlag()
    {
        _flagInstance = Instantiate(_flagTemplate, MousePosition, Quaternion.identity);
        _flagInstance.GetComponent<Renderer>().material.color = new Color(1, 1, 1, .5f);
    }
}