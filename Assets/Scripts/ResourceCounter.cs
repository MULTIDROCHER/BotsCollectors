using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] private Text _text;

    private float _duration = 1;
    private int _count;

    public void AddResource()
    {
        _count++;
        _text.DOText(_count.ToString(), _duration, true, ScrambleMode.All);
    }
}