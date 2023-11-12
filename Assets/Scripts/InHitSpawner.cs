using UnityEngine;

public abstract class InHitSpawner : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    protected Collider GetHit(out RaycastHit hit)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
            return hit.collider;
        else
            return null;
    }
}