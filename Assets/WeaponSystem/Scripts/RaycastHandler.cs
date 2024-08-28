using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Raycasthandler : MonoBehaviour
{
    public static Raycasthandler Instance { get; private set; }

    private LineRenderer lineRenderer;
    private Vector3 _target;
    private Ray _ray;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
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
        }
    }

    public Ray CreateRay(Vector3 origin, Vector3 target)
    {
        _ray.origin = origin;
        _target = target;
        _ray.direction = (target - origin).normalized;

        DrawRay();
        return _ray;
    }

    private void DrawRay()
    {
        lineRenderer.SetPosition(0, _ray.origin);
        Debug.DrawRay(_ray.origin, _ray.direction * Vector3.Distance(_ray.origin, _target), Color.red);
        lineRenderer.SetPosition(1, _target);
    }
}
