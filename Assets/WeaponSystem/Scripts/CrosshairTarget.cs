using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    Camera camera;
    Ray ray;
    RaycastHit hitInfo;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        ray.origin = camera.transform.position;
        ray.direction = camera.transform.forward;

        Physics.Raycast(ray, out hitInfo);
        transform.position = hitInfo.point;
    }
}
