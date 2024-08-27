using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Raycasthandler : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 _origin;
    private Vector3 _target;
    private Vector3 _direction;
    private Ray _ray;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ConfigureLineRenderer();

    }

    private void ConfigureLineRenderer()
    {
        if(lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
            lineRenderer.enabled = false;
        }
    }

    public void CreateRay(Vector3 origin, Vector3 target)
    {
        _origin = origin;
        _target = target;
        _direction = (_target - _origin).normalized;
        _ray = new Ray(origin, _direction);

        DrawRay();
    }

    private void DrawRay()
    {
        Debug.DrawRay(_ray.origin, _ray.direction * Vector3.Distance(_origin, _target), Color.red);
    }
}
